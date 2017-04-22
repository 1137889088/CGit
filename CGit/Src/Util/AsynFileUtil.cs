using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CGit.Src.Util
{

    public class AsynFileUtil
    {
       private ReaderWriterLockSlim rwl = new ReaderWriterLockSlim();
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns>是否创建成功</returns>
        public bool createDictionary(string path)
        {
            lock (path)//给方法加锁防止同时创建文件
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

        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path">文件路径</param>
        /// <returns>是否创建成功</returns>
        public bool createFile(string path)
        {
            lock (path)//给方法加锁防止同时创建文件
            {
                if (Directory.Exists(path))
                {
                    return false;
                }
                System.IO.File.Create(path);
                return true;
            }
        }
       
        /// <summary>
        /// 线程安全的文件写入操作
        /// 如果文件存在就写入文件
        /// 如果文件不存在就创建文件然后写入，
        /// 写入文件失败返回false
        /// </summary>
        /// <param name="filename">要写入的文件名</param>
        /// <param name="content">要写入的内容</param>
        /// <returns>是否写入文件成功</returns>
        public bool createFileAndWrite(string filePath, string content)
        {
            if (rwl.TryEnterWriteLock(3000))//尝试获取写锁
            {
                bool isSuccess = false;
                try
                {
                    FileStream fs = new FileStream(@filePath, FileMode.OpenOrCreate, FileAccess.Write); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
                    StreamWriter sw = new StreamWriter(fs); // 创建写入流
                    sw.WriteLine(@content); // 写入
                    sw.Close(); //关闭文件
                    isSuccess = true;
                }
                catch
                {
                    isSuccess = false;
                }
                finally
                {
                    rwl.ExitWriteLock();//释放写锁
                }
                return isSuccess;
            }   
            return false;
        }

        /// <summary>
        /// 文件读取操作
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns>读取到的内容，如果出错或者为读取到则返回null</returns>
        public string readFile(string filePath)
        {
            string text = null;
            if (rwl.TryEnterReadLock(3000))//尝试获取读锁
            {
                try
                {
                    text = System.IO.File.ReadAllText(@filePath);
                }
                finally
                {
                    rwl.ExitReadLock();//释放读取锁
                }
                return text;
            }
            return null;
        }
    }
}
