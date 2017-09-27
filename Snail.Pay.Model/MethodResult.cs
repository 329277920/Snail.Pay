using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    /// <summary>
    /// 通用方法返回值
    /// </summary>
    public class MethodResult
    {
        public string Code { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; }

        public MethodResult(string code, string message, dynamic data)
        {
            Code = code;
            Message = message;
            Data = data;
        }

        public MethodResult(string code, string message) : this(code, message, string.Empty) { }

        public virtual bool IsSuccess
        {
            get { return Code == MethodResultCode.Success; }
        }
    }
}
