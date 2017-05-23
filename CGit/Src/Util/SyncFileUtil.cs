using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CGit.Src.Util
{

    public class SyncFileUtil
    {
        private ReaderWriterLock rwl = new ReaderWriterLock();
        private string path;
        private int count;

        /// <summary>
        /// 构造函数初始化文件路径
        /// </summary>
        /// <param name="path">文件路径</param>
        public SyncFileUtil(string path)
        {
            this.path = path;
            count = 0;
        }

        /// <summary>
        /// 原子操作的操作员增加
        /// </summary>
        public void addOneOperatorer()
        {
            Interlocked.Increment(ref count);
        }
        /// <summary>
        /// 原子操作进行操作员的减少
        /// </summary>
        /// <returns></returns>
        public void subOneOperatorer()
        {
            Interlocked.Decrement(ref count);
        }
        /// <summary>
        /// 判断是否有操作员存在
        /// </summary>
        /// <returns>是否存在操作员</returns>
        public bool hasOperatorer()
        {
            if(count == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 创建文件夹
        /// </summary>
        /// <returns>是否创建成功</returns>
        public bool createDictionary()
        {

            rwl.AcquireWriterLock(3000);//尝试获取写锁
            if (Directory.Exists(path))
            {
                return false;
            }
            // Try to create the directory.
            DirectoryInfo di = Directory.CreateDirectory(path);
            rwl.ReleaseWriterLock();
            return true;
        }
      
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <returns>删除是否成功</returns>
        public bool delete()
        {
            rwl.AcquireWriterLock(3000);//尝试获取写锁
            DirectoryInfo di = new DirectoryInfo(path);
            di.Delete(true);
            rwl.ReleaseWriterLock();
            return true;
        }
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <returns>是否创建成功</returns>
        public bool createFile()
        {
            rwl.AcquireWriterLock(3000);//尝试获取写锁
            if (File.Exists(path))//判断文件是否存在
            {
                return false;
            }
            System.IO.File.Create(path);
            rwl.ReleaseWriterLock();
            return true;
        }
       
        /// <summary>
        /// 线程安全的文件写入操作
        /// 如果文件存在就写入文件
        /// 如果文件不存在就创建文件然后写入，
        /// 写入文件失败返回false
        /// </summary>
        /// <param name="content">要写入的内容</param>
        /// <returns>是否写入文件成功</returns>
        public bool createFileAndWrite(string content)
        {
            rwl.AcquireWriterLock(3000);//尝试获取写锁
            bool isSuccess = false;
            try
            {
                FileStream fs = new FileStream(@path, FileMode.OpenOrCreate, FileAccess.Write); //可以指定盘符，也可以指定任意文件名，还可以为word等文件
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
                rwl.ReleaseWriterLock();//释放写锁
            }
            return isSuccess;

        }

        /// <summary>
        /// 文件读取操作
        /// </summary>
        /// <returns>读取到的内容，如果出错或者为读取到则返回null</returns>
        public string readFile()
        {
            if(File.Exists(path)){//判断是否是文件
                string text = null;
                rwl.AcquireReaderLock(3000);//尝试获取读锁
                try
                {
                    text = System.IO.File.ReadAllText(@path);
                }
                finally
                {
                    rwl.ReleaseReaderLock();//释放读取锁
                }
                return text;
             }
             return null;
        }
        /// <summary>
        /// 列出所有文件夹
        /// </summary>
        /// <returns></returns>
        public DirectoryInfo[] listDir()
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                rwl.AcquireReaderLock(3000);
                DirectoryInfo[] childs = dir.GetDirectories();
                rwl.ReleaseReaderLock();//释放写读锁
                return childs;
            }
            return null;
        }      
        /// <summary>
        /// 列出所有文件
        /// </summary>
        /// <returns></returns>
        public FileInfo[] listFile()
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            if (dir.Exists)
            {
                rwl.AcquireReaderLock(3000);
                FileInfo[] childs = dir.GetFiles();
                rwl.ReleaseReaderLock();//释放写读锁
                return childs;
            }
            return null;
        }
    }
}
