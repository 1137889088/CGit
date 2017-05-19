using CGit.Models;
using CGit.Src.Constant;
using CGit.Src.Dao;
using CGit.Src.Service;
using CGit.Src.Util;
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
        //VersionDao versionDao = new VersionDao();

        // GET: Project
        public ActionResult projectFileModify()
        {
            return View();
        }
          /// <summary>
          /// 列出所有文件和文件夹
          /// </summary>
          /// <param name="id">要列出文件的仓库id</param>
          /// <param name="version">要列出的版本</param>
          /// <param name="path">版本内的路径</param>
          /// <returns></returns>
        public ActionResult projectFiles(string id,string version,string path)
        {
            if (id != null)//传来的仓库id不为空
            {
                Repository repository = repositoryDao.findRepositoryById(id);//获取仓库的id
                Src.Dao.UserDao userDao = new Src.Dao.UserDao();
                User user = userDao.findUserByEmail(repository.email);//根据仓库id查找用户的信息
                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                user.pwd = "";
                ViewData["user"] = user;//设置用户信息
                ViewData["repository"] = repository;//设置仓库信息
                //List<MVersion> versions = versionDao.findVersionByRepositoryId(id);//根据仓库查找所有版本

                string currentRepositoryPath = projectPath + repositorySavePath + id;//当前仓库路径
                List<string> versions = new List<string>();//版本列表
                foreach (DirectoryInfo dir in SingletonSyncFileManager.getInstance(currentRepositoryPath).listDir())//查找所有版本
                {
                    versions.Add(dir.Name);
                }
                SingletonSyncFileManager.removeOneOperator(currentRepositoryPath);
                ViewData["versions"] = versions;
                if (versions == null|| versions.Count==0)
                {
                    return View();
                }

                List<string> dirs = new List<string>();//文件夹列表
                List<string> files = new List<string>();//文件列表

                //VersionService service = new VersionService();
                string showPath;//要显示的文件列表路径
                if (version == null || version.Equals(""))
                {
                    showPath = projectPath + repositorySavePath + id + versions[0] + "/" + path;
                }
                else
                {
                    showPath = projectPath + repositorySavePath + id + version + "/" + path;
                }
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(showPath);
                foreach (DirectoryInfo dir in fileUtil.listDir())
                {//列出所有文件夹路径
                    dirs.Add(dir.Name);
                }
                foreach (FileInfo file in fileUtil.listFile())
                {//列出所有文件路径
                    dirs.Add(file.Name);
                }
                SingletonSyncFileManager.removeOneOperator(showPath);
            }
            else
            {
                return RedirectToAction("frame", "User");
            }
            return View();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
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