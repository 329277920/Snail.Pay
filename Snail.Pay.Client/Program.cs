using HttpProxy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            TestCreateOrder();

            // TestNotify();

            // TestQuery();
          
            Console.ReadKey();
        }

        /// <summary>
        /// 测试创建订单
        /// </summary>
        static void TestCreateOrder()
        {
            var pays = new ProxyGenerator().RegisterInterceptor(new DefaultIntercept()).
              CreateProxy<IPayService>();

            dynamic order = new System.Dynamic.ExpandoObject();
            order.Amount = 1;
            order.Description = "这是一个测试商品";
            order.ExpiredTime = 30;
            order.OrderNo = DateTime.Now.Millisecond.ToString();
            order.Title = "测试商品";
            order.AppId = "1001";
            order.ProductId = 1001;

            // 支付宝h5创建订单
            // var payInfo1 = pays.InitiatePay("zfb", "h5", order).Result;

            // 微信创建订单
            var payInfo1 = pays.InitiatePay("wx", "qrcode", order).Result;

            Console.WriteLine(payInfo1);
        }

        /// <summary>
        /// 测试后端回调
        /// </summary>
        static void TestNotify()
        {
            var pays = new ProxyGenerator().RegisterInterceptor(new DefaultIntercept()).
            CreateProxy<IPayService>();

            var result = pays.Notify("zfb", "notify", "xx0001", 100).Result;

            Console.WriteLine(result);
        }

        private static object _lock = new object();

        /// <summary>
        /// 测试查询
        /// </summary>
        static void TestQuery()
        {
            var pays = new ProxyGenerator().RegisterInterceptor(new DefaultIntercept()).CreateProxy<IPayService>();

            var items = new Queue<string>();
            for (var i = 1; i <= 10000; i++)
            {
                items.Enqueue("XXX00000" + i.ToString());                 
            }

            for (var i = 0; i < 100; i++)
            {
                new System.Threading.Thread(() => 
                {
                    while (true)
                    {
                        string item = "";
                        lock (_lock)
                        {
                            if (items.Count > 0)
                            {
                                item = items.Dequeue();
                            }

                        }
                        if (item == "")
                        {
                            break;
                        }
                        var result = pays.QueryAsync(new
                        {
                            TransactionId = item
                        }).Result;
                    }                    

                }).Start();
            }

            Stopwatch watch = new Stopwatch();
            watch.Start();
            //System.Threading.Tasks.Parallel.ForEach<string>(items.ToArray(), item =>
            //{
            //    pays.QueryAsync(new
            //    {
            //        TransactionId = item
            //    });
            //});
            watch.Stop();
            Console.WriteLine(watch.ElapsedMilliseconds);

            //var result = pays.Query(new
            //{
            //    TransactionId = "XXX00001"
            //}).Result;

            //Console.WriteLine(result);
        }
    }
}
