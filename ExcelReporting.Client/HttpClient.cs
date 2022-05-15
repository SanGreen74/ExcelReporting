using System;
using System.Net;
using System.Text;

namespace ExcelReporting.Client
{
    internal class HttpClient
    {
        private readonly string basePath;

        public HttpClient(string basePath)
        {
            this.basePath = basePath;
        }

        public HttpWebResponse Post<T>(string path, T body)
            where T : class
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body is null");
            }

            var serializedBody = JsonCustomSerializer.Instance.Serialize(body);
            var webRequest = WebRequest.Create($"{basePath}/{path}");
            webRequest.Headers[HttpRequestHeader.ContentType] = "application/json";
            webRequest.Method = "POST";
            var requestStream = webRequest.GetRequestStream();
            var bytes = Encoding.UTF8.GetBytes(serializedBody);
            requestStream.Write(bytes, 0, bytes.Length);

            var webResponse = webRequest.GetResponse();
            return (HttpWebResponse) webResponse;
        }
    }
}