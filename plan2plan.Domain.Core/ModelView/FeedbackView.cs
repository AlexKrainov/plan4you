using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core.ModelView
{
    public class FeedbackView
    {
        public string name { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string message { get; set; }
    }
}
