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
    public class StatisticsController : Controller
    {
        private IStatisticsRepository stattisticsRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository)
        {
            this.stattisticsRepository = statisticsRepository;
        }
        /// <summary>
        /// Сохраняем всю информацию о пользователе и о действиях которые он совершает на странице
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> SaveInfo(Person_info id)
        {
            StatisticsWorker convertStatistics = new StatisticsWorker(stattisticsRepository, id, Session.SessionID);
            stattisticsRepository.Create(convertStatistics.statistic);
            try
            {
                await stattisticsRepository.Save();
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