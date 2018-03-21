using plan2plan.Domain.Core;
using plan2plan.Domain.Core.ModelView;
using plan2plan.Domain.Interfaces;
using plan2plan.Infrastructure.Data.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Convert
{
    public class StatisticsWorker
    {
        public Statistics statistic;
        private IStatisticsRepository statisticsRepository;
        public StatisticsWorker(IStatisticsRepository statisticsRepository, Person_info person_Info, string sessionID)
        {
            this.statisticsRepository = statisticsRepository;
            statistic = new Statistics();

            statistic.SessionID = sessionID;
            statistic.dateTime = DateTime.Now;
            statistic.Browser_name = person_Info.browser_name;
            statistic.Browser_version = person_Info.browser_version;
            statistic.City = person_Info.city;
            statistic.Country = person_Info.country;
            statistic.Index = person_Info.index;
            statistic.IP = person_Info.ip;
            statistic.isMobile = person_Info.is_mobile;
            statistic.Location = person_Info.location;
            statistic.OS_name = person_Info.os_name;
            statistic.OS_version = person_Info.os_version;
            statistic.Referrer = person_Info.referrer;
            statistic.Screen_size = person_Info.screen_size;

            if (statisticsRepository.GetStatisticsByIP(person_Info.ip) != null)// db.Statistics.Where(x => x.IP == person_Info.ip).Any())
            {
                statistic.Status = "Return";
            }
            else
            {
                statistic.Status = "New";
            }
        }
    }
}
