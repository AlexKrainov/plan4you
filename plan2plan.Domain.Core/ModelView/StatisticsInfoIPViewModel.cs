using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Core.ModelView
{
    public class StatisticsInfoIPViewModel
    {
        public StatisticsInfoIPViewModel()
        {
            this.email = new Email();
        }
        /// <summary>
        /// Сколько заходил
        /// </summary>
        public int CountCome { get; set; }
        public int CountLike { get; set; }
        public int CountDownload { get; set; }
        public Email email { get; set; }
        public Statistics statistics { get; set; }
    }
}
