using plan2plan.Domain.Core.ModelView;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Controllers.User
{
    public class AuthController : Controller
    {
        // GET: Auth
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult LogIn(UserViewModel user)
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}