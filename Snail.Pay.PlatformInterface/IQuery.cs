using Snail.Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.PlatformInterface
{
    /// <summary>
    /// 支付查询接口
    /// </summary>
    public interface IQuery
    {
        /// <summary>
        /// 查询支付结果
        /// </summary>
        /// <param name="trade">交易实体</param>
        /// <returns>返回查询结果</returns>
        MethodResult Query(TransactionInfo trade);
    }
}
