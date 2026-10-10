using System;
using System.IO.Ports;
using System.Text;

namespace SafeDoc.Device
{
    public sealed class SerialPortManager : IDisposable
    {
        private readonly SerialPort _serialPort;

        public bool IsConnected
        {
            get
            {
                return _serialPort != null && _serialPort.IsOpen;
            }
        }

        public string PortName
        {
            get
            {
                return _serialPort.PortName;
            }
        }

        public event EventHandler<string> DataReceived;

        public event EventHandler<string> ErrorOccurred;

        public SerialPortManager(
            string portName,
            int baudRate = 115200,
            Parity parity = Parity.None,
            int dataBits = 8,
            StopBits stopBits = StopBits.One
        )
        {
            _serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);

            _serialPort.Encoding = Encoding.UTF8;
            _serialPort.Handshake = Handshake.None;

            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        public void Connect()
        {
            if (IsConnected)
            {
                return;
            }

            try
            {
                _serialPort.Open();
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, ex.Message);
                throw;
            }
        }

        public void Disconnect()
        {
            if (IsConnected == false)
            {
                return;
            }

            try
            {
                _serialPort.Close();
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, ex.Message);
            }
        }

        public void Send(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return;
            }

            if (IsConnected == false)
            {
                throw new InvalidOperationException("Serial Port is not connected.");
            }

            _serialPort.Write(data);
        }

        public void ClearReceivedData()
        {
            if (IsConnected == false)
            {
                return;
            }

            _serialPort.DiscardInBuffer();
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                string data = _serialPort.ReadExisting();

                if (string.IsNullOrEmpty(data) == false)
                {
                    DataReceived?.Invoke(this, data);
                }
            }
            catch (Exception ex)
            {
                ErrorOccurred?.Invoke(this, ex.Message);
            }
        }

        public void Dispose()
        {
            try
            {
                if (_serialPort != null)
                {
                    _serialPort.DataReceived -= SerialPort_DataReceived;

                    if (_serialPort.IsOpen)
                    {
                        _serialPort.Close();
                    }

                    _serialPort.Dispose();
                }
            }
            catch
            {
            }
        }
    }
}
