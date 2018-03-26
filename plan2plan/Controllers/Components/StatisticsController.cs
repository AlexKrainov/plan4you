using plan2plan.Domain.Core.ModelView;
using plan2plan.Domain.Interfaces;
using plan2plan.Infrastructure.Data.Convert;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Controllers.Components
{
    [Authorize]
    public class StatisticsController : Controller
    {
        private IStatisticsRepository statisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            this.statisticsRepository = statisticsRepository;
        }

        public ActionResult Index()
        {
            return View(statisticsRepository.GetAllStatistics().OrderByDescending(x => x.ID));//
        }

        public ActionResult Delete(int id)
        {
            if (id > 0)
            {
                statisticsRepository.Delete(id);
                statisticsRepository.Save();
            }
            return RedirectToAction("Index");
        }

        /// <summary>
        /// Сохраняем всю информацию о пользователе и о действиях которые он совершает на странице
        /// Пока сохраняем только первый вход
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<JsonResult> SaveInfo(PersonViewModel id)
        {
            StatisticsWorker convertStatistics = new StatisticsWorker(statisticsRepository, id, Session.SessionID);
            statisticsRepository.Create(convertStatistics.statistic);
            try
            {
                await statisticsRepository.Save();
            }
            catch (Exception ex)
            {
                return Json(new
                {
                    is_ok = false,
                    error_message = ex.Message
                });
            }
            return Json(new
            {
                is_ok = true,
                session_id = Session.SessionID,
                ip = convertStatistics.statistic.IP
            });
        }
    }
}