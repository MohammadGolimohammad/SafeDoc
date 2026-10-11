namespace SafeDoc.Models
{
    public sealed class SafeDocDeviceUser
    {
        public int UserId
        {
            get;
            set;
        }

        public string UserName
        {
            get;
            set;
        }

        public string Pass
        {
            get;
            set;
        }

        public long Expire
        {
            get;
            set;
        }

        public bool IsUseFlash
        {
            get;
            set;
        }

        public int UseUsbFlashCount
        {
            get;
            set;
        }

        public int UsbUsedTimes
        {
            get;
            set;
        }
    }
}
