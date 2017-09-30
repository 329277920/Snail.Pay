using System;
using System.Threading.Tasks;

namespace Snail.Pay.Common.Log
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public sealed class Logger
    {
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex">异常对象</param>
        public static void Error(string message, Exception ex = null)
        {
            SafeInvoke((obj) =>
            {
                var ps = obj as Tuple<string, Exception>;
#if DEBUG
                Debug(ps.Item1 + "error:" + ps.Item2?.ToString());
#endif
                GetLogger().Error(ps.Item1, ps.Item2);

            }, new Tuple<string, Exception>(message, ex));
        }

        /// <summary>
        /// 异步写入异常日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex">异常对象</param>
        public static Task ErrorAsync(string message, Exception ex = null)
        {
            return new TaskFactory().StartNew(obj =>
            {
                var ps = obj as Tuple<string, Exception>;

                Error(ps.Item1, ps.Item2);
            }, new Tuple<string, Exception>(message, ex));
        }

        /// <summary>
        /// 写入普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Info(string message)
        {
            SafeInvoke((obj) =>
            {
                var content = obj as string;
#if DEBUG
                Debug(content);
#endif               
                GetLogger().Info(content);
            }, message);
        }

        /// <summary>
        /// 异步写入普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        public static Task InfoAsync(string message)
        {
            return new TaskFactory().StartNew(objMsg =>
            {
                Info(objMsg as string);
            }, message);
        }

        /// <summary>
        /// 在Debug模式中，将信息写入输出窗口
        /// </summary>
        /// <param name="message">日志内容</param>
        public static void Debug(string message)
        {
            SafeInvoke((obj) =>
            {
                GetLogger().Debug(obj as string);

            }, message);
        }

        /// <summary>
        /// 安全执行某个方法，将错误信息输出到调试窗口
        /// </summary>
        /// <param name="invoke"></param>
        /// <param name="value"></param>
        private static void SafeInvoke(Action<object> invoke,object value)
        {
            try
            {
                invoke(value);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
            }
        }

        private static ILogger GetLogger()
        {
            // 这里使用Log4Net，写入本地日志副本
            return new Log4NetLogger();
            //var log = Snail.IOC.IocContainer.Resolve<ILogger>();
            //if (log == null)
            //{
            //    throw new Exception("未能加载ILogger的实现类。");
            //}
            //return log;
        }
    }
}
