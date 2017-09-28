using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snail.Pay.Platform.Zfb
{
    /// <summary>
    /// 支付平台配置信息
    /// </summary>
    internal class PayConfig
    {
        private PayConfig()
        {
#if DEBUG
            AppId = "2016081900289850";
            PayUrl = "https://openapi.alipaydev.com/gateway.do";
            RsaPublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAs+eXNM0I350Pe1uBvqwpCW6WBusx1bH6sxAw6DWhnaOGKGvkh2/DmVb1odS+53KOgKvW7csqtykXwiHig+4rxEpRQIFQFpCVHeNtavkf7lDR0qCnVRHwJf1KWwQ+wjFHFY5G2J4s5Ww98SwpsaZi5qaj3GHtofzUUOHIh41DZU+njyFwfuAZ8GiF7iWAwTQ+MHO+oWPbSN75xhQjj3TdB5m+vNNo1xykVvOGhrXhV6niIJHQCeSOgUIeQVmWkJT0TphsIZf/ot4tbiTKCplQyWGTc5Z/W8296v7KQ2YQGqaRsZV6tBpEN7YdX+rvf8yQdfXjW0TBMscHb1zfbvaqOQIDAQAB";
            RsaPrivateKey = "MIIEpQIBAAKCAQEAr8Amc3VgbeKBCJHaYYbEUsWcc68w21+uaCmOPSDEQ60b4hajLaepoLrmiO54DPSZ9/vrihXHSlHaA9Yx7UMM1MatgXK0qumBDV14Ux/3WinHWD3w5YJGpwcL3EVLXqC/YDq9yQBC1Dsix8fp4kGciaBo6UsvgiZJd6mHFPZQn3ZsKPDL11TtsnKmlqqZ1j6lkCHEOddm+Cf8sFGZNZye9zk4REHEZZVURnkRnv571oW5pTDdsiyad7Dc0sI2xNtmnkwGESofx36caAPEPprpQ72ZQLsYZCrKR4GnTYR2QP1+NwSbLrd/7rasfUpphOrezjprIJGo+K4ZI0bIzXPyLQIDAQABAoIBAQCvjriaEsDNYznTjqZfT1ii+gSrRm/+Yth78i7EfwuuMqisskI0I8v841XPqK5A+sEmhv4kxFZ7tcGanYbn41dY4FGHAkbYh0HDUQAVwNO7vAoF6nHNQNJEnHRLhuif0OD6RhM4Skt3Zs76U4cc3L96dsL1b4Y4cvhmUPC/jsDAV8hxBVhXFeaqJ/bJFNqUqRN7e4YFsXvj+S8mCjqYtTeLAeoWhLzqh7C4PucXUwdwYbz3IGtwWZx3vFM2UgN+JceKivJk/H5nEXzCtzDfe96RyMlOqGYVga0rxmKldbO4FgJEEwEzXYpE/pA3J17oG0kzK0Q29rAoKpOFoOyElHRhAoGBAOhfXFg1W3rXOxCWVvQTcaG9urR8vhjcPX7obJ3NOovpdstKx+K/d+fcQfl8iqHGwlo5/n04PwxwVst+tBKqxrfEHO/swTjdFk6iHcnkASM/BUWt0ysxRB5BbA3SMgQXn8PgcR7nieokXe8qad4HSSHJFcmB1eYjErIA5vhs6nZVAoGBAMGe7NXIw/dF4op5FX/cxo/jsNmy5PgClGPHdXvZEYN1tcxggKEV8NTMOqfJEPDdrU3zfMqMk61IrGQ2ZbCR4KK6f8+X6QwRJiiZYq5Z2NJzg7Li5WfB/JhtMqQdbQ5c5krck2D5PDo3aifxuwQFlUBAFYMLBeRGBtk+0UGxdfR5AoGAec3qyO4HKsgBVPuJTt2WVdLvOEafGsbvkUNiFAGM6+QP+hYT2t7EiowhJRbMUGqwW71EkfzWx71nMboTyCkuiwtIo9c1nYn+dG90L+zNT91r9Q9dvqlPbJCchE4nG5AKlhFjGnECEPLJLmHMJq8o/YW7Xuoo5j6CQT5J0/S7nhUCgYEArn9EgweFVX0uRg4KSkn2ygcyg4CuyrXIdoR0ZUGkfw2+4oq2YncbrSzCUCTtl6axYednOESpcypj84zy6McP5JigR79o0O9DrKNQREHFHyXsM3Q5u+EgfV8snKvIdYFUK3PPfz4gAXefvJAnM+C0OkuHF6r/jFNwKKpsfQAqhkECgYEAvUcI4YGl2dCObQk9V9L5bv0xpbmk5xKiMIIIkDU6TgaFs4IR3h1Z8e6IugxcwpiH8KLdBzUHI2b1HVeMArVgdBgK7Fi5lDNJhfXmzRdrgZOulS9e2G7n8CIDTgJWqPpAIayGp0iEo2dI8DQvDnpsFYkTntGBJmGozX3ATYyjwXY=";
            NotifyUrl = "http://192.168.10.82/notify/zfb";
            ReturnUrl = "http://192.168.10.82/return/zfb";
            EncryptType = "RSA2";
            DataFormat = "json";
            Version = "1.0";
            Charset = "utf-8";
            Title = "测试商品";
            Description = "这是一个测试商品";
            ExpiredTime = 24 * 60 * 15;
#else

#endif

        }

        public static PayConfig Instance = new PayConfig();

        /// <summary>
        /// APPID
        /// </summary>
        public string AppId { get; set; }

        /// <summary>
        /// 支付网关
        /// </summary>
        public string PayUrl { get; set; }

        /// <summary>
        /// RSA公钥（支付宝）
        /// </summary>
        public string RsaPublicKey { get; set; }

        /// <summary>
        /// RSA私钥，商户
        /// </summary>
        public string RsaPrivateKey { get; set; }

        /// <summary>
        /// 异步通知地址
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// 同步通知地址
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// 加密类型（RSA2，AES）
        /// </summary>
        public string EncryptType { get; set; }

        /// <summary>
        /// 数据格式，默认为json
        /// </summary>
        public string DataFormat { get; set; }

        /// <summary>
        /// 接口版本，默认为1.0
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// 字符编码格式
        /// </summary>
        public string Charset { get; set; }
     
        /// <summary>
        /// 交易描述（可以由业务系统传入）
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 商品标题（可以由业务系统传入）
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 交易过期时间（单位分钟）
        /// </summary>
        public int ExpiredTime { get; set; }
        
    }
}
