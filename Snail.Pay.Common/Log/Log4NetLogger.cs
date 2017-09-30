using log4net;
using log4net.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Common.Log
{
    /// <summary>
    /// 日志写入器，使用log4net
    /// </summary>
    internal class Log4NetLogger : ILogger
    {
        private Type CurrType = typeof(Log4NetLogger);

        /// <summary>
        /// 使用指定的配置文件初始化配置
        /// 如果配置节点在应用程序的config文件中，则不需要另外指定。
        /// 也可以在程序集属性中加入[assembly: log4net.Config.XmlConfigurator(Watch = true, ConfigFile = "")]
        /// </summary>
        /// <param name="config">log4net配置文件路径</param>
        public Log4NetLogger(string config = null)
        {
            if (string.IsNullOrEmpty(config))
            {
                config = PathUnity.GetFilePath("log4net.config");
            }
            if (!File.Exists(config))
            {
                throw new Exception(string.Format("not find config file : {0}", config));
            }
            XmlConfigurator.Configure(new FileInfo(config));         
        }
         
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex">异常对象</param>
        public void Error(string message, Exception ex = null)
        {            
            LogManager.GetLogger(Log4NetConsts.LOGGER_ERROR).Error(message, ex);
        }

        /// <summary>
        /// 写入普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public void Info(string message)
        {
            LogManager.GetLogger(CurrType).Info(message);
        }

        /// <summary>
        /// 获取一个ILog对象
        /// </summary>
        /// <param name="logger">ILog对象名称</param>
        public ILog GetLogger(string logger)
        {
            return LogManager.GetLogger(logger);
        }

        /// <summary>
        /// 在Debug模式中，将信息写入输出窗口
        /// </summary>
        /// <param name="message">日志内容</param>
        public void Debug(string message)
        {
            System.Diagnostics.Debug.WriteLine(message);
        }
    }
}
