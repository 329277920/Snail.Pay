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
        /// <summary>
        /// 设置当前请求上下文某个时间段内的唯一ID
        /// </summary>
        /// <param name="request"></param>
        public static bool TrySetUniqueId(HttpRequestMessage request)
        {
            if (request != null)
            {
                request.Headers.Add(LOG_HEADER, NewLogId().ToString());
                return request.Headers.Contains(LOG_HEADER);
            }
            return false;
        }

        /// <summary>
        /// 获取当前请求上下文某个时间段内的唯一ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="uniqueId"></param>
        /// <returns></returns>
        public static bool TryGetLogId(HttpRequestMessage request, out string uniqueId)
        {
            uniqueId = "";
            if (request != null && request.Headers.Contains(LOG_HEADER))
            {
                uniqueId = request.Headers.GetValues(LOG_HEADER).SingleOrDefault();
                return uniqueId?.Length > 0;
            }
            return false;
        }

        /// <summary>
        /// 写入普通日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="request"></param>      
        public static Task InfoAsync(string content, HttpRequestMessage request = null)
        {
            if (TryGetLogId(request, out string uniqueId))
            {
                content = ("logid:" + uniqueId + "," + content);
            }
            return Common.Log.Logger.InfoAsync(content);
        }

        /// <summary>
        /// 写入异常日志
        /// </summary>
        /// <param name="content"></param>
        /// <param name="request"></param>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static Task ErrorAsync(string content, HttpRequestMessage request = null, Exception ex = null)
        {
            if (TryGetLogId(request, out string uniqueId))
            {
                content = ("logid:" + uniqueId + "," + content);
            }
            return Common.Log.Logger.ErrorAsync(content, ex);
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
