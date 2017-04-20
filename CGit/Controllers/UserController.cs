using CGit.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CGit.Controllers
{
    public class UserController : Controller
    {

        Src.Dao.UserDao dao = new Src.Dao.UserDao();
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
            Session.Remove("loginUser");
            return RedirectToAction("login","Home");
        }

        public ActionResult hotProject()
        {
            return View();
        }

        public ActionResult checkPwd()
        {
            string oldPwd = Request["oldPwd"];
            User user = (User)Session["loginUser"];
            if (user.pwd.Equals(oldPwd)){
                return Content("ture");
            }

            return Content("false"); ;
        }

        public ActionResult modifyPwd()
        {
            string oldPwd = Request["oldPwd"];
            string newPwd = Request["newPwd"];
            User user = (User)Session["loginUser"];
            if (user.pwd.Equals(oldPwd))
            {
                user.pwd = newPwd;
                dao.updateUser(user);
                HttpContext.Session["loginUser"] = user;
                return Content("更改密码成功");
            }

            return Content("更改密码失败"); ;
        }
        public ActionResult imgUpload()
        { 
            User user = (User)Session["loginUser"];
            string email = user.email;
            string imgData = Request["imgData"];
            imgData = imgData.Replace(' ', '+');
            string strPath = Server.MapPath("/");
            Src.Util.ImgUtil.Base64StringToImage(imgData, strPath + "/img/user/" + email, Src.Util.ImgUtil.TYPE_PNG);
           // Src.Util.ImgUtil.saveImg(strPath+"/img/user/" + email+".png", imgData);
            return Content(imgData); 
        }
    }
}