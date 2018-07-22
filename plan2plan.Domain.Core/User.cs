using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [Required]
        public int EmailID { get; set; }
        public int UserTypeID { get; set; }
        [Required]
        public string Password { get; set; }
        public string Name { get; set; }
        public DateTime dateTime { get; set; }
        public string SurName { get; set; }
        public string Phone { get; set; }
        public string City { get; set; }
        public string Address { get; set; }


        public virtual Email Email { get; set; }
        public virtual UserType UserType { get; set; }

    }
}
