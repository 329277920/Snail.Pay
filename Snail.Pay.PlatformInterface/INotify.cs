using Snail.Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.PlatformInterface
{
    /// <summary>
    /// 异步通知接口
    /// </summary>
    public interface INotify
    {
        /// <summary>
        /// 支付前端回调
        /// </summary>
        /// <param name="queryOrder">支付结果</param>      
        /// <returns>返回处理结果</returns>
        MethodResult Notify(string queryOrder);
    }
}
