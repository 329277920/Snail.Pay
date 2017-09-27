using Snail.Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.PlatformInterface
{
    /// <summary>
    /// 发起支付接口
    /// </summary>
    public interface IInitiatePay
    {
        /// <summary>
        /// 发起支付
        /// </summary>
        /// <param name="tradeInfo">交易实体</param>
        /// <param name="extendInfo">交易扩展信息</param>
        /// <returns>返回发起结果</returns>
        MethodResult Pay(TransactionInfo tradeInfo, TransactionExtendInfo extendInfo);
    }
}
