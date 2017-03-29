using CGit.Models;
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
        public ActionResult doRegeist(User user)
        {
            Src.Dao.UserDao dao = new Src.Dao.UserDao();
            if (dao.findUserByEmail(user.email) != null)
            {
                ViewData["msg"] = "注册失败";
                ViewData["result"] = "对不起，这个邮箱账号已经被注册";
            }
            else
            {
                if (dao.save(user) > 0)
                {
                    ViewData["msg"] = "注册成功";
                    ViewData["result"] = "欢迎你，您已成功注册<strong> CGit</ strong > 账号";
                }
                else
                {
                    ViewData["msg"] = "注册失败";
                    ViewData["result"] = "对不起，由于服务器的原因可能注册失败";
                }
            }
            return View("registerResult");
        }
        public ActionResult doLogin()
        {
            string email = Request["email"];
            string pwd = Request["pwd"];
            Src.Dao.UserDao dao = new Src.Dao.UserDao();
            User user = dao.login(email, pwd);
            if (user != null)
            {
                user.pwd = "";
                HttpContext.Session["loginUser"] = user;
                return RedirectToAction("frame", "User");
            }
            else
            {
                ViewData["msg"] = "登录失败用户名或密码错误";
                return View("login");
            }
        }

        public ActionResult doFindPwd()
        {
            return RedirectToAction("frame", "User");
        }
    }
}