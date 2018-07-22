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
using System.Net;
using plan2plan.Filter.Auth;
using Serilog;

namespace plan2plan.Controllers
{
    using File = plan2plan.Domain.Core.File;

    [MyAuthorize(Roles = "admin")]
    public class FileController : Controller
    {
        private IFileRepository fileRepository;
        private IActionRepository actionRepository;
        private IEmailRepository emailRepository;

        public FileController(IFileRepository fileRepository,
            IActionRepository actionRepository,
            IEmailRepository emailRepository)
        {
            this.fileRepository = fileRepository;
            this.actionRepository = actionRepository;
            this.emailRepository = emailRepository;
        }

        #region Base action create/read/update/delete
        public ActionResult Index()
        {
            return View(fileRepository.GetAllFiles());
        }
        // GET: Files/Details/5
        public ActionResult Details(Guid id)
        {
            plan2plan.Domain.Core.File file = fileRepository.GetFileByID(id);

            if (file == null)
            {
                Log.Warning("Не нашелся файл с id: {id}", id);
                return HttpNotFound();
            }
            return View(file);
        }

        // GET: Files/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Description,FileName,Path,PreviewPath_min,PreviewPath_avg,PreviewPath_max,isDelete,isShow,isExist")]
        File file, HttpPostedFileBase[] main_file)
        {
            if (ModelState.IsValid)
            {
                #region Сохраняем файл на сервер
                if (main_file != null)
                {
                    try
                    {
                        var fileInput = main_file[0];
                        var path = Server.MapPath("../Source/" + fileInput.FileName);
                        using (var stream = new FileStream(path, FileMode.Create))
                        {
                            fileInput.InputStream.CopyTo(stream);
                            file.Path = "/Source/" + fileInput.FileName;
                        }
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Ошибка при создании файла");
                        throw ex;
                    }
                }
                #endregion

                fileRepository.Create(file);
                fileRepository.Save();

                if (main_file != null)
                {
                    new ImageGenerator(fileRepository, Server).UpdateFiles();
                }

                return RedirectToAction("Index");
            }

            return View(file);
        }

        // GET: Files/Edit/5
        public ActionResult Edit(Guid id)
        {
            plan2plan.Domain.Core.File file = fileRepository.GetFileByID(id);
            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,Title,Description,FileName,Path,PreviewPath_min,PreviewPath_avg,PreviewPath_max,isDelete,isShow,isExist")] File file)
        {
            if (ModelState.IsValid)
            {
                fileRepository.Update(file);
                fileRepository.Save();
                return RedirectToAction("Index");
            }
            return View(file);
        }

        // GET: Files/Delete/5
        public ActionResult Delete(Guid id)
        {
            File file = fileRepository.GetFileByID(id);

            if (file == null)
            {
                return HttpNotFound();
            }
            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            string path = fileRepository.Delete(id);
            fileRepository.Save();

            RemoveFile(path);

            return RedirectToAction("Index");
        }

        private void RemoveFile(string path)
        {
            var serverPath = Server.MapPath("../" + path);
            var fileInfo = new FileInfo(serverPath);

            try
            {

                if (fileInfo.Exists == true)
                {
                    fileInfo.Delete();
                }
            }
            catch (Exception ex)
            {
                Log.Error("Remove file, error: " + ex.Message);
            }
        }
        #endregion

        /// <summary>
        /// Запускает генератор превью файлов .jpg по pdf-файлу
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult PreviewPdfGenerator()
        {
            new ImageGenerator(fileRepository, Server).UpdateFiles();

            return RedirectToAction("Index", "File");
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult> DownloadFile(string id, bool isLoad, string mail)
        {
            Guid result;
            if (Guid.TryParse(id, out result) == true && string.IsNullOrEmpty(mail) == false)
            {
                var file = await fileRepository.GetFileByIDTask(result);

                if (ExistsFile(file))
                {
                    if (isLoad == false)
                    {
                        //http://kino-hd1080.ru/998-nevidimyy-gost-2016-smotret-onlayn.html

                        var action = new Domain.Core.Action
                        {
                            dateTime = DateTime.Now,
                            email = emailRepository.GetOrCreateEmail(mail, Request.UserHostAddress),
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
                            Log.Error(ex, "Ошибка при асинхронной загрузки файла");
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