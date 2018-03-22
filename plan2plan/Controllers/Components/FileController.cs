using plan2plan.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using plan2plan.Infrastructure.Business.Extentions;
using plan2plan.Domain.Core;
using System.Threading.Tasks;
using plan2plan.Common.Image;

namespace plan2plan.Controllers
{
    public class FileController : Controller
    {
        private IFileRepository fileRepository;
        private IActionRepository actionRepository;

        public FileController(IFileRepository fileRepository, IActionRepository actionRepository)
        {
            this.fileRepository = fileRepository;
            this.actionRepository = actionRepository;
        }

        [HttpGet]
        public ActionResult PreviewPdfGenerator()
        {
            new ImageGenerator(fileRepository, Server).UpdateFiles();

            return RedirectToAction("Index", "Home");
        }
        [HttpGet]
        public async Task<ActionResult> DownloadFile(string id, bool isLoad, string mail)
        {
            if (Guid.TryParse(id, out Guid result) == true && string.IsNullOrEmpty(mail) == false)
            {
                var file = await fileRepository.GetFileByID(result);

                if (ExistsFile(file))
                {
                    if (isLoad == false)
                    {

                        var action = new Domain.Core.Action
                        {
                            dateTime = DateTime.Now,
                            email = new Email { Mail = mail },
                            IP = Request.UserHostAddress,
                            isDownload = true,
                            FileID = result
                        };

                        actionRepository.Create(action);

                        try
                        {
                            await actionRepository.SaveAsync();

                        }
                        catch (Exception ex)
                        {

                            return Json(new { is_ok = false, error_message = ex.Message }, JsonRequestBehavior.AllowGet);
                        }
                        return Json(new { is_ok = true }, JsonRequestBehavior.AllowGet);
                    }

                    var fileInfo = new FileInfo(Server.MapPath(file.Path));
                    byte[] fileBytes = System.IO.File.ReadAllBytes(fileInfo.FullName);

                    return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, file.FileName + fileInfo.Extension);
                }
            }
            return Json(new { is_ok = false, error_message = "Ошибка при получении ID файла или электронной почты." }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult Test(int fileid)
        {
            if (fileid == 1)
            {
                return Json(54, JsonRequestBehavior.AllowGet);
            }
            else if (fileid == 2)
            {
                return Json(26, JsonRequestBehavior.AllowGet);
            }
            return Json(0, JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Проверяет существует ли файл в заданной директории
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        protected bool ExistsFile(plan2plan.Domain.Core.File file)
        {
            var path = Server.MapPath("../" + file.Path);
            var fileInfo = new FileInfo(path);

            fileRepository.SetExistFile(file, fileInfo.Exists);

            return fileInfo.Exists;
        }
    }
}