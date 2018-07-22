using plan2plan.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Interfaces
{
    public interface IStatisticsRepository
    {
        void Create(Statistics statistics);
        void Update(Statistics statistics);
        void Delete(int id);
        Task<int> Save();
        Statistics GetStatisticsByID(int id);
        Statistics GetStatisticsByIP(string ip);
        IEnumerable<Statistics> GetAllStatistics();
        int GetAllVisitByNameRegion(List<string> regions);
    }
}
