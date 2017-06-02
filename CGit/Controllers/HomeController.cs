using CGit.Models;
using CGit.Src.bll;
using CGit.Src.Dao;
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
            User user = (User)Session["loginUser"];
            if (user != null)
            {
                return RedirectToAction("frame", "User");
            }
            return View();
        }

        public ActionResult login()
        {
            User user = (User)Session["loginUser"];
            if (user != null)
            {
                return RedirectToAction("frame", "User");
            }
            return View();
        }

        public ActionResult register()
        {
            User user = (User)Session["loginUser"];
            if (user != null)//用户已经登录
            {
                return RedirectToAction("frame", "User");
            }
            return View();
        }

        public ActionResult findPwd()
        {
            return View();
        }

        public ActionResult doRegeist(User user)
        {
            User user1 = (User)Session["loginUser"];
            if (user1 != null)
            {
                return RedirectToAction("frame", "User");
            }
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
                    ViewData["result"] = "欢迎你，您已成功注册<strong> CGit</strong > 账号";
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
            User user1 = (User)Session["loginUser"];
            if (user1 != null)
            {
                return RedirectToAction("frame", "User");
            }
            string email = Request["email"];
            string pwd = Request["pwd"];
            Src.Dao.UserDao dao = new Src.Dao.UserDao();
            User user = dao.login(email, pwd);
            if (user != null)//用户存在登录成功
            {
                //user.pwd = "";
                HttpContext.Session["loginUser"] = user;
                return RedirectToAction("frame", "User");
            }
            else
            {
                ViewData["msg"] = "用户名或密码错误";
                return View("login");
            }
        }

        public ActionResult doFindPwd()
        {
            Src.Dao.UserDao dao = new Src.Dao.UserDao();
            string email = Request["eamil"];
            User user = dao.findUserByEmail(email);
            if (user == null)//邮箱没有被注册
            {
                ViewData["msg"] = "该邮箱未注册,请重新输入";
            }
            else {
                ViewData["msg"] = "找回密码成功，请到邮箱查看";
                Src.Util.EmailUtil.SentMail(email, "您的密码找回成功，密码为" + user.pwd, "CGit找回密码", "CGit");
            }
            return View("findPwd");
        }
        public ActionResult search(string keyWord)
        {
            UserBll userBll = new UserBll();
            List<User> userList = userBll.searchUser(keyWord);
            ViewData["userList"] = userList;
            RepositoryBll repositoryBll = new RepositoryBll();
            List<Repository> repositoryList = repositoryBll.searchRepository(keyWord);
            ViewData["repositoryList"] = repositoryList;

            RepositoryDao repositoryDao = new RepositoryDao();
            FollowRepositoryBll followRepositoryBll = new FollowRepositoryBll();
            List<string> repositoryCountList = followRepositoryBll.findRepositoryAndCount();
            if (repositoryCountList != null)
            {
                List<Repository> repositorylist = new List<Repository>();
                foreach (string userEmail in repositoryCountList)
                {
                    repositorylist.Add(repositoryDao.findRepositoryById(userEmail));
                }
                ViewData["repositoryCountList"] = repositorylist;
            }
            return View("searchResult");
        }
    }
}