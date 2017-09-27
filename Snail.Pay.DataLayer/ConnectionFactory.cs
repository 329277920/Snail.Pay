using System;
using System.Data;

namespace Snail.Pay.DataLayer
{
    /// <summary>
    /// 数据库连接建立工厂类
    /// </summary>
    public sealed class ConnectionFactory
    {
        /// <summary>
        /// 获取一个新的数据库连接串
        /// </summary>
        /// <param name="isOpened">是否打开连接，默认为True</param>
        /// <returns></returns>
        public static IDbConnection NewConnection(bool isOpened)
        {
            //var conStrings = System.Configuration.ConfigurationManager.ConnectionStrings;
            //if (conStrings?.Count <= 0)
            //{
            //    throw new Exception("the database connection string is not configured");
            //}

            //IDbConnection db = null;

            //switch (conStrings[0].ProviderName)
            //{
            //    case DbProvider.Oracle:
            //        db = new Oracle
            //}             
            return null;
        }
    }
}
