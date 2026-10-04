using System.Collections.Generic;

namespace SafeDoc.Models
{
    public sealed class SafeDocResponse
    {
        public bool isSuccess
        {
            get; set;
        }
        public int responseStatusCode
        {
            get; set;
        }
        public List<object> resultData
        {
            get; set;
        } = new List<object>();
    }
}
