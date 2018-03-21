using plan2plan.Domain.Core;
using plan2plan.Domain.Core.ModelView;
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

        public Feedback GetModelFeedback()
        {
            return new Feedback
            {
                DateTime = DateTime.Now,
                Email = new Email
                {
                    Mail = feedbackView.email
                },
                Message = feedbackView.message,
                Name = feedbackView.name
            };
        }
    }
}
