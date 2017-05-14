using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Util
{
    public class FileUtil
    {
        /// <summary>
        /// 根据路径创建文件
        /// 
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>是否创建成功</returns>
        public bool createFile(string filePath)
        {
            if (Directory.Exists(filePath))
            {
                return false;
            }
            System.IO.File.Create(filePath);
            return true;
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>是否创建成功</returns>
        public bool createDictionary(string path)
        {
            if (Directory.Exists(path))
            {
                return false;
            }
            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
            return true;
        }
    }
}
