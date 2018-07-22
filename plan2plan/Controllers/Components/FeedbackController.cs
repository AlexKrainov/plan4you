using plan2plan.Domain.Core.ModelView;
using plan2plan.Domain.Interfaces;
using plan2plan.Infrastructure.Data.Convert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Controllers
{
    public class FeedbackController : Controller
    {
        private IFeedbackRepository feedbackRepository;
        private IEmailRepository emailRepository;

        public FeedbackController(IFeedbackRepository feedbackRepository,
            IEmailRepository emailRepository)
        {
            this.feedbackRepository = feedbackRepository;
            this.emailRepository = emailRepository;
        }

        [HttpPost]
        public async Task<ActionResult> Create(FeedbackViewModel feedbackView)
        {
            if (this.ModelState.IsValid == true)
            {
                FeedbackWorker feedbackWorker = new FeedbackWorker(feedbackView);
                var newFeedback = feedbackWorker.GetModelFeedback(emailRepository, Request.UserHostAddress);

                feedbackRepository.Create(newFeedback);

                try
                {
                    await feedbackRepository.Save();

                    ViewBag.Message = "Ваше сообщение отправлено. Спасибо!";
                    return PartialView();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Извините, не удалось отправить сообщение. Ошибка при сохранении.";
                    return PartialView();
                }
            }

            ViewBag.Message = "Извините, не удалось отправить сообщение. Так как не все поля заполнены корректно.";
            return PartialView();
        }
    }
}