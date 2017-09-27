using Snail.Pay.Model;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.PlatformInterface
{
    /// <summary>
    /// 前端回调通知接口
    /// </summary>
    public interface IReturn
    {
        /// <summary>
        /// 支付前端回调
        /// </summary>
        /// <param name="queryOrder">支付结果</param>      
        /// <returns>返回处理结果</returns>
        MethodResult Return(string queryOrder);
    }
}
