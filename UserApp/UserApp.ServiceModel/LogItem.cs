using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserApp.ServiceModel
{
    public class LogItem
    {
        public User User { get; set; }
        
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime TimeStamp { get; set; }
        public string Action { get; set; }
        public string Url { get; set; }
        public string IpAddress { get; set; }
    }
}
