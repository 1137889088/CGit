using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace CGit.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        public ActionResult projectFileModify()
        {
            return View();
        }
        public ActionResult projectFiles()
        {
            return View();
        }
        public ActionResult projectFileContent()
        {
            return View();
        }
        public ActionResult projectComment()
        {
            return View();
        }
    }
}