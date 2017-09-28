namespace Snail.Pay.Model
{
    /// <summary>
    /// 订单通知结果实体类（第三方支付发起前端通知，后端通知，查询结果）
    /// </summary>
    public class TransactionResultInfo
    {
        /// <summary>
        /// 获取或设置该笔交易是否成功完成
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 订单实际支付金额
        /// </summary>
        public int PayAmount { get; set; }

        /// <summary>
        /// 支付平台实际订单号
        /// </summary>
        public string PayPlatformNo { get; set; }
    }
}
