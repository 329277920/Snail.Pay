using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Common.Log
{
    /// <summary>
    /// 日志记录器接口
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="message">日志内容</param>
        /// <param name="ex">异常对象</param>
        void Error(string message, Exception ex = null);

        /// <summary>
        /// 写入普通日志
        /// </summary>
        /// <param name="message">日志内容</param>
        void Info(string message);

        /// <summary>
        /// 在Debug模式中，将信息写入输出窗口
        /// </summary>
        /// <param name="message">日志内容</param>
        void Debug(string message);
    }
}
