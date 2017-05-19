using CGit.Models;
using CGit.Src.Util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Service
{
    public class VersionService
    {
        public List<string> findVsersionsDictoryByRepository(string versionPath )
        {
            List<string> dictoryList = new List<string>();
            SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(versionPath);
            foreach (DirectoryInfo t in fileUtil.listDir())
            {
                dictoryList.Add(t.Name);
            }
            return dictoryList;
        }
        public List<string> findVsersionsFileByRepository(string versionPath)
        {
            List<string> dictoryList = new List<string>();
            SyncFileUtil fileUtil = SingletonSyncFileManager.getInstance(versionPath);
            foreach (FileInfo t in fileUtil.listFile())
            {
                dictoryList.Add(t.Name);
            }
            return dictoryList;
        }
    }
}
