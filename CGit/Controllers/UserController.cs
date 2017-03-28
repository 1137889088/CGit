using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGit.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult frame()
        {
            return View();
        }
        public ActionResult userHome()
        {
            return View();
        }
        public ActionResult myFollow()
        {
            return View();
        }
        public ActionResult singOut()
        {
            return RedirectToAction("login","Home");
        }
        public ActionResult hotProject()
        {
            return View();
        }
    }
}