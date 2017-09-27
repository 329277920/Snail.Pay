using Snail.Pay.DataLayerInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Snail.Pay.Model;

namespace Snail.Pay.DataLayer
{
    /// <summary>
    /// 交易扩展信息数据处理类
    /// </summary>
    public class TransactionExtendDal : ITransactionExtendDal
    {
        /// <summary>
        /// 新增交易记录
        /// </summary>
        /// <param name="entity">交易实体</param>
        /// <returns>返回操作结果，大于0成功</returns>
        public int Insert(TransactionExtendInfo entity)
        {
            return 1;
        }

        /// <summary>
        /// 根据交易ID查询交易记录
        /// </summary>
        /// <param name="transactionId">交易ID</param>
        /// <returns>返回TransactionInfo</returns>
        public TransactionExtendInfo Select(string transactionId)
        {
            return null;
        }
    }
}
