using System;
using System.IO;
using System.Net;
using System.Text;

namespace ExcelReporting.Client
{
    internal class HttpClient
    {
        private readonly string basePath;
        private const int DefaultTimeout = 30 * 1000;
        
        public HttpClient(string basePath)
        {
            this.basePath = basePath;
        }

        public OperationResult<TResponse> Post<TResponse>(string path, object body, int? timeout = null)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body is null");
            }

            try
            {
                var webRequest = CreatePostRequest(path, body, timeout ?? DefaultTimeout);
                using (var webResponse = webRequest.GetResponse())
                {
                    var responseStream = webResponse.GetResponseStream();
                    if (responseStream == null)
                    {
                        throw new ArgumentNullException(nameof(responseStream), "Response doesn't contain stream");
                    }
                    using (var sr = new StreamReader(responseStream))
                    {
                        var jsonValue = sr.ReadToEnd();
                        var response = JsonCustomSerializer.Instance.Deserialize<TResponse>(jsonValue);
                        return new OperationResult<TResponse>
                        {
                            Result = response,
                            StatusCode = 200
                        };
                    }
                }
            }
            catch (Exception e)
            {
                //TODO logging
                return new OperationResult<TResponse>
                {
                    Result = default,
                    ErrorDescription = e.ToString(),
                    StatusCode = 500
                };
            }
        }

        private WebRequest CreatePostRequest<TBody>(string path, TBody body, int timeout)
            where TBody : class
        {
            var webRequest = WebRequest.Create($"{basePath}/{path}");
            webRequest.ContentType = "application/json";
            webRequest.Method = "POST";

            var serializedBody = JsonCustomSerializer.Instance.Serialize(body);
            var bytes = Encoding.UTF8.GetBytes(serializedBody);
            webRequest.ContentLength = bytes.Length;
            webRequest.Timeout = timeout;
            
            var requestStream = webRequest.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            return webRequest;
        }
    }
}