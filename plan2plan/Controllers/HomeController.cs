using MailKit.Net.Smtp;
using MimeKit;
using plan2plan.Domain.Interfaces;
using Serilog;
using Serilog.Debugging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Controllers
{
    public class HomeController : Controller
    {
        private IFileRepository fileReposotory;

        public HomeController(IFileRepository file)
        {
            this.fileReposotory = file;
        }
        // GET: Home
        public ActionResult Index()
        {
            ViewBag.PreviewFiles = fileReposotory.GetFiles().OrderBy(x => x.Order).ToList();

            return View();
        }
   
        public ActionResult PreviewDocs()
        {
            return PartialView();
        }

        public ActionResult PreviewBox()
        {
            return PartialView();
        }

        public ActionResult PreviewFeedback()
        {
            return PartialView();
        }
        public ActionResult PreviewInstagram()
        {
            return View();
        }
    }
}