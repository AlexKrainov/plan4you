namespace plan2plan.Domain.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Statistics
    {
        public int ID { get; set; }
        public string IP { get; set; }
        public string SessionID { get; set; }
        public string Browser_name { get; set; }
        public string Browser_version { get; set; }
        public string OS_name { get; set; }
        public string OS_version { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Status { get; set; }
        public bool isMobile { get; set; }
        public string Referrer { get; set; }
        public string FullReferrer { get; set; }
        public string Screen_size { get; set; }
        public string Index { get; set; }
        public string Location { get; set; }
        public System.DateTime dateTime { get; set; }
    }
}
