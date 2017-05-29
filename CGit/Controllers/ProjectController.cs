using CGit.Models;
using CGit.Src.Constant;
using CGit.Src.Dao;
using CGit.Src.Util;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace CGit.Controllers
{
    public class ProjectController : Controller
    {
        RepositoryDao repositoryDao = new RepositoryDao();
        //VersionDao versionDao = new VersionDao();
        // GET: ProjectrepositoryId
        public ActionResult projectFileModify(string repositoryId, string version, string path)
        {
            ViewData["repositoryId"] = repositoryId;
            ViewData["version"] = version;
            ViewData["path"] = path;

            string projectPath = Server.MapPath("/");//取得项目决对路径
            string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
            string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径



            Repository repository = repositoryDao.findRepositoryById(repositoryId);//获取仓库的id
            Src.Dao.UserDao userDao = new Src.Dao.UserDao();
            User user = userDao.findUserByEmail(repository.email);//根据仓库id查找用户的信息

            ViewData["user"] = user;//设置仓库所属用户信息
            ViewData["repository"] = repository;//设置仓库信息

            DirectoryInfo[] versions = SingletonSyncFileManager.getInstance(currentRepositoryPath).listDir();//版本列表
            ViewData["versions"] = versions;//版本列表
            SingletonSyncFileManager.removeOneOperator(currentRepositoryPath);
            return View();
        }
        public ActionResult saveFile(string repositoryId, string version, string path, string fileContent)
        {
            if (repositoryId != null || !repositoryId.Equals("") ||
              version != null || !version.Equals(""))
            {
                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径

                string showPath = projectPath + repositorySavePath + repositoryId + "/" + version + "/" + path;
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(showPath);
                fileUtil.createFileAndWrite(fileContent);
                SingletonSyncFileManager.removeOneOperator(showPath);
                return projectFileContent(repositoryId, version, path);
            }
            return RedirectToAction("frame", "User");
        }

        public string getFileConetent(string repositoryId, string version, string path)
        {
            if (repositoryId != null || !repositoryId.Equals("") ||
               version != null || !version.Equals(""))
            {
                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径

                string showPath = projectPath + repositorySavePath + repositoryId + "/" + version + "/" + path;
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(showPath);
                string content = fileUtil.readFile();
                ViewData["fileContent"] = content;
                SingletonSyncFileManager.removeOneOperator(showPath);
                return content;
            }
            return "";
        }
        /// <summary>
        /// 创建新的版本
        /// </summary>
        /// <param name="repositoryId"></param>
        /// <param name="versionIdentity"></param>
        /// <returns></returns>
        public ActionResult newVersion(string repositoryId, string versionIdentity)
        {
            if (repositoryId != null)//传来的仓库id不为空
            {
                if (versionIdentity != null || !versionIdentity.Equals(""))
                {
                    string projectPath = Server.MapPath("/");//取得项目决对路径
                    string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                    string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径
                    string newRepositoryPath = currentRepositoryPath + "/" + versionIdentity;//当前仓库路径
                    SingletonSyncFileManager.getInstance(newRepositoryPath).createDictionary();
                    SingletonSyncFileManager.removeOneOperator(newRepositoryPath);
                    return projectFiles(repositoryId, versionIdentity, "");
                }
                else
                {
                    return projectFiles(repositoryId, "", "");
                }

            }
            return RedirectToAction("frame", "User");
        }


        /// <summary>
        /// 列出所有文件和文件夹
        /// </summary>
        /// <param name="repositoryId">要列出文件的仓库id</param>
        /// <param name="version">要列出的版本</param>
        /// <param name="path">版本内的路径</param>
        /// <returns></returns>
        public ActionResult projectFiles(string repositoryId, string version, string path)
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
                ViewData["user"] = user;//设置仓库所属用户信息
                ViewData["repository"] = repository;//设置仓库信息
                                                    //List<MVersion> versions = versionDao.findVersionByRepositoryId(id);//根据仓库查找所有版本

                DirectoryInfo[] versions = SingletonSyncFileManager.getInstance(currentRepositoryPath).listDir();//版本列表
                ViewData["versions"] = versions;//版本列表
                SingletonSyncFileManager.removeOneOperator(currentRepositoryPath);


                if (versions == null || versions.Length == 0)
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
                    ViewData["version"] = versions[0];//文件夹列表
                }
                else
                {
                    showPath = projectPath + repositorySavePath + repositoryId + "/" + version + "/" + path;
                    ViewData["version"] = version;//文件夹列表
                }
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(showPath);
                ViewData["dirList"] = fileUtil.listDir();//文件夹列表
                ViewData["fileList"] = fileUtil.listFile();//文件列表
                SingletonSyncFileManager.removeOneOperator(showPath);
                ViewData["path"] = path;
            }
            else
            {
                return RedirectToAction("frame", "User");
            }
            return View("projectFiles");
        }
        public ActionResult newDir(string repositoryId, string version, string path, string fileName)
        {
            if(repositoryId!=null&&!repositoryId.Equals("") &&
                version != null && !version.Equals("") &&
                    fileName != null && !fileName.Equals(""))
            {
                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径

                string showPath = projectPath + repositorySavePath + repositoryId + "/" + version + "/" + path + "/" + fileName;
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(showPath);
                fileUtil.createDictionary();
                SingletonSyncFileManager.removeOneOperator(showPath);
                return projectFiles(repositoryId, version, path);
            }
            return RedirectToAction("frame", "User");
        }

        public ActionResult newFile(string repositoryId, string version, string path, string fileName)
        {
            if (repositoryId != null || !repositoryId.Equals("") ||
                version != null || !version.Equals("") ||
                    fileName != null || !fileName.Equals(""))
            {
                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径

                string showPath = projectPath + repositorySavePath + repositoryId + "/" + version + "/" + path + "/" + fileName;
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(showPath);
                fileUtil.createFile();
                SingletonSyncFileManager.removeOneOperator(showPath);
                return projectFiles(repositoryId, version, path);
            }
            return RedirectToAction("frame", "User");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public ActionResult projectFileContent(string repositoryId, string version, string path)
        {
            if (repositoryId != null || !repositoryId.Equals("") ||
                version != null || !version.Equals(""))
            {
                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径

                Repository repository = repositoryDao.findRepositoryById(repositoryId);//获取仓库的id
                Src.Dao.UserDao userDao = new Src.Dao.UserDao();
                User user = userDao.findUserByEmail(repository.email);//根据仓库id查找用户的信息

                ViewData["user"] = user;//设置仓库所属用户信息
                ViewData["repository"] = repository;//设置仓库信息
                //List<MVersion> versions = versionDao.findVersionByRepositoryId(id);//根据仓库查找所有版本

                DirectoryInfo[] versions = SingletonSyncFileManager.getInstance(currentRepositoryPath).listDir();//版本列表
                ViewData["versions"] = versions;//版本列表
                SingletonSyncFileManager.removeOneOperator(currentRepositoryPath);
                ViewData["repositoryId"] = repositoryId;
                ViewData["version"] = version;
                ViewData["path"] = path;
                string showPath = projectPath + repositorySavePath + repositoryId + "/" + version + "/" + path;
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(showPath);
                string content = fileUtil.readFile();
                ViewData["fileContent"] = content;
                SingletonSyncFileManager.removeOneOperator(showPath);
                return View("projectFileContent");
            }
            return RedirectToAction("frame", "User");
        }
        public ActionResult projectComment(string repositoryId)
        {

            return RedirectToAction("projectComment", "Comment", repositoryId); ;
        }
        public ActionResult uploadFile(HttpPostedFileBase file, string repositoryId, string version, string path)
        {

            if (repositoryId != null || !repositoryId.Equals("") ||
                version != null || !version.Equals("") ||
                    file != null)
            {
                string fileName = file.FileName;               
                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径

                string showPath = projectPath + repositorySavePath + repositoryId + "/" + version + "/" + path + "/" + fileName;

                file.SaveAs(showPath);
                return projectFiles(repositoryId, version, path);
            }
            return RedirectToAction("frame", "User");
        }

        public ActionResult modifyTitle(string repositoryId, string version, string path,string newTitle)
        {
            repositoryDao.updataName(newTitle, repositoryId);
            return projectFiles(repositoryId, version, newTitle);
        }
        public ActionResult fileDownload(string repositoryId)
        {
            if (repositoryId != null && !repositoryId.Equals(""))
            {
                string projectPath = Server.MapPath("/");//取得项目决对路径
                string repositorySavePath = ConfigConstant.repositoryPath;//获取仓库存放位置
                string currentRepositoryPath = projectPath + repositorySavePath + repositoryId;//当前仓库路径
                string temp = projectPath + "temp/"+ repositoryId+".zip";//当前仓库路径
                SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(temp);
                fileUtil.delete();
                SingletonSyncFileManager.removeOneOperator(temp);
                ZipFile.CreateFromDirectory(currentRepositoryPath, temp);
                return File(temp, "1", Url.Encode(repositoryId+".zip"));
            }
            return null;
        }
    }
}