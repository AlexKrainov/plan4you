using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core.ModelView
{
    //    {
    //  "actoin": {
    //    "check_sheets": {
    //      "id": 1,
    //      "is_like": false,
    //      "likes": 34,
    //      "downloads": 34
    //    }
    //  }
    //}

    public class action
    {
        public List<check_sheet> check_sheets { get; set; }
        public action()
        {
            this.check_sheets = new List<check_sheet>();
        }
    }

    public class check_sheet
    {
        public string id { get; set; }
        public bool is_like { get; set; }
        public int likes { get; set; }
        public int downloads { get; set; }
    }
}
