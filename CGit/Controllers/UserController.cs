using CGit.Models;
using CGit.Src.bll;
using CGit.Src.Constant;
using CGit.Src.Dao;
using CGit.Src.Util;
using System.Collections.Generic;
using System.Web.Mvc;

namespace CGit.Controllers
{
    public class UserController : Controller
    {
        Src.Dao.UserDao userDao = new Src.Dao.UserDao();
        RepositoryDao repositoryDao = new RepositoryDao();
     
        /// <summary>
        /// 加载主页面框架
        /// </summary>
        /// <returns></returns>
        public ActionResult frame()
        {
            return View();
        }

        public ActionResult follow(string follow_email)
        {
            User user = (User)Session["loginUser"];
            FollowUserBll bll = new FollowUserBll();
            bll.save(user.email, follow_email);
            return userHome(follow_email);
        }
        public ActionResult unfollow(string follow_email)
        {
            User user = (User)Session["loginUser"];
            FollowUserBll bll = new FollowUserBll();
            bll.delete(user.email, follow_email);
            return userHome(follow_email);
        }
        /// <summary>
        /// 转到用户主页
        /// </summary>
        /// <returns></returns>
        public ActionResult userHome(string email)
        {
            //获取显示的用户email的主页
            User user = null;
            if (email == null || email.Equals(""))//如果email为空是查看自己的信息
            {
                user = (User)Session["loginUser"];
                email = user.email;
            }
            else//如果email不为空就查找这个用户
            {
                user = userDao.findUserByEmail(email);
            }
            if (user == null)//防止不存在该email的用户
            {
                user = (User)Session["loginUser"];
                if (user == null)
                {
                    return RedirectToAction("login", "Home");//如果用户不存在就跳转到首页
                }
                email = user.email;
            }
            User currentUser = (User)Session["loginUser"];
            if (currentUser != null)
            {
                FollowUserBll bll = new FollowUserBll();
                ViewData["isFollow"] = bll.isFollow(currentUser.email, email);//判断是否关注
            }
            else
            {
                ViewData["isFollow"] = false;
            }
          

            List<Repository> repositoryList = repositoryDao.findAllRepositoryByEmail(email);//查找所有仓库
            ViewData["repositoryList"] = repositoryList;//将仓库数据放入viewData
            ViewData["user"] = user;//将用户信息放入viewData
            return View("userHome");
        }
        /// <summary>
        /// 我的关注
        /// </summary>
        /// <returns></returns>
        public ActionResult myFollow()
        {
            if ((User)Session["loginUser"] == null)
            {
                return RedirectToAction("login", "Home");//如果用户不存在就跳转到首页
            }
            string email = ((CGit.Models.User)Session["loginUser"]).email;

            FollowUserBll userBll = new FollowUserBll();
            List<string> userList = userBll.findAllFollowUserByUserEmail(email);
            if (userList != null)
            {
                List<User> userlist = new List<User>();
                foreach (string userEmail in userList)
                {
                    userlist.Add(userDao.findUserByEmail(userEmail));
                }
                ViewData["userList"] = userlist;
            }
          

            FollowRepositoryBll repositoryBll = new FollowRepositoryBll();
            List<string> repositoryList = repositoryBll.findAllFollowRepositoryByRepositoryEmail(email);
            if (repositoryList != null)
            {
                List<Repository> repositorylist = new List<Repository>();
                foreach (string userEmail in userList)
                {
                    repositorylist.Add(repositoryDao.findRepositoryById(userEmail));
                }
                ViewData["repositoryList"] = repositoryList;
            }
              
            return View();
        }
        /// <summary>
        /// 登出
        /// </summary>
        /// <returns></returns>
        public ActionResult singOut()
        {
            Session.Remove("loginUser");
            return RedirectToAction("login", "Home");
        }
        /// <summary>
        /// 热门项目视图
        /// </summary>
        /// <returns></returns>
        public ActionResult hotProject()
        {
            FollowRepositoryBll repositoryBll = new FollowRepositoryBll();
            List<string> repositoryList = repositoryBll.findRepositoryAndCount();
            if (repositoryList != null)
            {
                List<Repository> repositorylist = new List<Repository>();
                foreach (string userEmail in repositoryList)
                {
                    repositorylist.Add(repositoryDao.findRepositoryById(userEmail));
                }
                ViewData["repositoryList"] = repositorylist;
            }
            return View();
        }
        public ActionResult hotUser()
        {
            FollowUserBll userBll = new FollowUserBll();
            List<string> userList = userBll.findUserAndCount(); ;
            if (userList != null)
            {
                List<User> userlist = new List<User>();
                foreach (string userEmail in userList)
                {
                    userlist.Add(userDao.findUserByEmail(userEmail));
                }
                ViewData["userList"] = userlist;
            }
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
            if (user.pwd.Equals(oldPwd))
            {//密码检测
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

            return Content("更改密码失败");
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
            string projectPath = Server.MapPath("/");//取得项目路径
            string headImgPath = ConfigConstant.headImgPath;//获取图片位置
            Src.Util.ImgUtil.Base64StringToImage(imgData, projectPath + headImgPath + email, Src.Util.ImgUtil.TYPE_PNG);
            // Src.Util.ImgUtil.saveImg(strPath+"/img/user/" + email+".png", imgData);
            return Content("头像上传成功");
        }
        /// <summary>
        /// 添加仓库
        /// </summary>
        /// <param name="repository">仓库</param>
        /// <returns></returns>
        public ActionResult addRepository(Repository repository)
        {
            if (repository != null)//如果传来的仓库为空
            {
                repository.email = ((User)Session["loginUser"]).email;
                int id = repositoryDao.saveAndReturnId(repository);//*******存在危险
                string projectPath = Server.MapPath("/");//取得项目路径
                string repositoryPath = ConfigConstant.repositoryPath;//获取仓库路径
                string path = projectPath + repositoryPath + id;
                SingletonSyncFileManager.getInstance(path).createDictionary();
                SingletonSyncFileManager.removeOneOperator(path);
            }
            return userHome(null);
        }
        /// <summary>
        /// 删除仓库
        /// </summary>
        /// <param name="id">仓库id</param>
        /// <returns></returns>
        public ActionResult deleteRepository(string id)
        {
            if (id != null)//如果传来删除的用户id为空
            {
                string projectPath = Server.MapPath("/");//取得项目路径
                string repositoryPath = ConfigConstant.repositoryPath;//获取仓库路径
                string path = projectPath + repositoryPath + id;
                SingletonSyncFileManager.getInstance(path).delete();
                SingletonSyncFileManager.removeOneOperator(path);
                repositoryDao.deleteRepositoryById(id);
            }
            return userHome(null);
        }
    }
}