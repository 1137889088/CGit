using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGit.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult login()
        {

            return View();
        }

        public ActionResult register()
        {
            return View();
        }

        public ActionResult findPwd()
        {
            return View();
        }

        public ActionResult doLogin()
        {
            return RedirectToAction("frame", "User");
        }

        public ActionResult doFindPwd()
        {
            return RedirectToAction("frame", "User");
        }
    }
}