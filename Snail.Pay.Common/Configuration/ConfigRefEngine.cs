using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snail.Pay.Common.Configuration
{
    /// <summary>
    /// 配置文件刷新引擎
    /// </summary>
    internal class ConfigRefEngine
    {
        /// <summary>
        /// 配置文件刷新间隔时间（秒）
        /// </summary>
        private const int Interval = 1;

        private static int IsStart = 0;

        private ConfigRefEngine() { }

        internal static ConfigRefEngine Instance = new ConfigRefEngine();

        /// <summary>
        /// 记录一次循环中加载失败的项
        /// </summary>
        private List<ConfigRefItem> _errItems;

        /// <summary>
        /// 开启刷新任务
        /// </summary>
        public void Start()
        {
            if (Interlocked.Increment(ref IsStart) >= 2)
            {
                return;
            }

            new Task(() => 
            {
                while (true)
                {
                    this._errItems = new List<ConfigRefItem>();

                    // 从队列中取出需要刷新的配置文件
                    ConfigRefItem refItem = null;

                    while ((refItem = ConfigRefQueue.Instance.Dequeue()) != null)
                    {
                        var obj = ConfigurationManager.LoadConfig(refItem.FilePath, refItem.ConfigEntityType);
                        
                        if (obj == null)
                        {
                            _errItems.Add(refItem);
                        }
                        else
                        {
                            // 刷新缓存
                            ConfigurationManager.Set(refItem.FilePath, obj);
                        }
                    }

                    // 加载失败，重新添加到队列中
                    foreach (var item in this._errItems)
                    {
                        ConfigRefQueue.Instance.Enqueue(item);
                    }
                     
                    Thread.Sleep(Interval * 1000);
                }
            }).Start();
        }
    }


}
