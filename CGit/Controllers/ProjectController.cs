using CGit.Models;
using CGit.Src.Constant;
using CGit.Src.Dao;
using CGit.Src.Util;
using System.Collections.Generic;
using System.IO;
using System.Web.Mvc;

namespace CGit.Controllers
{
    public class ProjectController : Controller
    {
        RepositoryDao repositoryDao = new RepositoryDao();
        //VersionDao versionDao = new VersionDao();

        // GET: ProjectrepositoryId
        public ActionResult projectFileModify()
        {
            return View();
        }

        /// <summary>
        /// 创建新的版本
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <param name="versionIdentity"></param>
        /// <returns></returns>
        public ActionResult newVersion(string repositoryId,string versionIdentity)
        {
            string projectPath = Server.MapPath("/");//取得项目决对路径
            string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
            string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径
            string newRepositoryPath = currentRepositoryPath + "/" + versionIdentity;//当前仓库路径
            SingletonSyncFileManager.getInstance(newRepositoryPath).createDictionary();
            SingletonSyncFileManager.removeOneOperator(newRepositoryPath);
            return projectFiles(repositoryId,versionIdentity,"");
        }


        /// <summary>
        /// 列出所有文件和文件夹
        /// </summary>
        /// <param name="repositoryId">要列出文件的仓库id</param>
        /// <param name="version">要列出的版本</param>
        /// <param name="path">版本内的路径</param>
        /// <returns></returns>
        public ActionResult projectFiles(string repositoryId,string version,string path)
        {
            if (repositoryId != null)//传来的仓库id不为空
            {
                Repository repository = repositoryDao.findRepositoryById(repositoryId);//获取仓库的id
                Src.Dao.UserDao userDao = new Src.Dao.UserDao();
                User user = userDao.findUserByEmail(repository.email);//根据仓库id查找用户的信息

                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径

                user.pwd = "";
                ViewData["user"] = user;//设置用户信息
                ViewData["repository"] = repository;//设置仓库信息
                //List<MVersion> versions = versionDao.findVersionByRepositoryId(id);//根据仓库查找所有版本
   
                DirectoryInfo[] versions = SingletonSyncFileManager.getInstance(currentRepositoryPath).listDir();//版本列表
                ViewData["versions"] = versions;//版本列表
                SingletonSyncFileManager.removeOneOperator(currentRepositoryPath);
              

                if (versions == null|| versions.Length==0)
                {
                    return View("projectFiles");
                }


                if (path == null)
                {
                    path = "";
                }
                //VersionService service = new VersionService();
                string showPath;//要显示的文件列表路径
                if (version == null || version.Equals(""))
                {
                    showPath = projectPath + repositorySavePath + repositoryId + "/" + versions[0] + "/" + path;
                }
                else
                {
                    showPath = projectPath + repositorySavePath + repositoryId + "/" +  version + "/" + path;
                } 
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(showPath);
                ViewData["dirList"]= fileUtil.listDir();//文件夹列表
                ViewData["fileList"] = fileUtil.listFile();//文件列表
                SingletonSyncFileManager.removeOneOperator(showPath);
            }
            else
            {
                return RedirectToAction("frame", "User");
            }
            return View("projectFiles");
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