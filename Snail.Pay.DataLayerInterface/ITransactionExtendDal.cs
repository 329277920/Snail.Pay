using Snail.Pay.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.DataLayerInterface
{
    /// <summary>
    /// 交易扩展信息数据处理接口
    /// </summary>
    public interface ITransactionExtendDal
    {
        /// <summary>
        /// 新增交易记录
        /// </summary>
        /// <param name="entity">交易实体</param>
        /// <returns>返回操作结果，大于0成功</returns>
        int Insert(TransactionExtendInfo entity);

        /// <summary>
        /// 根据交易ID查询交易记录
        /// </summary>
        /// <param name="transactionId">交易ID</param>
        /// <returns>返回TransactionInfo</returns>
        TransactionExtendInfo Select(string transactionId);
    }
}
