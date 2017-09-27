using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;

namespace Snail.Pay.Interceptor
{
    public sealed class FitterUtility
    {
        public static void SetLogId(HttpRequestMessage request)
        {
            request.Headers.Add(LOG_HEADER, NewLogId().ToString());
        }

        public static string GetLogId(HttpRequestMessage request)
        {
            return request.Headers.GetValues(LOG_HEADER).SingleOrDefault();
        }


        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>
        /// <param name="ex"></param>
        public static void Error(HttpRequestMessage request, string content, Exception ex)
        {
            Common.Logger.Error(string.Format("logid:{0},{1}", GetLogId(request), content));
        }

        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>      
        public static void Info(HttpRequestMessage request, string content)
        {
            Common.Logger.Error(string.Format("logid:{0},{1}", GetLogId(request), content));
        }

        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="request"></param>
        /// <param name="content"></param>      
        public static Task InfoAsync(HttpRequestMessage request, string content)
        {
            return Common.Logger.InfoAsync(string.Format("logid:{0},{1}", GetLogId(request), content));
        }


        #region 记录ID生成

        private const string LOG_HEADER = "LogId_Header_Monitor";

        /// <summary>
        /// 记录每一次请求的任务ID
        /// </summary>
        private static long LogId;

        private static object LockObj = new object();

        private static long NewLogId()
        {
            lock (LockObj)
            {
                if (LogId >= long.MaxValue)
                {
                    LogId = 0;
                }
                return ++LogId;
            }
        }
        #endregion
    }
}
