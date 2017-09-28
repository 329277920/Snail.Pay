using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model.ClientModels
{
    /// <summary>
    /// 前端查询交易订单实体
    /// </summary>
    public class OrderQueryInfo : OrderInfo
    {
        /// <summary>
        /// 获取或设置支付平台交易ID
        /// </summary>
        public string TransactionId { get; set; }
    }
}
