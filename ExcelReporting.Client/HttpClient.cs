using System;
using System.IO;
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

        public OperationResult<TResponse> Post<TResponse>(string path, object body)
        {
            if (body == null)
            {
                throw new ArgumentNullException(nameof(body), "Body is null");
            }

            try
            {
                var webRequest = CreatePostRequest(path, body);
                var webResponse = webRequest.GetResponse();
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

        private WebRequest CreatePostRequest<TBody>(string path, TBody body)
            where TBody : class
        {
            var webRequest = WebRequest.Create($"{basePath}/{path}");
            webRequest.Headers[HttpRequestHeader.ContentType] = "application/json";
            webRequest.Method = "POST";

            var serializedBody = JsonCustomSerializer.Instance.Serialize(body);
            var requestStream = webRequest.GetRequestStream();
            var bytes = Encoding.UTF8.GetBytes(serializedBody);
            requestStream.Write(bytes, 0, bytes.Length);
            
            return webRequest;
        }
    }
}