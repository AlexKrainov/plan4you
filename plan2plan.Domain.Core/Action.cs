using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core
{
    public class Action
    {
        public int ID { get; set; }
        [Required]
        public string IP { get; set; }
        public Guid? FileID { get; set; }
        public int? EmailID { get; set; }
        public bool isDownload { get; set; }
        public bool isLike { get; set; }
        public DateTime dateTime { get; set; }


        public virtual File file { get; set; }
        public virtual Email email { get; set; }
    }
}
