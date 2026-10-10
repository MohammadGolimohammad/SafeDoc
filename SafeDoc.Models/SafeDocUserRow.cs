namespace SafeDoc.Models
{
    public sealed class SafeDocUserRow
    {
        public int? UserId
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

        public string ExpireDate
        {
            get;
            set;
        }

        public string ExpireTime
        {
            get;
            set;
        }

        public string FingerprintStatus
        {
            get;
            set;
        }

        public bool FlashPermission
        {
            get;
            set;
        }

        public int UsbUseCount
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
