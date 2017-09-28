using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model
{
    public class MethodResultMessage
    {
        #region 全局

        public const string Success = "Success";

        /// <summary>
        /// 客户端请求错误
        /// </summary>
        public const string RequestFailed = "客户端请求错误";

        public const string ServerFailed = "处理失败";

        #endregion

        #region 订单相关

        public const string OrderExisted = "不能重复提交订单";

        /// <summary>
        /// 订单已支付
        /// </summary>
        public const string OrderPaid = "订单支付已完成";

        /// <summary>
        /// 订单未找到
        /// </summary>
        public const string OrderUnfound = "订单未找到";

        #endregion

        #region 支付相关

        public const string SignError = "校验签名失败";

        /// <summary>
        /// 请求外部接口失败
        /// </summary>
        public const string CallFailed = "请求外部接口失败";

        #endregion
    }
}
