using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core
{
    public class UserSession
    {
        public int ID { get; set; }
        [Required]
        public int UserID { get; set; }
        [Required]
        public string SessionID { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Finish { get; set; }

        public virtual User User { get; set; }
    }
}
