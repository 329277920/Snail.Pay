using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Pay.Common.Configuration
{
    /// <summary>
    /// 配置文件管理器
    /// </summary>
    public class ConfigurationManager
    {
        /// <summary>
        /// 读写访问控制锁
        /// </summary>
        private static ReaderWriterLockSlim _lock;

        /// <summary>
        /// 配置信息缓存列表
        /// </summary>
        private static Dictionary<string, object> _configs;

        private static Encoding _encode;

        static ConfigurationManager()
        {
            _lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
            _configs = new Dictionary<string, object>();
            _encode = Encoding.UTF8;

            ConfigRefEngine.Instance.Start();
        }

        /// <summary>
        /// 根据指定的配置文件获取配置配置实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="cfgFile">配置文件路径</param>
        /// <returns></returns>
        public static T Get<T>(string cfgFile)
            where T : class, new()
        {
            var cfgKey = GetConfigKey(cfgFile);

            var oldCfg = GetExists(cfgKey);
            if (oldCfg != null)
            {
                return (T)oldCfg;
            }

            var newCfg = (T)LoadConfig(cfgFile, typeof(T));
            if (newCfg != default(T))
            {
                Set(cfgKey, newCfg);
            }
            return newCfg;
        }

        /// <summary>
        /// 根据指定的键查找已存在的配置实体
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="cfgKey"></param>
        /// <returns></returns>
        private static object GetExists(string cfgKey)
        {
            try
            {
                _lock.EnterReadLock();
                if (_configs.ContainsKey(cfgKey))
                {
                    return _configs[cfgKey];
                }
            }
            finally
            {
                _lock.ExitReadLock();
            }
            return null;
        }

        /// <summary>
        /// 设置配置文件实体
        /// </summary>       
        /// <param name="cfgKey">配置文件缓存Key值</param>
        /// <param name="config">配置实体</param>      
        internal static void Set(string cfgKey, object config)
        {
            try
            {
                _lock.EnterWriteLock();
                if (_configs.ContainsKey(cfgKey))
                {
                    _configs[cfgKey] = config;
                }
                else
                {
                    _configs.Add(cfgKey, config);

                    InitMoniter(cfgKey);
                }
                ConfigLogger.LoadConfigSucess(cfgKey);
            }
            catch (Exception ex)
            {
                ConfigLogger.UnKnowError(ex);
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        /// <summary>
        /// 获取配置文件缓存的Key
        /// </summary>
        /// <param name="cfgFile"></param>
        /// <returns></returns>
        private static string GetConfigKey(string cfgFile)
        {
            return System.IO.Path.GetFullPath(cfgFile);
        }

        /// <summary>
        /// 加载配置实体
        /// </summary>
        /// <param name="filePath">配置文件完整路径</param>
        /// <param name="cfgType"></param>
        /// <returns>返回加载是否成功</returns>
        internal static object LoadConfig(string filePath, Type cfgType)
        {
            if (!File.Exists(filePath))
            {
                ConfigLogger.FileIsNotFind(filePath);
                return null;
            }
            try
            {                
                using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    // 使用 IConfiguration 接口填充配置实体
                    var parser = cfgType.Assembly.CreateInstance(cfgType.FullName) as IConfiguration;
                    if (parser != null)
                    {
                        try
                        {
                            parser.Fill(fs);
                        }
                        catch (Exception ex)
                        {
                            ConfigLogger.IFileConfigError(cfgType, ex);
                            return null;
                        }
                    }
                    // 使用XML序列化实体
                    else
                    {
                        var fileContent = fs.ReadToEnd(_encode);
                        return Serializer.XmlDeserialize(fileContent, cfgType);
                    }
                }                                             
            }
            catch (System.IO.IOException ioEx)
            {
                ConfigLogger.ReadFileError(filePath, ioEx);

                return null;
            }
            catch (Exception ex)
            {
                ConfigLogger.UnKnowError(ex);

                return null;
            }
            return null;
        }

        /// <summary>
        /// 开启对某个配置文件的监控
        /// </summary>
        /// <param name="filePath">文件路径</param>
        private static void InitMoniter(string filePath)
        {
            try
            {
                var fileWatcher = new FileSystemWatcher(System.IO.Path.GetDirectoryName(filePath), System.IO.Path.GetFileName(filePath));                 
                fileWatcher.IncludeSubdirectories = false;
                fileWatcher.Changed += (sender, e) =>
                {
                    var cgFile = e.FullPath;
                    object oldCfg = GetExists(cgFile);
                    if (oldCfg == null)
                    {
                        ConfigLogger.CacheIsNotFind(cgFile);
                        return;
                    }

                    var type = oldCfg.GetType();
                   
                    ConfigRefQueue.Instance.Enqueue(new ConfigRefItem() {
                        ConfigEntityType = type,
                        AddTime = DateTime.Now,
                        FilePath = cgFile
                    });
                };

                fileWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ex)
            {
                ConfigLogger.UnKnowError(ex);
            }
        }
    }
}
