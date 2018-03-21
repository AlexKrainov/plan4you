using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        public int EmailID { get; set; }
        public int UserTypeID { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Password { get; set; }
        public string FirstName { get; set; }
        public DateTime dateTime { get; set; }

        public virtual Email Email { get; set; }
        public virtual UserType UserType { get; set; }
    }
}
