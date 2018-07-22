using plan2plan.Domain.Core;
using plan2plan.Domain.Core.ModelView;
using plan2plan.Domain.Interfaces;
using plan2plan.Filter.Auth;
using plan2plan.Infrastructure.Data.Convert;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace plan2plan.Controllers.Components
{
    [MyAuthorize(Roles = "admin")]
    public class StatisticsController : Controller
    {
        private IStatisticsRepository statisticsRepository;
        private IActionRepository actionRepository;
        private IEmailRepository emailRepository;

        public StatisticsController(IStatisticsRepository statisticsRepository,
            IActionRepository actionRepository,
            IEmailRepository emailRepository)
        {
            this.statisticsRepository = statisticsRepository;
            this.actionRepository = actionRepository;
            this.emailRepository = emailRepository;
        }

        public ActionResult List()
        {
            return View();
        }

        [HttpPost]
        public JsonResult GetTableData()
        {
            var data = statisticsRepository.GetAllStatistics().OrderByDescending(x => x.ID);

            return Json(new { data = data });
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

        [HttpPost]
        public ActionResult GetInfo(string ip)
        {
            ip = ip.Trim();
            if (string.IsNullOrEmpty(ip) == false)
            {
                StatisticsInfoIPViewModel model = new StatisticsInfoIPViewModel
                {
                    CountCome = statisticsRepository.GetAllStatistics().Count(x => x.IP == ip),
                    CountLike = actionRepository.GetCountLikeByIP(ip),
                    CountDownload = actionRepository.GetCountDownloadsByIP(ip),
                    email = emailRepository.GetEmailByIP(ip),
                    statistics = statisticsRepository.GetStatisticsByIP(ip)
                };

                return PartialView("_InfoPartial", model);
            }

            return Json(false);
        }

        public ActionResult GeoChart()
        {
            var dic = new List<DictionaryRegion>();
            dic.Add(new DictionaryRegion { Key = "Московская область", SearcOptions = new List<string> { "МО", "Московская область" } });
            dic.Add(new DictionaryRegion { Key = "Москва", SearcOptions = new List<string> { "Москва" } });
            dic.Add(new DictionaryRegion { Key = "Краснодарский край", SearcOptions = new List<string> { "Краснодарский край" } });
            dic.Add(new DictionaryRegion { Key = "Татарстан", SearcOptions = new List<string> { "Татарстан" } });
            dic.Add(new DictionaryRegion { Key = "Санкт-Петербург", SearcOptions = new List<string> { "Санкт-Петербург" } });
            dic.Add(new DictionaryRegion { Key = "Курская область", SearcOptions = new List<string> { "Курская область" } });
            dic.Add(new DictionaryRegion { Key = "Массачусетс", SearcOptions = new List<string> { "Массачусетс" } });
            dic.Add(new DictionaryRegion { Key = "Нижегородская область", SearcOptions = new List<string> { "Нижегородская область" } });
            dic.Add(new DictionaryRegion { Key = "California", SearcOptions = new List<string> { "California" } });
            dic.Add(new DictionaryRegion { Key = "Ростовская область", SearcOptions = new List<string> { "Ростовская область" } });
            dic.Add(new DictionaryRegion { Key = "Самарская область", SearcOptions = new List<string> { "Самарская область" } });
            dic.Add(new DictionaryRegion { Key = "Ямало-Ненецкий АО", SearcOptions = new List<string> { "Ямало-Ненецкий АО" } });
            dic.Add(new DictionaryRegion { Key = "Свердловская область", SearcOptions = new List<string> { "Свердловская область" } });

            for (int i = 0; i < dic.Count; i++)
            {
                dic[i].Value = statisticsRepository.GetAllVisitByNameRegion(dic[i].SearcOptions);
            }

            return View(dic);
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
            StatisticsWorker convertStatistics = new StatisticsWorker(statisticsRepository, id, Session.SessionID); //, HttpContext.Request.UrlReferrer);
            statisticsRepository.Create(convertStatistics.statistic);
            try
            {
                await statisticsRepository.Save();
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Ошибка при сохранении статистики");
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