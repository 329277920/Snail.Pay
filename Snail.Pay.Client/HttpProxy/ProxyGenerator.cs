using Castle.Core.Interceptor;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// Http调用代理生成器
    /// </summary>
    public class ProxyGenerator : Castle.Core.Interceptor.IInterceptor
    {
        #region 初始化
        /// <summary>
        /// 存储拦截器列表
        /// </summary>
        private List<IInterceptor> _icpList;

        private Castle.DynamicProxy.ProxyGenerator _generator;

        private Dictionary<string, IRequestHandler> _reqHandlers;

        /// <summary>
        /// 初始化代理生成器
        /// </summary>
        /// <param name="interceptors">添加外部拦截器到拦截器队列中</param>
        public ProxyGenerator(params IInterceptor[] interceptors)
        {
            this._generator = new Castle.DynamicProxy.ProxyGenerator();

            // 添加默认拦截器
            this._icpList = new List<IInterceptor>();
            this._icpList.Add(new ProxyInterceptor());

            // 添加默认HttpContentType处理对象
            this._reqHandlers = new Dictionary<string, IRequestHandler>();
            this._reqHandlers.Add(HttpContentType.FormData, new FormHandler());
            this._reqHandlers.Add(HttpContentType.Json, new JsonHandler());
            this._reqHandlers.Add(HttpContentType.MultipartFormData, new MultipartFormHandler());
            this._reqHandlers.Add(HttpContentType.WebApi, new WebApiHandler());            
        }
       
        #endregion

        /// <summary>
        /// 创建调用代理
        /// </summary>
        /// <typeparam name="T">接口类型参数，用于定义远程服务</typeparam>            
        /// <returns>返回T接口类型的代理</returns>
        public T CreateProxy<T>() where T : class
        {
            return this._generator.CreateInterfaceProxyWithoutTarget<T>(this);
        }

        /// <summary>
        /// 注册一个拦截器到Http请求处理栈中
        /// </summary>
        /// <param name="interceptor">Http调用处理拦截器，该拦截器将添加到调用栈顶端</param>
        /// <returns></returns>
        public ProxyGenerator RegisterInterceptor(IInterceptor interceptors)
        {
            this._icpList.Insert(0, interceptors);
            return this;
        }

        /// <summary>
        /// 注册一个用于处理某个ContentType类型的IRequestHandler，如果该类型存在，将被改写
        /// </summary>
        /// <param name="contentType">Http请求类型</param>
        /// <param name="handler">处理对象</param>
        /// <returns></returns>
        public ProxyGenerator RegisterContentType(string contentType, IRequestHandler handler)
        {
            if (this._reqHandlers.ContainsKey(contentType))
            {
                this._reqHandlers.Remove(contentType);
            }
            this._reqHandlers.Add(contentType, handler);
            return this;
        }

        /// <summary>
        /// 处理Http调用
        /// </summary>
        /// <param name="invocation"></param>
        public void Intercept(IInvocation invocation)
        {
            // 获取Http调用配置
            var proxyMethod = ProxyUnity.GetSingleCustomAttribute<ProxyMethodAttribute>(invocation.Method);

            this.CheckMethodSettings(proxyMethod);

            var context = this.CreateRequestContext(proxyMethod, invocation);

            // 格式化提交参数
            context.RequestHandler.Handle(context);

            // 调用所有拦截器，处理Http请求
            foreach (var interceptor in this._icpList)
            {
                if (!interceptor.Intercept(context))
                {
                    return;
                }
            }            
        }

        #region 私有成员

        private void CheckMethodSettings(ProxyMethodAttribute proxyMethod)
        {
            if (proxyMethod == null)
            {
                throw new Exception("method is not marked as HttpProxyMethodAttribute.");
            }
            if (!this._reqHandlers.ContainsKey(proxyMethod.ContentType))
            {
                throw new Exception(proxyMethod.ContentType + " , the contenttype is undefined.");
            }
        }

        private RequestContext CreateRequestContext(ProxyMethodAttribute settings, IInvocation invocation)
        {
            var context = new RequestContext(settings, invocation);

            context.RequestHandler = this._reqHandlers[settings.ContentType];

            return context;
        }

        #endregion

    }
}
