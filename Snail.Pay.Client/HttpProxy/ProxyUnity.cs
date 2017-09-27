using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Reflection;
 

namespace HttpProxy
{
    internal class ProxyUnity
    {
        /// <summary>
        /// 获取是否将请求内容写入post流中
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool IsWriteToRequestStream(HttpRequestMessage request)
        {
            return request.Method == System.Net.Http.HttpMethod.Post || request.Method == System.Net.Http.HttpMethod.Put;
        }
        
        /// <summary>
        /// 生成多表单提交边界值
        /// </summary>
        /// <returns></returns>
        public static string GenerateMultipartBoundary()
        {
            return string.Format("--{0}{1}",
               DateTime.Now.Ticks.ToString("x"),
              _rand.Value.Next(10000, 99999));
        }

        public static T GetSingleCustomAttribute<T>(ParameterInfo parameter)
        {
            return (T)parameter.GetCustomAttributes(typeof(T), true).Take(1).SingleOrDefault();
        }

        public static T GetSingleCustomAttribute<T>(MethodInfo method)
        {
            return (T)method.GetCustomAttributes(typeof(T), true).Take(1).SingleOrDefault();
        }

        internal static System.Net.Http.HttpMethod ConvertToHttpMethod(HttpMethod method)
        {
            switch (method)
            {
                case HttpMethod.Delete:
                    return System.Net.Http.HttpMethod.Delete;
                case HttpMethod.Get:
                    return System.Net.Http.HttpMethod.Get;
                case HttpMethod.Head:
                    return System.Net.Http.HttpMethod.Head;
                case HttpMethod.Options:
                    return System.Net.Http.HttpMethod.Options;
                case HttpMethod.Post:
                    return System.Net.Http.HttpMethod.Post;
                case HttpMethod.Put:
                    return System.Net.Http.HttpMethod.Put;
                case HttpMethod.Trace:
                    return System.Net.Http.HttpMethod.Trace;
            }
            return System.Net.Http.HttpMethod.Get;
        }

        #region 私有成员

        /// <summary>
        /// 随机数生成器
        /// </summary>
        private static Lazy<Random> _rand = new Lazy<Random>(() =>
        {
            return new Random(BitConverter.ToInt32(Guid.NewGuid().ToByteArray(), 0));
        }, true);

        #endregion
    }
}
