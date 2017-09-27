using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    /// <summary>
    /// 交易状态
    /// </summary>
    public enum TransactionStatus
    {
        /// <summary>
        /// 未支付
        /// </summary>
        Unpaid = 0,
        /// <summary>
        /// 已支付
        /// </summary>
        Paid = 1       
    }
}
