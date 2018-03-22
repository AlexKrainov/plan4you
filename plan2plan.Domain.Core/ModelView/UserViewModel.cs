using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core.ModelView
{
    public class UserViewModel
    {
        public string login { get; set; }
        public string pwd { get; set; }
        public bool remember_me { get; set; }
    }
}
