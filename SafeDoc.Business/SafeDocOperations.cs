using System;
using System.Collections.Generic;
using SafeDoc.Models;

namespace SafeDoc.Business
{
    public sealed class SafeDocOperations
    {
        private readonly DeviceConnectionService _connection;
        private readonly int _commandTimeoutMilliseconds;
        private readonly int _settingsTimeoutMilliseconds;
        private readonly int _fingerprintEnrollTimeoutMilliseconds;

        public SafeDocOperations(
            DeviceConnectionService connection,
            int commandTimeoutMilliseconds,
            int settingsTimeoutMilliseconds,
            int fingerprintEnrollTimeoutMilliseconds
        )
        {
            if (connection == null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            _connection = connection;
            _commandTimeoutMilliseconds = commandTimeoutMilliseconds;
            _settingsTimeoutMilliseconds = settingsTimeoutMilliseconds;
            _fingerprintEnrollTimeoutMilliseconds = fingerprintEnrollTimeoutMilliseconds;
        }

        public SafeDocResponse EnrollFingerprint(string userId)
        {
            return Send(SafeDocCommandType.FingerEnroll, User(userId));
        }

        public SafeDocResponse DeleteFingerprint(string userId)
        {
            return Send(SafeDocCommandType.FingerDeleteOne, User(userId));
        }

        public SafeDocResponse DeleteAllFingerprints()
        {
            return Send(SafeDocCommandType.FingerDeleteAll);
        }

        public SafeDocResponse AddPassword(string userId, string name, string value)
        {
            return Send(
                SafeDocCommandType.PasswordAdd,
                User(userId),
                new { passName = name },
                new { passValue = value }
            );
        }

        public SafeDocResponse DeletePassword(string userId, string name)
        {
            return Send(
                SafeDocCommandType.PasswordDeleteOne,
                User(userId),
                new { passName = name }
            );
        }

        public SafeDocResponse DeleteAllPasswords(string userId)
        {
            return Send(SafeDocCommandType.PasswordDeleteAll, User(userId));
        }

        public SafeDocResponse SetExpiration(string userId, long timestamp)
        {
            return Send(
                SafeDocCommandType.SetExpireDateTime,
                User(userId),
                new { expirationTimestamp = timestamp }
            );
        }

        public SafeDocResponse SetDeviceDateTime(string date, string time)
        {
            return Send(
                SafeDocCommandType.SetTimeDate,
                new { time = time },
                new { date = date }
            );
        }

        public SafeDocResponse GetDeviceDateTime()
        {
            return Send(SafeDocCommandType.GetTimeDate);
        }

        public SafeDocResponse GetDeviceDateTime(out SafeDocDeviceSettings settings)
        {
            return GetAllSettings(out settings);
        }

        public SafeDocResponse GetPasswordNames(string userId)
        {
            return Send(SafeDocCommandType.GetAllPassName, User(userId));
        }

        public SafeDocResponse GetPasswords(string userId)
        {
            return Send(SafeDocCommandType.GetAllPassword, User(userId));
        }

        public IList<PasswordInfo> GetPasswordList(string userId)
        {
            SafeDocResponse namesResponse = GetPasswordNames(userId);
            if (namesResponse.isSuccess == false)
            {
                throw new InvalidOperationException(
                    "خواندن نام رمزها ناموفق بود. کد: " + namesResponse.responseStatusCode
                );
            }

            SafeDocResponse valuesResponse = GetPasswords(userId);
            if (valuesResponse.isSuccess == false)
            {
                throw new InvalidOperationException(
                    "خواندن مقدار رمزها ناموفق بود. کد: " + valuesResponse.responseStatusCode
                );
            }

            List<object> namesData = namesResponse.resultData ?? new List<object>();
            List<object> valuesData = valuesResponse.resultData ?? new List<object>();
            if (namesData.Count != valuesData.Count)
            {
                throw new InvalidOperationException("تعداد نام‌ها و مقدارهای رمز یکسان نیست.");
            }

            List<PasswordInfo> passwords = new List<PasswordInfo>();
            for (int index = 0; index < namesData.Count; index++)
            {
                string name = namesData[index] == null ? string.Empty : namesData[index].ToString();
                string value = valuesData[index] == null ? string.Empty : valuesData[index].ToString();
                passwords.Add(new PasswordInfo { Name = name, Value = value });
            }

            return passwords;
        }

        public SafeDocResponse GetExpiration(string userId)
        {
            return Send(SafeDocCommandType.GetExpireDateTime, User(userId));
        }

        public SafeDocResponse GetAllUsers()
        {
            return Send(SafeDocCommandType.GetAllUsers);
        }

        public SafeDocResponse GetAllUsers(out List<SafeDocDeviceUser> users)
        {
            SafeDocResponse response = GetAllUsers();
            users = _connection.ReadResponseList<SafeDocDeviceUser>(response);
            return response;
        }

        public SafeDocResponse GetUser(string userId)
        {
            return Send(SafeDocCommandType.GetUser, User(userId));
        }

        public SafeDocResponse SavePerson(
            string userId,
            string userName,
            string password,
            long expirationTimestamp,
            bool useUsbFlash,
            int usbUseCount
        )
        {
            return Send(
                SafeDocCommandType.SavePerson,
                User(userId),
                new { passName = userName },
                new { passValue = password },
                new { expirationTimestamp = expirationTimestamp },
                new { useUsbFlash = useUsbFlash },
                new { usbUseCount = usbUseCount }
            );
        }

        public SafeDocResponse GetAllSettings()
        {
            return Send(
                SafeDocCommandType.GetAllSettings,
                _settingsTimeoutMilliseconds
            );
        }

        public SafeDocResponse GetAllSettings(out SafeDocDeviceSettings settings)
        {
            SafeDocResponse response = GetAllSettings();
            settings = _connection.ReadResponseData<SafeDocDeviceSettings>(response);
            return response;
        }

        public SafeDocResponse CheckToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentException("توکن نمی‌تواند خالی باشد.", nameof(token));
            }

            return Send(SafeDocCommandType.CheckToken, new { token = token.Trim() });
        }

        public SafeDocResponse SetEnterStatus(bool enabled)
        {
            return Send(SafeDocCommandType.SetEnterStatusInHID, new { enterStatus = enabled });
        }

        public SafeDocResponse GetEnterStatus()
        {
            return Send(SafeDocCommandType.GetEnterStatusInHID);
        }

        public SafeDocResponse SetUsbTimeout(int minutes)
        {
            return Send(
                SafeDocCommandType.SetUsbConnectionTimeout,
                new { usbConnectionTimeoutValue = minutes }
            );
        }

        public SafeDocResponse GetUsbTimeout()
        {
            return Send(SafeDocCommandType.GetUsbConnectionTimeout);
        }

        public SafeDocResponse SetHidAfterExpiration(bool enabled)
        {
            return Send(
                SafeDocCommandType.SetHidStatusAfterExpiration,
                new { HidStatusAfterExpiration = enabled }
            );
        }

        public SafeDocResponse GetHidAfterExpiration()
        {
            return Send(SafeDocCommandType.GetHidStatusAfterExpiration);
        }

        public SafeDocResponse SetBuzzerStatus(bool enabled)
        {
            return Send(
                SafeDocCommandType.SetBuzzerStatus,
                new { buzzerStatus = enabled }
            );
        }

        public SafeDocResponse GetBuzzerStatus()
        {
            return Send(SafeDocCommandType.GetBuzzerStatus);
        }

        private SafeDocResponse Send(SafeDocCommandType type, params object[] data)
        {
            return Send(type, _commandTimeoutMilliseconds, data);
        }

        private SafeDocResponse Send(SafeDocCommandType type, int timeoutMilliseconds, params object[] data)
        {
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data));
            }

            SafeDocCommand command = new SafeDocCommand();
            command.commandType = type;
            command.requiredData = new List<object>(data);

            if (type == SafeDocCommandType.FingerEnroll)
            {
                timeoutMilliseconds = _fingerprintEnrollTimeoutMilliseconds;
            }

            return _connection.SendCommand(command, timeoutMilliseconds);
        }

        private static object User(string userId)
        {
            return new { userId = userId };
        }
    }
}
