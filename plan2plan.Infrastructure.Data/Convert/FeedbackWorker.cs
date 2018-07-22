using plan2plan.Domain.Core;
using plan2plan.Domain.Core.ModelView;
using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace plan2plan.Infrastructure.Data.Convert
{
    public class FeedbackWorker
    {
        private FeedbackViewModel feedbackView;

        public FeedbackWorker(FeedbackViewModel feedbackView)
        {
            this.feedbackView = feedbackView;
        }

        public Feedback GetModelFeedback(IEmailRepository emailRepository, string ip)
        {
            return new Feedback
            {
                DateTime = DateTime.Now,
                Email = emailRepository.GetOrCreateEmail(feedbackView.email, ip),
                Message = feedbackView.message,
                Name = feedbackView.name
            };
        }
    }
}
