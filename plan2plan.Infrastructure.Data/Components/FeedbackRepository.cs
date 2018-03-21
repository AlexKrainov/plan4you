using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using plan2plan.Domain.Core;
using plan2plan.Domain.Interfaces;

namespace plan2plan.Infrastructure.Data.Components
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private plat2platContext context;

        public FeedbackRepository(plat2platContext context)
        {
            this.context = context;
        }
        public void Create(Feedback feedback)
        {
            context.Feedbacks.Add(feedback);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Feedback GetFeedbackByID(int id)
        {
            return context.Feedbacks.FirstOrDefault(x => x.ID == id);
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }

        public void Update(Feedback feedback)
        {
            throw new NotImplementedException();
        }
    }
}
