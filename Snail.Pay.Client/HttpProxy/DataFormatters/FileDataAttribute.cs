using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HttpProxy
{
    /// <summary>
    /// 文件格式
    /// </summary>
    public class FileDataAttribute : Attribute, ICustomDataFormatter
    {
        public HttpContent Format(ParameterInfo parameter, object value, RequestContext httpContext)
        {
            var filePath = value?.ToString();
            if (string.IsNullOrEmpty(filePath))
            {
                throw new Exception("the upload file not specified file path.");
            }
            var fileInfo = new FileInfo(filePath);
            if (!fileInfo.Exists)
            {
                throw new Exception("the file was not found , file path :" + fileInfo.FullName);
            }
            var fileStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Read);
            var content = new StreamContent(fileStream);

            content.Headers.Clear();
            content.Headers.Add("Content-Type", "application/octet-stream");
            content.Headers.Add("Content-Disposition",
                string.Format("form-data; name=\"{0}\"; filename=\"{1}\"",
                parameter.Name,
                System.Web.HttpUtility.UrlEncode(fileInfo.Name)));

            return content;
        }
    }
}
