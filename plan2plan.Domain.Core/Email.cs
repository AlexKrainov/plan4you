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

        public Email()
        {
            this.Letters = new HashSet<Letter>();
        }
        public int ID { get; set; }
        [Required]
        public string Mail { get; set; }
        /// <summary>
        /// Подтверждена почта ?
        /// </summary>
        public bool isEmailConfirmed { get; set; }
        /// <summary>
        /// Подписан на рассылку ?
        /// </summary>
        public bool subscribedToNewsletters { get; set; }
        public string IP { get; set; }

        public virtual IEnumerable<Letter> Letters { get; set; }
    }
}
