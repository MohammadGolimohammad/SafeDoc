using System.Collections.Generic;

namespace SafeDoc.Models
{
    public sealed class SafeDocCommand
    {
        public SafeDocCommandType commandType
        {
            get; set;
        }
        public List<object> requiredData
        {
            get; set;
        } = new List<object>();
    }
}
