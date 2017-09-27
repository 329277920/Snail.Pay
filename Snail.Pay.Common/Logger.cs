using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Common
{
    /// <summary>
    /// 日志记录器
    /// </summary>
    public sealed class Logger
    {
        public static void Error(string title, string message, Exception ex = null)
        {
            var log = new StringBuilder();            

#if DEBUG
            System.Diagnostics.Debug.WriteLine(log.ToString());
#endif
        }

        public static void Error(string message, Exception ex = null)
        {
            var log = new StringBuilder(message);
            if (ex != null)
            {
                log.Append("," + ex.ToString());
            }
#if DEBUG
            System.Diagnostics.Debug.WriteLine(log.ToString());
#endif

        }

        public static async Task ErrorAsync(string message, Exception ex = null)
        {
            var log = new StringBuilder(message);
            if (ex != null)
            {
                log.Append("," + ex.ToString());
            }
            await new TaskFactory().StartNew((obj) =>
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(obj.ToString());
#endif
            }, log).ConfigureAwait(false);
        }

        public static async Task InfoAsync(string message)
        {            
            await new TaskFactory().StartNew((obj) =>
            {
#if DEBUG
                System.Diagnostics.Debug.WriteLine(obj.ToString());
#endif
            }, message).ConfigureAwait(false);
        }
    }
}
