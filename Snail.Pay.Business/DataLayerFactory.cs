using Microsoft.Practices.Unity;
using Snail.Pay.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Business
{
    /// <summary>
    /// 数据层接口创建工厂
    /// </summary>
    public sealed class DataLayerFactory
    {
        /// <summary>
        /// 获取一个数据层的接口实例
        /// </summary>
        /// <typeparam name="T">接口类型</typeparam>
        /// <returns>返回接口实现</returns>
        public static T GetDataLayer<T>()
        {
            return Container.Resolve<T>();
        }


        #region 私有成员

        /// <summary>
        /// 延迟加载 UnityContainer
        /// </summary>
        private static UnityContainer Container = new Lazy<UnityContainer>(() =>
        {
            var container = new UnityContainer();

            var interfaces = GetPayInterfaces();
            if (interfaces == null || interfaces.Length <= 0)
            {
                throw new Exception("data layer interfaces not found.");
            }

            var assembly = GetPayDataLayerProvider();
            if (assembly == null)
            {
                throw new Exception("data layer provider not found.");
            }

            foreach (var fromType in interfaces)
            {
                var toType = GetDataLayerType(fromType, assembly);
                if (toType == null)
                {
                    throw new Exception("data layer provider do not implement interface '" + fromType.FullName + "'.");
                }
                container.RegisterType(fromType, toType);
            }

            return container;
        }, true).Value;

        /// <summary>
        /// 获取所有数据层接口
        /// </summary>
        /// <returns></returns>
        private static Type[] GetPayInterfaces()
        {
            return (from type in Assembly.Load("Snail.Pay.DataLayerInterface").GetTypes()
                    where type.IsInterface
                    select type).ToArray();
        }

        /// <summary>
        /// 获取数据层提供者
        /// </summary>
        /// <returns></returns>
        private static Assembly GetPayDataLayerProvider()
        {
            var provider = Config.ConfigManager.Current.GetPayDataLayerProvider();
            if (provider == null || provider.Length <= 0)
            {
                throw new Exception("the datalayer provider is not configured");
            }
            var fullPath = PathUnity.GetFilePath(provider);
            if (string.IsNullOrEmpty(fullPath))
            {
                throw new Exception("file is not found.");
            }
            return Assembly.LoadFile(fullPath);
        }

        /// <summary>
        /// 获取实现某个数据层接口的类型
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        private static Type GetDataLayerType(Type interfaceType, Assembly assembly)
        {
            return (from type in assembly.GetTypes()
                    where type.GetInterfaces().Contains(interfaceType)
                    select type).FirstOrDefault();
        }

        #endregion

    }
}
