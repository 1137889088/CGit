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
    public class CommentController : Controller
    {
        CommentBll bll = new CommentBll();
        // GET: Comment
        public ActionResult projectComment(string repositoryId)
        {
            if (repositoryId != null&&repositoryId!="")
            {
                ViewData["repositoryId"] = repositoryId;
                RepositoryDao repositoryDao = new RepositoryDao();
                Repository repository = repositoryDao.findRepositoryById(repositoryId);//获取仓库的id
                Src.Dao.UserDao userDao = new Src.Dao.UserDao();
                User user = userDao.findUserByEmail(repository.email);//根据仓库id查找用户的信息

                ViewData["user"] = user;//设置仓库所属用户信息
                ViewData["repository"] = repository;//设置仓库信息

                List<Comment> comments = bll.findAllcommentByRepositoryId(repositoryId);
                ViewData["commnets"] = comments;

            }
          
            return View("projectComment");
        }

        public ActionResult addComment(Comment comment)
        {
            if (comment.repositoryId!= 0 &&
                comment.content != null && comment.content != ""&& comment.title != null && comment.title != ""
                && comment.userEmail != null && comment.userEmail != "")
            {
                comment.data = DateTime.Now.ToString("dddd,MMMM,dd ,yyyy", new System.Globalization.DateTimeFormatInfo());
                bll.save(comment);

            }

            return projectComment(comment.repositoryId+"");
        }
    }
} 