namespace Snail.Pay.Model.ClientModels
{
    /// <summary>
    /// App提交创建订单实体
    /// </summary>
    public class OrderInfo
    {
        /// <summary>
        /// 所属系统ID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// 交易描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 交易过期时间（分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
    }
}
