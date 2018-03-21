using plan2plan.Domain.Interfaces;
using plan2plan.Infrastructure.Business.Extentions;
using plan2plan.Infrastructure.Business.SystemLikes;
using plan2plan.Infrastructure.Data.Convert;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Controllers.Components
{
    public class ActionController : Controller
    {
        private IFileRepository fileRepository;
        private IActionRepository actionRepository;

        public ActionController(IFileRepository fileRepository, IActionRepository actionRepository)
        {
            this.fileRepository = fileRepository;
            this.actionRepository = actionRepository;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Get()
        {
            ActionWorker worker = new ActionWorker(actionRepository, fileRepository, Request.UserHostAddress);
            var action = await worker.GetActionCheckSheets(); //ToDo: await

            return Json(action, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public async Task<ActionResult> UpdateLike(string datatype, string id)
        {
            LikeWorker likeWorker = new LikeWorker(actionRepository, Request.UserHostAddress, datatype, id);

            if (await likeWorker.Update() == 1) //successfully
            {
                var fileID = Guid.Parse(id);
                var is_user_liked = actionRepository.IsLikeByIP(Request.UserHostAddress, fileID);
                var count_likes = actionRepository.GetCountLikesByFileID(fileID);

                return Json(new { is_ok = true, is_like = is_user_liked, likes = count_likes });
            }

            return Json(new { is_ok = false });
        }

        [HttpPost]
        public ActionResult GetCountDownload(string datatype, string id)
        {
            if (string.IsNullOrEmpty(datatype) == false
                && string.IsNullOrEmpty(id) == false
                && Guid.TryParse(id, out Guid result) == true)
            {
                if (datatype == "check_sheets")
                {
                    var count = actionRepository.GetCountDownloadsByFileID(result);

                    return Json(new { is_ok = true, downloads = count });
                }
            }

            return Json(new { is_ok = false });
        }
    }
}