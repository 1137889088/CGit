using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGit.Src.Util
{
    public static class SingletonManager
    {
        static ConcurrentDictionary<string, AsynFileUtil> fileDic = new ConcurrentDictionary<string, AsynFileUtil>();

        // 根据类型获取单例
        public static Object getInstance(string path)
        {
            return fileDic.GetOrAdd(path,new AsynFileUtil());
        }
    }
}
