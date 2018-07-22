using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Components
{
    public class StatisticsRepository : IStatisticsRepository
    {
        private plat2platContext context;
        private const string myIP = "95.26.36.8";

        public StatisticsRepository(plat2platContext _context)
        {
            this.context = _context;
        }
        public void Create(Statistics statistics)
        {
            context.Statistics.Add(statistics);
        }

        public void Delete(int id)
        {
            Statistics statistic = context.Statistics.Find(id);
            if (statistic != null)
            {
                context.Statistics.Remove(statistic);
            }
        }

        public IEnumerable<Statistics> GetAllStatistics()
        {
            return context.Statistics.Where(x => x.IP != myIP);
        }

        public int GetAllVisitByNameRegion(List<string> regions)
        {
            int count = 0;
            for (int i = 0; i < regions.Count(); i++)
            {
                string region = regions[i];
                count = context.Statistics.Count(x => x.City == region && x.IP != myIP ) + count;
            }

            return count;
        }

        public Statistics GetStatisticsByID(int id)
        {
            throw new NotImplementedException();
        }

        public Statistics GetStatisticsByIP(string ip)
        {
            return context.Statistics.FirstOrDefault(x => x.IP == ip);
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }

        public void Update(Statistics statistics)
        {
            throw new NotImplementedException();
        }
    }
}
