using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SafeDoc.Models
{
    public enum SafeDocCommandType
    {
        FingerEnroll = 1,
        FingerDeleteOne = 2,
        FingerDeleteAll = 3,

        PasswordAdd = 4,
        PasswordDeleteOne = 5,
        PasswordDeleteAll = 6,

        SetExpireDateTime = 7,

        SetTimeDate = 8,
        GetTimeDate = 9,

        GetAllPassName = 10,
        GetAllPassword = 11,

        GetExpireDateTime = 12,

        SetEnterStatusInHID = 13,
        GetEnterStatusInHID = 14,

        SetUsbConnectionTimeout = 15,
        GetUsbConnectionTimeout = 16,

        SetHidStatusAfterExpiration = 17,
        GetHidStatusAfterExpiration = 18,

        GetAllUsers = 19,
        GetUser = 20,

        SavePerson = 21,
        SetBuzzerStatus = 22,
        GetBuzzerStatus = 23,
        GetAllSettings = 24,
        CheckToken = 25,
        GetUserId = 26,
    }
}
