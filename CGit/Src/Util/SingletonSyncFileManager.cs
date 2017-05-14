using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Util
{
    public static class SingletonSyncFileManager
    {
        static ConcurrentDictionary<string, SyncFileUtil> fileDic = new ConcurrentDictionary<string, SyncFileUtil>();

       /// <summary>
       /// 根据操作文件的路径获取文件同步操作类的单例
       /// 如果操作类存在就直接返回这个操作
       /// 如果操作类不存在就创建这个类并返回
       /// </summary>
       /// <param name="path">文件路径</param>
       /// <returns>获取到的类</returns>
        public static SyncFileUtil getInstance(string path)
        {
            return fileDic.GetOrAdd(path,new SyncFileUtil(path));
        }


        /// <summary>
        /// 减少指定path下操作员的数量
        /// 如果操作员的数量减少到零就将其移除
        /// </summary>
        /// <param name="path">路径</param>
        public static void removeOneOperator(string path)
        {
            SyncFileUtil syncFileUtil = getInstance(path);
            lock (syncFileUtil)
            {
                syncFileUtil.subOneOperatorer();
                if (syncFileUtil.hasOperatorer())
                {
                    syncFileUtil.subOneOperatorer();
                }
                else
                {
                    fileDic.TryRemove(path,out syncFileUtil);
                }
            }
        }
    }
}
