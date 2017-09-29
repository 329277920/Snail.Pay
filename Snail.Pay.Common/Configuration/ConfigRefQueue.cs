using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Snail.Pay.Common.Configuration
{
    /// <summary>
    /// 配置文件刷新队列
    /// </summary>
    internal class ConfigRefQueue
    {
        private List<ConfigRefItem> _refList = new List<ConfigRefItem>();

        public static ConfigRefQueue Instance = new ConfigRefQueue();

        /// <summary>
        /// 配置文件刷新间隔时间（秒）
        /// </summary>
        private const int Interval = 1;

        /// <summary>
        /// 将某一刷新项添加到刷新列表中
        /// </summary>
        /// <param name="refItem"></param>
        public void Enqueue(ConfigRefItem refItem)
        {
            lock (this)
            {
                // 检测是否已经添加过
                foreach (var item in this._refList)
                {
                    if (item.FilePath == refItem.FilePath)
                    {
                        return;
                    }
                }

                this._refList.Add(refItem);
            }
        }

        /// <summary>
        /// 从刷新列表中取出一项
        /// </summary>
        /// <returns></returns>
        public ConfigRefItem Dequeue()
        {
            ConfigRefItem entity = null;

            lock (this)
            {
                foreach (var item in this._refList)
                {
                    if (item.AddTime.AddSeconds(Interval) <= DateTime.Now)
                    {
                        entity = item;
                    }
                }

                if (entity != null)
                {
                    this._refList.Remove(entity);
                }
            }

            return entity;
        }
    }
}
