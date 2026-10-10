using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SafeDoc.Models;
using System;
using System.Text;
using System.Threading;

namespace SafeDoc.Device
{
    public sealed class SafeDocClient : IDisposable
    {
        private const string CommandPrefix = "a[";
        private const string CommandSuffix = "]";

        private readonly SerialPortManager _serialPort;
        private readonly object _sendLock = new object();
        private readonly object _responseLock = new object();
        private readonly AutoResetEvent _responseReady = new AutoResetEvent(false);
        private readonly StringBuilder _receivedText = new StringBuilder();

        private string _completeResponseJson;
        private Exception _communicationError;

        public SafeDocClient(string portName, int baudRate = 115200)
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

        public SafeDocResponse SendCommand(SafeDocCommand command, int timeoutMilliseconds = 10000)
        {
            if (command == null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            lock (_sendLock)
            {
                PrepareForNextResponse();

                string commandJson = JsonConvert.SerializeObject(command);
                string commandFrame = CommandPrefix + commandJson + CommandSuffix;
                _serialPort.ClearReceivedData();
                CommandSent?.Invoke(this, commandFrame);
                _serialPort.Send(commandFrame);

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

                string json = FindCompleteResponseJson(_receivedText.ToString());
                if (json == null)
                {
                    return;
                }

                _completeResponseJson = json;
                _responseReady.Set();
            }
        }

        private static string FindCompleteResponseJson(string text)
        {
            int startIndex = 0;
            while (startIndex < text.Length)
            {
                int firstBrace = text.IndexOf('{', startIndex);
                if (firstBrace < 0)
                {
                    return null;
                }

                int endIndex = FindJsonObjectEnd(text, firstBrace);
                if (endIndex < 0)
                {
                    return null;
                }

                string json = text.Substring(firstBrace, endIndex - firstBrace + 1);
                if (IsDeviceResponseJson(json))
                {
                    return json;
                }

                startIndex = endIndex + 1;
            }

            return null;
        }

        private static int FindJsonObjectEnd(string text, int firstBrace)
        {
            int openBraces = 0;
            bool isInsideString = false;
            bool isEscaped = false;

            for (int index = firstBrace; index < text.Length; index++)
            {
                char currentCharacter = text[index];

                if (isInsideString)
                {
                    if (isEscaped)
                    {
                        isEscaped = false;
                    }
                    else if (currentCharacter == '\\')
                    {
                        isEscaped = true;
                    }
                    else if (currentCharacter == '"')
                    {
                        isInsideString = false;
                    }

                    continue;
                }

                if (currentCharacter == '"')
                {
                    isInsideString = true;
                }
                else if (currentCharacter == '{')
                {
                    openBraces++;
                }
                else if (currentCharacter == '}')
                {
                    openBraces--;

                    if (openBraces == 0)
                    {
                        return index;
                    }
                }
            }

            return -1;
        }

        private static bool IsDeviceResponseJson(string json)
        {
            try
            {
                JObject responseObject = JObject.Parse(json);
                return responseObject["isSuccess"] != null
                    && responseObject["responseStatusCode"] != null;
            }
            catch (JsonReaderException)
            {
                return false;
            }
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
