using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core.ModelView
{
    public class Person_info
    {
        public string referrer { get; set; }
        public string ip { get; set; }
        public string city { get; set; }
        public string country { get; set; }
        public string location { get; set; }
        public string index { get; set; }
        public string browser_name { get; set; }
        public string browser_version { get; set; }
        public string os_name { get; set; }
        public string os_version { get; set; }
        public string screen_size { get; set; }
        public string status { get; set; }
        public bool is_mobile { get; set; }
    }
}
