using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model.ClientModels
{
    /// <summary>
    /// 退款提交对象
    /// </summary>
    public class OrderRefundInfo
    {
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 原订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 退款订单号
        /// </summary>
        public string RefundOrderNo { get; set; }

        /// <summary>
        /// 退款订单金额
        /// </summary>
        public int RefundAmount { get; set; }
    }
}
