using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snail.Pay.Common.Configuration
{
    /// <summary>
    /// 日志ID列表
    /// </summary>
    internal enum ConfigLogIds
    {
        /// <summary>
        /// 提示信息
        /// </summary>
        Info = 10000,

        /// <summary>
        /// 未知异常
        /// </summary>
        UnKnowError = 99999,

        /// <summary>
        /// 首次加载配置文件
        /// </summary>
        LoadConfigSucess = 10000,

        /// <summary>
        /// 刷新配置文件
        /// </summary>
        RefConfigSucess = 10010,

        /// <summary>
        /// 配置文件未找到
        /// </summary>
        FileIsNotFind = 10020,

        /// <summary>
        /// 配置文件类型错误，不继承IConfigFile接口
        /// </summary>
        TypeError = 10030,

        /// <summary>
        /// 在调用IConfigFile接口方法时异常
        /// </summary>
        IFileConfigError = 10040,

        /// <summary>
        /// 读取文件异常
        /// </summary>
        ReadFileError = 10050,

        /// <summary>
        /// 在配置文件发生变更时，没有从缓存中读取配置实体
        /// </summary>
        CacheIsNotFind = 10060,


    }
}
