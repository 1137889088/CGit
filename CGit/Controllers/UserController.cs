using CGit.Models;
using CGit.Src.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGit.Controllers
{
    public class UserController : Controller
    {

        Src.Dao.UserDao userDao = new Src.Dao.UserDao();
        RepositoryDao repositoryDao = new RepositoryDao();
        public ActionResult frame()
        {
            return View();
        }

        public ActionResult userHome()
        {
            string email = Request["email"];
            if (email == null || email.Equals(""))
            {
                User user = (User)Session["loginUser"];
                email = user.email;
            }
            List<Repository> repositoryList = repositoryDao.findAllRepositoryByEmail(email);
            ViewData["repositoryList"] = repositoryList;
            return View("userHome");
        }

        public ActionResult myFollow()
        {
            return View();
        }

        public ActionResult singOut()
        {
            Session.Remove("loginUser");
            return RedirectToAction("login","Home");
        }
        /// <summary>
        /// 热门项目视图
        /// </summary>
        /// <returns></returns>
        public ActionResult hotProject()
        {
            return View();
        }
        /// <summary>
        /// 检查密码
        /// </summary>
        /// <returns></returns>
        public ActionResult checkPwd()
        {
            string oldPwd = Request["oldPwd"];
            User user = (User)Session["loginUser"];
            if (user.pwd.Equals(oldPwd)){//密码检测
                return Content("ture");
            }

            return Content("false"); ;
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <returns></returns>
        public ActionResult modifyPwd()
        {
            string oldPwd = Request["oldPwd"];
            string newPwd = Request["newPwd"];
            User user = (User)Session["loginUser"];
            if (user.pwd.Equals(oldPwd))//判断密码相同
            {
                user.pwd = newPwd;
                userDao.updateUser(user);
                HttpContext.Session["loginUser"] = user;
                return Content("更改密码成功");
            }

            return Content("更改密码失败"); ;
        }
        /// <summary>
        /// 图片上传
        /// </summary>
        /// <returns></returns>
        public ActionResult imgUpload()
        { 
            User user = (User)Session["loginUser"];
            string email = user.email;
            string imgData = Request["imgData"];
            imgData = imgData.Replace(' ', '+');//解决base64中'+' 被空格取代的问题
            string strPath = Server.MapPath("/");//取得项目路径
            Src.Util.ImgUtil.Base64StringToImage(imgData, strPath + "/img/user/" + email, Src.Util.ImgUtil.TYPE_PNG);
           // Src.Util.ImgUtil.saveImg(strPath+"/img/user/" + email+".png", imgData);
            return Content("头像上传成功"); 
        }
       
        public ActionResult addRepository(Repository repository)
        {
            if (repository != null)
            {
                repository.email = ((User)Session["loginUser"]).email;
                repositoryDao.save(repository);
            }
            return userHome();
        }
    }
}