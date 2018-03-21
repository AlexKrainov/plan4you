using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace plan2plan.Controllers.Components
{
    public class PersonController : Controller
    {
        // GET: Person
        public ActionResult GetStatistics()
        {
            return View();
        }
    }
}