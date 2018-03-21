using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core
{
   public class Email
    {
        public int ID { get; set; }
        [Required]
        public string Mail { get; set; }
    }
}
