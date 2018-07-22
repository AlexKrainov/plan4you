using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core.ModelView
{
    public class UserCheckInViewModel : UserViewModel
    {
        public UserCheckInViewModel()
        {
        }

        public UserCheckInViewModel(UserViewModel user)
            : base(user)
        {
        }
        public string name { get; set; }
    }
}
