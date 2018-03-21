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

        public StatisticsRepository(plat2platContext _context)
        {
            this.context = _context;
        }
        public void Create(Statistics statistics)
        {
            context.Statistics.Add(statistics);
        }

        public void Delete(Statistics statistics)
        {
            throw new NotImplementedException();
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
