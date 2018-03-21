using plan2plan.Domain.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Domain.Interfaces
{
    public interface IFeedbackRepository
    {
        void Create(Feedback feedback);
        void Update(Feedback feedback);
        void Delete(int id);
        Feedback GetFeedbackByID(int id);
        Task<int> Save();
    }
}
