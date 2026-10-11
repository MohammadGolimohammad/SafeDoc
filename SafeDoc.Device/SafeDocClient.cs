using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SafeDoc.Models;
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

namespace SafeDoc.Device
{
    public sealed class SafeDocClient : IDisposable
    {
        private readonly SerialPortManager _serialPort;
        private readonly object _sendLock = new object();
        private readonly object _responseLock = new object();
        private readonly AutoResetEvent _responseReady = new AutoResetEvent(false);
        private readonly StringBuilder _receivedText = new StringBuilder();

        private string _completeResponseJson;
        private Exception _communicationError;

        public SafeDocClient(string portName, int baudRate)
        {
            _serialPort = new SerialPortManager(portName, baudRate);
            _serialPort.DataReceived += OnSerialDataReceived;
            _serialPort.ErrorOccurred += OnSerialError;
        }

        public bool IsConnected
        {
            get
            {
                return _serialPort.IsConnected;
            }
        }

        public string PortName
        {
            get
            {
                return _serialPort.PortName;
            }
        }

        public event EventHandler<string> CommandSent;
        public event EventHandler<string> RawDataReceived;
        public event EventHandler<string> ErrorOccurred;

        public void Connect()
        {
            _serialPort.Connect();
        }

        public void Disconnect()
        {
            _serialPort.Disconnect();
        }

        public SafeDocResponse SendCommand(SafeDocCommand command, int timeoutMilliseconds)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            lock (_sendLock)
            {
                PrepareForNextResponse();

                string commandJson = JsonConvert.SerializeObject(command);
                _serialPort.ClearReceivedData();
                CommandSent?.Invoke(this, commandJson);
                _serialPort.Send(commandJson);

                WaitForDeviceResponse(timeoutMilliseconds);

                lock (_responseLock)
                {
                    SafeDocResponse response = JsonConvert.DeserializeObject<SafeDocResponse>(
                        _completeResponseJson
                    );
                    if (response == null)
                    {
                        throw new InvalidOperationException("پاسخ دستگاه به مدل پاسخ قابل تبدیل نیست.");
                    }

                    return response;
                }
            }
        }

        public T ReadResponseData<T>(SafeDocResponse response)
            where T : class, new()
        {
            if (response == null || response.resultData == null || response.resultData.Count == 0)
            {
                return new T();
            }

            string dataJson = JsonConvert.SerializeObject(response.resultData[0]);
            return JsonConvert.DeserializeObject<T>(dataJson) ?? new T();
        }

        public SafeDocDeviceSettings ReadDeviceSettings(SafeDocResponse response)
        {
            SafeDocDeviceSettings settings = new SafeDocDeviceSettings();
            if (response == null || response.resultData == null)
            {
                return settings;
            }

            foreach (object result in response.resultData)
            {
                string resultJson = JsonConvert.SerializeObject(result);
                SafeDocDeviceSettings current = JsonConvert.DeserializeObject<SafeDocDeviceSettings>(
                    resultJson
                );
                if (current == null)
                {
                    continue;
                }

                if (resultJson.IndexOf("\"time\"", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    settings.Time = current.Time;
                }

                if (resultJson.IndexOf("\"date\"", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    settings.Date = current.Date;
                }

                if (resultJson.IndexOf("\"enterStatus\"", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    settings.EnterStatus = current.EnterStatus;
                }

                if (resultJson.IndexOf("\"usbConnectionTimeoutValue\"", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    settings.UsbConnectionTimeoutValue = current.UsbConnectionTimeoutValue;
                }

                if (resultJson.IndexOf("\"HidStatusAfterExpiration\"", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    settings.HidStatusAfterExpiration = current.HidStatusAfterExpiration;
                }

                if (resultJson.IndexOf("\"buzzerStatus\"", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    settings.BuzzerStatus = current.BuzzerStatus;
                }
            }

            return settings;
        }

        public System.Collections.Generic.List<T> ReadResponseList<T>(SafeDocResponse response)
        {
            if (response == null || response.resultData == null)
            {
                return new System.Collections.Generic.List<T>();
            }

            string dataJson = JsonConvert.SerializeObject(response.resultData);
            return JsonConvert.DeserializeObject<System.Collections.Generic.List<T>>(dataJson)
                ?? new System.Collections.Generic.List<T>();
        }

        private void PrepareForNextResponse()
        {
            lock (_responseLock)
            {
                _responseReady.Reset();
                _completeResponseJson = null;
                _communicationError = null;
                _receivedText.Clear();
            }
        }

        private void WaitForDeviceResponse(int timeoutMilliseconds)
        {
            bool responseArrived = _responseReady.WaitOne(timeoutMilliseconds);

            if (responseArrived == false)
            {
                throw new TimeoutException("دستگاه در زمان تعیین‌شده پاسخی ارسال نکرد.");
            }

            lock (_responseLock)
            {
                if (_communicationError != null)
                {
                    throw _communicationError;
                }

                if (string.IsNullOrWhiteSpace(_completeResponseJson))
                {
                    throw new InvalidOperationException("پاسخ کامل JSON از دستگاه دریافت نشد.");
                }
            }
        }

        private void OnSerialDataReceived(object sender, string receivedPart)
        {
            RawDataReceived?.Invoke(this, receivedPart);

            lock (_responseLock)
            {
                _receivedText.Append(receivedPart);
                string receivedJson = _receivedText.ToString().Trim('\0', ' ', '\r', '\n', '\t');
                receivedJson = NormalizeSettingsResponse(receivedJson);
                receivedJson = NormalizeFingerprintUserResponse(receivedJson);
                if (IsCompleteJson(receivedJson) == false)
                {
                    return;
                }

                JObject responseObject;
                try
                {
                    responseObject = JObject.Parse(receivedJson);
                }
                catch (JsonReaderException)
                {
                    return;
                }

                if (responseObject["isSuccess"] == null || responseObject["responseStatusCode"] == null)
                {
                    return;
                }

                _completeResponseJson = responseObject.ToString();
                _responseReady.Set();
            }
        }

        private static string NormalizeSettingsResponse(string receivedJson)
        {
            if (
                receivedJson.IndexOf("\"time\"", StringComparison.OrdinalIgnoreCase) < 0
                || receivedJson.IndexOf("\"date\"", StringComparison.OrdinalIgnoreCase) < 0
            )
            {
                return receivedJson;
            }

            string normalizedJson = Regex.Replace(receivedJson, @"\[\s*\{\s*\{", "[{");
            normalizedJson = Regex.Replace(
                normalizedJson,
                @"\}\s*,\s*\{\s*\""date\""\s*:\s*(\""[^\""\r\n]*\"")\s*\}\s*,",
                ", \"date\": $1,"
            );

            return normalizedJson;
        }

        private static string NormalizeFingerprintUserResponse(string receivedJson)
        {
            if (receivedJson.IndexOf("\"userId\"", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return receivedJson;
            }

            return Regex.Replace(
                receivedJson,
                @"(""resultData""\s*:\s*)\[\s*""userId""\s*:\s*(-?\d+)\s*\]",
                "$1[{\"userId\": $2}]"
            );
        }

        private static bool IsCompleteJson(string text)
        {
            if (string.IsNullOrWhiteSpace(text) || text[0] != '{' || text[text.Length - 1] != '}')
            {
                return false;
            }

            int openBraces = 0;
            bool insideText = false;
            bool escapedCharacter = false;

            foreach (char character in text)
            {
                if (insideText)
                {
                    if (escapedCharacter)
                    {
                        escapedCharacter = false;
                        continue;
                    }

                    if (character == '\\')
                    {
                        escapedCharacter = true;
                        continue;
                    }

                    if (character == '"')
                    {
                        insideText = false;
                    }

                    continue;
                }

                if (character == '"')
                {
                    insideText = true;
                    continue;
                }

                if (character == '{')
                {
                    openBraces++;
                    continue;
                }

                if (character == '}')
                {
                    openBraces--;
                    if (openBraces < 0)
                    {
                        return false;
                    }
                }
            }

            return openBraces == 0 && insideText == false;
        }

        private void OnSerialError(object sender, string errorMessage)
        {
            ErrorOccurred?.Invoke(this, errorMessage);

            lock (_responseLock)
            {
                _communicationError = new Exception(errorMessage);
                _responseReady.Set();
            }
        }

        public void Dispose()
        {
            _serialPort.DataReceived -= OnSerialDataReceived;
            _serialPort.ErrorOccurred -= OnSerialError;
            _responseReady.Dispose();
            _serialPort.Dispose();
        }
    }
}
