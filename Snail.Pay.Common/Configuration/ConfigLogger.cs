using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Snail.Pay.Common.Configuration
{
    /// <summary>
    /// 配置管理日志写入类
    /// </summary>
    internal sealed class ConfigLogger
    {
        private const string LOG_SOURCE = "ConfigManager";

        public static void Write(ConfigLogIds logId, EventLogEntryType logType, string msg)
        {
            switch (logType)
            {
                case EventLogEntryType.Error:
                    Log.Logger.Error(msg);
                    break;
                case EventLogEntryType.Information:
                    Log.Logger.Info(msg);
                    break;
            }           
        }

        public static void FileIsNotFind(string filePath)
        {           
            Write(ConfigLogIds.FileIsNotFind, EventLogEntryType.Error, string.Format("未找到配置文件：{0}", filePath));
        }

        public static void TypeError(Type entityType)
        {
            Write(ConfigLogIds.TypeError, EventLogEntryType.Error, string.Format("指定类型不继承接口IConfigFile：{0}", entityType.FullName));
        }

        public static void IFileConfigError(Type entityType, Exception ex)
        {
            Write(ConfigLogIds.IFileConfigError, EventLogEntryType.Error, string.Format("在调用IConfigFile接口方法时出错：{0}，错误信息：{1}", entityType.FullName, ex == null ? "无" : ex.ToString()));
        }

        public static void ReadFileError(string filePath, Exception ex)
        {
            Write(ConfigLogIds.ReadFileError, EventLogEntryType.Error, string.Format("未能读取配置文件：{0}，错误信息：{1}", filePath, ex == null ? "无" : ex.ToString()));
        }

        public static void UnKnowError(Exception ex)
        {
            Write(ConfigLogIds.UnKnowError, EventLogEntryType.Error, string.Format("发生未知异常，错误信息：{0}", ex == null ? "无" : ex.ToString()));
        }

        public static void CacheIsNotFind(string filePath)
        {
            Write(ConfigLogIds.CacheIsNotFind, EventLogEntryType.Error, string.Format("未能从缓存中读取配置信息，配置文件路径：{0}", filePath));
        }

        public static void LoadConfigSucess(string filePath)
        {
            Write(ConfigLogIds.LoadConfigSucess, EventLogEntryType.Information, string.Format("加载配置文件：{0}", filePath));
        }

        public static void RefConfigSucess(string filePath)
        {
            Write(ConfigLogIds.RefConfigSucess, EventLogEntryType.Information, string.Format("刷新配置文件：{0}", filePath));
        }
    }
}
