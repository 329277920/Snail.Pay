using Snail.Pay.Common.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Platform.Wx
{
    /// <summary>
    /// 微信支付配置
    /// </summary>
    public class PayConfig
    {
        #region 私有成员

        private static string FilePath
        {
            get { return Common.PathUnity.GetFilePath("configs/Wx.PayConfig.xml"); }
        }         

        #endregion
         

        /// <summary>
        /// 获取当前的配置文件实例
        /// </summary>
        public static PayConfig Current
        {
            get
            {
                if (string.IsNullOrEmpty(FilePath))
                {
                    throw new Model.KnownException("can not found the file with path 'configs/Wx.PayConfig.xml'.");
                }
                return ConfigurationManager.Get<PayConfig>(FilePath);
            }
        }

        /// <summary>
        /// 交易过期时间（单位分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
    }
}
