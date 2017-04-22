using CGit.Models;
using CGit.Src.Dao;
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
        RepositoryDao repositoryDao = new RepositoryDao();
        // GET: Project
        public ActionResult projectFileModify()
        {
            return View();
        }
        /// <summary>
        /// 查找项目文件
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult projectFiles(string id)
        {
            if (id != null)
            {
                Repository repository = repositoryDao.findRepositoryById(id);
                Src.Dao.UserDao userDao = new Src.Dao.UserDao();
                User user = userDao.findUserByEmail(repository.email);//查找用户的信息
                user.pwd = "";
                ViewData["user"] = user;
                ViewData["repository"] = repository;
     
            }
            else
            {
                return RedirectToAction("frame", "User");
            }
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