using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using _Path = System.IO.Path;

namespace Snail.Pay.Common
{
    public sealed class PathUnity
    {
        private static Lazy<Type> CurrType => new Lazy<Type>(() => typeof(PathUnity), true);

        /// <summary>
        /// 在程序执行目录下查找指定的文件
        /// </summary>
        /// <param name="fileName">文件名称</param>
        /// <returns>返回文件路径</returns>
        public static string GetFilePath(string fileName)
        {
            var filePath = _Path.GetFullPath(fileName);            
            if (System.IO.File.Exists(filePath))
            {
                return filePath;
            }
            filePath = _Path.Combine(_Path.GetDirectoryName(CurrType.Value.Assembly.Location), fileName);
            if (System.IO.File.Exists(filePath))
            {
                return filePath;
            }
            filePath = _Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, fileName);
            if (System.IO.File.Exists(filePath))
            {
                return filePath;
            }
            filePath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin", fileName);
            if (System.IO.File.Exists(filePath))
            {
                return filePath;
            }
            return null;
        }

        /// <summary>
        /// 在程序执行目录下查找指定的目录
        /// </summary>
        /// <param name="dirPath">目录名称</param>
        /// <returns>返回文件路径</returns>
        public static string GetDirPath(string dirName)
        {
            var dirPath = _Path.GetFullPath(dirName);
            if (System.IO.Directory.Exists(dirPath))
            {
                return dirPath;
            }
            dirPath = _Path.Combine(_Path.GetDirectoryName(CurrType.Value.Assembly.Location), dirName);
            if (System.IO.Directory.Exists(dirPath))
            {
                return dirPath;
            }
            dirPath = _Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, dirName);
            if (System.IO.Directory.Exists(dirPath))
            {
                return dirPath;
            }
            dirPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "bin", dirName);
            if (System.IO.Directory.Exists(dirPath))
            {
                return dirPath;
            }
            return null;
        }
    }
}
