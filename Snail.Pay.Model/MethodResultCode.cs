using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    public class MethodResultCode
    {
        #region 全局

        #endregion
        public const string Success = "000";

        /// <summary>
        /// 客户端请求错误
        /// </summary>
        public const string RequestFailed = "100";

        /// <summary>
        /// 服务器处理失败
        /// </summary>
        public const string ServerFailed = "200";

        #region 订单相关

        /// <summary>
        /// 订单已经存在
        /// </summary>
        public const string OrderExisted = "300";

        /// <summary>
        /// 订单已支付
        /// </summary>
        public const string OrderPaid = "310";

        #endregion

        #region 支付相关

        /// <summary>
        /// 校验签名失败
        /// </summary>
        public const string SignError = "400";

        #endregion
    }
}
