using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core.ModelView
{
    public class UserViewModel
    {
        public UserViewModel()
        {

        }
        public UserViewModel(UserViewModel user)
        {
            this.email = user.email;
            this.pwd = user.pwd;
            this.remember = user.remember;
        }
        [Required]
        public string email { get; set; }
        [Required]
        public string pwd { get; set; }
        public bool remember { get; set; }
    }
}
