using Snail.Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.DataLayerInterface
{
    /// <summary>
    /// 交易数据处理接口
    /// </summary>
    public interface ITransactionDal
    {
        /// <summary>
        /// 新增交易记录
        /// </summary>
        /// <param name="entity">交易实体</param>
        /// <returns>返回操作结果，大于0成功</returns>
        int Insert(TransactionInfo entity);

        /// <summary>
        /// 根据交易ID查询交易记录
        /// </summary>
        /// <param name="transactionId">交易ID</param>
        /// <returns>返回TransactionInfo</returns>
        TransactionInfo Select(string transactionId);

        /// <summary>
        /// 根据外部系统号与外部系统订单查询交易记录
        /// </summary>
        /// <param name="appId">外部系统号</param>
        /// <param name="outOrderNo">外部系统订单号</param>
        /// <returns>返回TransactionInfo</returns>
        TransactionInfo Select(string appId, string outOrderNo);
    }
}
