namespace plan2plan.Domain.Core
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Feedback
    {
        public int ID { get; set; }
        public string Name { get; set; }
        [Required]
        public int EmailID { get; set; }
        public string Message { get; set; }
        public Nullable<System.DateTime> DateTime { get; set; }

        public virtual Email Email { get; set; }
    }
}
