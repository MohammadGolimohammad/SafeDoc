using System;
using System.Configuration;

namespace SafeDoc.UI
{
    internal static class DeviceConfiguration
    {
        internal const string ApprovedVendorId = "VID_CAFE";
        internal const string ApprovedProductId = "PID_4014";

        internal static int BaudRate
        {
            get
            {
                return ReadNumber("DeviceBaudRate");
            }
        }

        internal static int DeviceReadyDelayMilliseconds
        {
            get
            {
                return ReadNumber("DeviceReadyDelayMilliseconds");
            }
        }

        internal static int CommandIntervalMilliseconds
        {
            get
            {
                return ReadNumber("CommandIntervalMilliseconds");
            }
        }

        internal static int CommandTimeoutMilliseconds
        {
            get
            {
                return ReadNumber("CommandTimeoutMilliseconds");
            }
        }

        internal static int SettingsTimeoutMilliseconds
        {
            get
            {
                return ReadNumber("SettingsTimeoutMilliseconds");
            }
        }

        internal static int FingerprintEnrollTimeoutMilliseconds
        {
            get
            {
                return ReadNumber("FingerprintEnrollTimeoutMilliseconds");
            }
        }

        internal static int DefaultUsbUseCount
        {
            get
            {
                return ReadNumber("DefaultUsbUseCount");
            }
        }

        private static string ReadText(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ConfigurationErrorsException("مقدار " + key + " در App.config ثبت نشده است.");
            }

            return value.Trim();
        }

        private static int ReadNumber(string key)
        {
            string value = ReadText(key);
            int number;
            if (int.TryParse(value, out number) == false || number < 0)
            {
                throw new ConfigurationErrorsException("مقدار " + key + " در App.config معتبر نیست.");
            }

            return number;
        }
    }
}
