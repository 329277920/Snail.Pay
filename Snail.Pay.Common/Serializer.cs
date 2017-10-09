using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Serialization;

namespace Snail.Pay.Common
{
    public class Serializer
    {
        /// <summary>
        /// xml序列化
        /// </summary>
        /// <param name="entity">实体类型</param>
        /// <returns>返回字节数组</returns>
        public static async Task<string> XmlSerializeAsync<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            using (Stream stream = new MemoryStream())
            {
                new XmlSerializer(typeof(T)).Serialize(stream, entity);
                stream.Position = 0;
                var buffer = new byte[(int)stream.Length];
                await stream.ReadAsync(buffer, 0, buffer.Length);
                return Encoding.Unicode.GetString(buffer);
            }
        }

        /// <summary>
        /// xml序列化
        /// </summary>
        /// <param name="entity">实体类型</param>
        /// <returns>返回字节数组</returns>
        public static string XmlSerialize<T>(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            using (Stream stream = new MemoryStream())
            {
                new XmlSerializer(typeof(T)).Serialize(stream, entity);
                stream.Position = 0;
                var buffer = new byte[(int)stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return Encoding.Unicode.GetString(buffer);
            }
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <param name="entity">实体类型</param>
        /// <returns>返回字节数组</returns>
        public static Task<T> XmlDeserializeAsync<T>(string content)
        {
            return Task.Factory.StartNew<T>(() =>
            {
                using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
                {
                    var serializer = new XmlSerializer(typeof(T));
                    return (T)serializer.Deserialize(stream);
                }
            });
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <param name="entity">实体类型</param>
        /// <returns>返回字节数组</returns>
        public static T XmlDeserialize<T>(string content)
        {
            using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);
            }
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <param name="entity">实体类型</param>
        /// <param name="entityType">实体对象类型</param>
        /// <returns>返回字节数组</returns>
        public static Task<object> XmlDeserializeAsync(string content, Type entityType)
        {
            return Task.Factory.StartNew<object>(() =>
            {
                using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
                {
                    var serializer = new XmlSerializer(entityType);
                    return serializer.Deserialize(stream);
                }
            });
        }

        /// <summary>
        /// xml反序列化
        /// </summary>
        /// <param name="entity">实体类型</param>
        /// <param name="entityType">实体对象类型</param>
        /// <returns>返回字节数组</returns>
        public static object XmlDeserialize(string content, Type entityType)
        {
            using (Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                var serializer = new XmlSerializer(entityType);
                return serializer.Deserialize(stream);
            }
        }


        private static JsonSerializerSettings _jsonSettings = new JsonSerializerSettings();

        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="entity">对象实体</param>
        /// <returns>返回Json串</returns>
        public static Task<string> JsonSerializeAsync(object entity)
        {
            return Task.Factory.StartNew<string>(() =>
            {
                return JsonConvert.SerializeObject(entity, _jsonSettings);
            });
        }

        /// <summary>
        /// json序列化
        /// </summary>
        /// <param name="entity">对象实体</param>
        /// <returns>返回Json串</returns>
        public static string JsonSerialize(object entity)
        {
            return JsonConvert.SerializeObject(entity, _jsonSettings);
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="json">Json串对象实体</param>
        /// <returns>返回对象实体</returns>
        public static Task<object> JsonDeserializeAsync(string json)
        {
            return Task.Factory.StartNew<object>(() =>
            {
                return JsonConvert.DeserializeObject(json);
            });
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="json">Json串对象实体</param>
        /// <returns>返回对象实体</returns>
        public static object JsonDeserialize(string json)
        {
            return JsonConvert.DeserializeObject(json);
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="json">Json串对象实体</param>
        /// <param name="type">需要转化的类型</param>
        /// <returns>返回对象实体</returns>
        public static object JsonDeserialize(string json, Type type)
        {
            return JsonConvert.DeserializeObject(json, type);
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="json">Json串对象实体</param>
        /// <returns>返回对象实体</returns>
        public static Task<T> JsonDeserializeAsync<T>(string json)
        {
            return Task.Factory.StartNew<T>(() =>
            {
                return JsonConvert.DeserializeObject<T>(json);
            });
        }

        /// <summary>
        /// json反序列化
        /// </summary>
        /// <param name="json">Json串对象实体</param>
        /// <returns>返回对象实体</returns>
        public static T JsonDeserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        /// <summary>
        /// 将实体序列化为二进制
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <returns>返回字节数组</returns>
        public static byte[] BinarySerialize(object entity)
        {
            using (var stream = new MemoryStream())
            {
                new BinaryFormatter().Serialize(stream, entity);
                return stream.GetBuffer();
            }
        }

        /// <summary>
        /// 将字节数组反序列化为实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="buffer">字节数组</param>
        /// <returns>返回实体对象</returns>
        public static T BinaryDeserialize<T>(byte[] buffer)
        {
            return (T)BinaryDeserialize(buffer);
        }

        /// <summary>
        /// 将字节数组反序列化为实体
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="buffer">字节数组</param>
        /// <returns>返回实体对象</returns>
        public static object BinaryDeserialize(byte[] buffer)
        {
            using (var stream = new MemoryStream())
            {
                stream.Write(buffer, 0, buffer.Length);
                stream.Position = 0;
                return new BinaryFormatter().Deserialize(stream);
            }
        }

        /// <summary>
        /// 将Querystring字符串转化成Dictionary<string, string>
        /// </summary>
        /// <param name="queryString"></param>
        /// <returns></returns>
        public static Dictionary<string, string> DictionaryDeserialize(string queryString)
        {
            var dic = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(queryString))
            {
                return dic;
            }
            foreach (var kv in queryString.Split(new char[] { '&' }, StringSplitOptions.RemoveEmptyEntries))
            {
                var kvs = kv.Split('=');
                if (kvs.Length != 2)
                {
                    continue;
                }
                dic.Add(kvs[0], HttpUtility.UrlDecode(kvs[1]));
            }
            return dic;
        }
    }
}
