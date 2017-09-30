using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    /// <summary>
    /// 已知系统异常信息，该异常要求日志监控程序不记录堆栈跟踪信息，同时通知异常处理器如何返回错误码
    /// </summary>
    public class KnowResultException : KnownException
    {
        public MethodResult Result { get; private set; }

        public KnowResultException(string message, string resultCode, string resultMsg) : base(message)
        {
            Result = new MethodResult(resultCode, resultMsg);
        }

        public KnowResultException(string message, string resultCode, string resultMsg,dynamic data) : base(message)
        {
            Result = new MethodResult(resultCode, resultMsg, data);
        }
    }
}
