using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    /// <summary>
    /// 已知系统异常信息，该异常要求日志监控程序不记录堆栈跟踪信息
    /// </summary>
    public class KnownException : Exception
    {
        public KnownException(string message) : base(message) { }
    }
}
