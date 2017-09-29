using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snail.Pay.Common.Configuration
{
    /// <summary>
    /// 配置文件刷新项
    /// </summary>
    internal class ConfigRefItem
    {
        /// <summary>
        /// 获取或设置类型名称
        /// </summary>
        public Type ConfigEntityType { get; set; }

        /// <summary>
        /// 获取或设置配置文件完整路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 获取或设置添加时间
        /// </summary>
        public DateTime AddTime { get; set; }


    }
}
