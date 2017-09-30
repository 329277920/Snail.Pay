using Microsoft.Practices.Unity;
using Snail.Pay.Common;
using Snail.Pay.PlatformInterface;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay
{
    /// <summary>
    /// 支付平台接口工厂类
    /// </summary>
    internal class PayInterfaceFactory
    {
      
        private static string LogTitle = typeof(PayInterfaceFactory).Name;
                        
        /// <summary>
        /// 通过支付平台和支付类型获取平台接口
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="platform">所属平台</param>
        /// <param name="actionType">操作类型</param>
        /// <returns>返回接口实例</returns>
        public static T Get<T>(string platform, string actionType)
        {
            return Container.Resolve<T>(GetFormatName(platform, actionType));
        }

        /// <summary>
        /// 通过支付平台和支付类型获取平台接口
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <param name="platform">所属平台</param>
        /// <param name="actionType">操作类型</param>
        /// <returns>返回接口实例</returns>
        public static T TryGet<T>(string platform, string actionType)
        {
            try
            {
                return Container.Resolve<T>(GetFormatName(platform, actionType));
            }
            catch
            {
                return default(T);
            }           
        }

        #region 私有成员

        private static UnityContainer Container = new Lazy<UnityContainer>(() =>
        {
            var container = new UnityContainer();

            // 获取所有支付接口
            var interfaces = GetPayInterfaces();
            if (interfaces?.Length <= 0)
            {
                throw new Exception("can not found any pay interfaces.");                 
            }

            // 获取所有支付提供者
            var providers = GetPlatformProviders();
            if (providers?.Length <= 0)
            {
                throw new Exception("can not found any platform provider.");              
            }

            // 注册支付接口
            foreach (var fromType in interfaces)
            {
                foreach (var action in GetPayAction(fromType, providers))
                {
                    var name = GetPayActionRegisterName(action);
                    if (name?.Length <= 0)
                    {
                        throw new Exception("can not found register name. attribute undefined.");
                    }
                    container.RegisterType(fromType, action, name);
                }
            }

            return container;

        }, true).Value;

        /// <summary>
        /// 获取所有支付接口
        /// </summary>
        /// <returns></returns>
        private static Type[] GetPayInterfaces()
        {
            return (from type in Assembly.Load("Snail.Pay.PlatformInterface").GetTypes()
                    where type.IsInterface
                    select type).ToArray();
        }

        /// <summary>
        /// 获取支付平台提供者
        /// </summary>
        /// <returns></returns>
        private static Assembly[] GetPlatformProviders()
        {
            var providers = Config.ConfigManager.Current.GetPayPlatformProviders();
            if (providers?.Length <= 0)
            {
                throw new Exception("the pay platform providers is not configured");
            }
            List<Assembly> assemblies = new List<Assembly>();
            foreach (var file in providers)
            {                
                try
                {
                    var fullPath = PathUnity.GetFilePath(file);
                    if (fullPath?.Length <= 0)
                    {
                        throw new Exception("file is not found.");
                    }
                    assemblies.Add(Assembly.LoadFile(fullPath));
                }
                catch (Exception ex)
                {
                    Common.Log.Logger.Error(string.Format("{0} laod provider is failed,{1}.", LogTitle, file), ex);
                }
            }           
            return assemblies.ToArray();
        }

        /// <summary>
        /// 获取实现某个支付接口的类型
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="providers"></param>
        /// <returns></returns>
        private static Type[] GetPayAction(Type interfaceType, IEnumerable<Assembly> providers)
        {
            List<Type> actions = new List<Type>();
            foreach (var ass in providers)
            {
                actions.AddRange((from type in ass.GetTypes()
                                  where type.GetInterfaces().Contains(interfaceType)
                                  select type).ToArray()
                    );
            }
            return actions.ToArray();
        }

        /// <summary>
        /// 获取某个支付接口的注册名称
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        private static string GetPayActionRegisterName(Type action)
        {
            var cus = action.GetCustomAttribute<TransactionInterfaceAttribute>();
            if (cus != null)
            {
                return GetFormatName(cus.TransactionPlatform, cus.TransactionActionType);
            }
            return null;
        }

        private static string GetFormatName(string platform, string actionType)
        {
            return string.Format("Pay_{0}_{1}", platform, actionType);
        }

        #endregion
    }
}
