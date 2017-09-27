using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    /// <summary>
    /// 交易实体
    /// </summary>
    public class TransactionInfo
    {
        /// <summary>
        /// 交易id
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// 外部系统订单号
        /// </summary>
        public string OuterOrderId { get; set; }

        /// <summary>
        /// 外部系统ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        public string PayType { get; set; }

        /// <summary>
        /// 支付平台编号
        /// </summary>
        public string PayPlatformNo { get; set; }

        /// <summary>
        /// 各支付平台支付流水号
        /// </summary>
        public string PayId { get; set; }      
        
        /// <summary>
        /// 交易状态
        /// </summary>
        public TransactionStatus Status { get; set; }
    }
}
