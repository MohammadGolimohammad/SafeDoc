namespace SafeDoc.Models
{
    public sealed class SafeDocDeviceSettings
    {
        public string Time
        {
            get;
            set;
        }

        public string Date
        {
            get;
            set;
        }

        public bool EnterStatus
        {
            get;
            set;
        }

        public int UsbConnectionTimeoutValue
        {
            get;
            set;
        }

        public bool HidStatusAfterExpiration
        {
            get;
            set;
        }

        public bool BuzzerStatus
        {
            get;
            set;
        }
    }
}
