using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core
{
    public class Letter
    {
        public int ID { get; set; }
        public int EmailID { get; set; }
        public int LetterTypeID { get; set; }
        public DateTime Date { get; set; }

        public virtual Email email { get; set; }
        public virtual LetterType letterType { get; set; }
    }
}
