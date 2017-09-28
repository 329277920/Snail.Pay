using Snail.Pay.Model.ClientModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Model.Check
{
    /// <summary>
    /// 数据校验管理类
    /// </summary>
    public class CheckDataManager
    {
        /// <summary>
        /// 校验客户端提交的订单信息
        /// </summary>
        /// <param name="entity">订单实体</param>
        /// <returns>返回校验是否成功，以及提示信息</returns>
        public static async Task<Tuple<bool, string>> CheckOrder(OrderInfo entity)
        {
            return await new TaskFactory<Tuple<bool, string>>()
               .StartNew((() => LazyCheckOrder.Value.Check(entity)));
        }

        /// <summary>
        /// 校验客户端提交的订单查询信息
        /// </summary>
        /// <param name="entity">订单查询实体</param>
        /// <returns>返回校验是否成功，以及提示信息</returns>
        public static async Task<Tuple<bool, string>> CheckOrderQuery(OrderQueryInfo entity)
        {
            return await new TaskFactory<Tuple<bool, string>>()
               .StartNew((() => LazyCheckOrderQuery.Value.Check(entity)));
        }

        #region 私有成员

        private static Lazy<CheckEntity<OrderInfo>> LazyCheckOrder = new Lazy<CheckEntity<OrderInfo>>(() =>
        {
            var chk = new CheckEntity<OrderInfo>();

            chk.AddRule(info => info, CheckUnity.Required, "未传入订单信息");
            chk.AddRule(info => info.Amount, value => value > 0, "订单金额必须大于0");
            chk.AddRule(info => info.AppId, CheckUnity.Required, "必须传入AppId");
            chk.AddRule(info => info.OrderNo, CheckUnity.Required, "必须传入OrderNo");            

            return chk;
        });

        private static Lazy<CheckEntity<OrderQueryInfo>> LazyCheckOrderQuery = new Lazy<CheckEntity<OrderQueryInfo>>(() =>
        {
            var chk = new CheckEntity<OrderQueryInfo>();

            chk.AddRule(info => info, CheckUnity.Required, "未传入订单传信息");
            chk.AddRule(info => info, (info) =>
            {
                if (info.TransactionId?.Length > 0)
                {
                    return true;
                }
                if (info.OrderNo?.Length > 0 && info.AppId?.Length > 0)
                {
                    return true;
                }
                return false;
            }, "必须传入交易流水号或订单号与系统编号的组合");
            return chk;
        });
        #endregion
    }
}
