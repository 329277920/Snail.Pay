using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    /// <summary>
    /// 支付行为定义
    /// </summary>
    public class TransactionActionType
    {
        /// <summary>
        /// App支付
        /// </summary>
        public const string App = "app";

        /// <summary>
        /// H5手机网站支付
        /// </summary>
        public const string H5 = "h5";

        /// <summary>
        /// 扫码支付
        /// </summary>
        public const string QRCode = "qrcode";

        /// <summary>
        /// 查询交易订单
        /// </summary>
        public const string Query = "query";

        /// <summary>
        /// 退款操作
        /// </summary>
        public const string Refund = "refund";

        /// <summary>
        /// 前端回调
        /// </summary>
        public const string Return = "return";

        /// <summary>
        /// 后端回调
        /// </summary>
        public const string Notify = "notify";
    }
}
