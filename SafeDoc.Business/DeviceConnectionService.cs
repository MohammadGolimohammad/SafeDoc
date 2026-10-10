using System;
using SafeDoc.Device;
using SafeDoc.Models;

namespace SafeDoc.Business
{
    public sealed class DeviceConnectionService : IDisposable
    {
        private readonly SafeDocClient _client;

        public DeviceConnectionService(string portName, int baudRate = 115200)
        {
            _client = new SafeDocClient(portName, baudRate);
            _client.RawDataReceived += OnDataReceived;
            _client.ErrorOccurred += OnErrorOccurred;
            _client.CommandSent += OnCommandSent;
        }

        public bool IsConnected
        {
            get
            {
                return _client.IsConnected;
            }
        }

        public event EventHandler<string> RawDataReceived;
        public event EventHandler<string> ErrorOccurred;
        public event EventHandler<string> CommandSent;

        public void Connect()
        {
            _client.Connect();
        }

        public SafeDocResponse SendCommand(SafeDocCommand command, int timeoutMilliseconds = 2000)
        {
            return _client.SendCommand(command, timeoutMilliseconds);
        }

        public T ReadResponseData<T>(SafeDocResponse response)
            where T : class, new()
        {
            return _client.ReadResponseData<T>(response);
        }

        public System.Collections.Generic.List<T> ReadResponseList<T>(SafeDocResponse response)
        {
            return _client.ReadResponseList<T>(response);
        }

        private void OnDataReceived(object sender, string data)
        {
            RawDataReceived?.Invoke(this, data);
        }

        private void OnErrorOccurred(object sender, string message)
        {
            ErrorOccurred?.Invoke(this, message);
        }

        private void OnCommandSent(object sender, string command)
        {
            CommandSent?.Invoke(this, command);
        }

        public void Dispose()
        {
            _client.RawDataReceived -= OnDataReceived;
            _client.ErrorOccurred -= OnErrorOccurred;
            _client.CommandSent -= OnCommandSent;
            _client.Dispose();
        }
    }
}
