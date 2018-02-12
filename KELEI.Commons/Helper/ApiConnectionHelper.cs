using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace KELEI.Commons.Helper
{
    public static class ApiConnectionHelper
    {
        // <summary>
        /// 执行HTTP的请求
        /// </summary>
        /// <param name="authInfo">用户验证信息</param>
        /// <param name="uri">执行的URI</param>
        /// <param name="httpMethod">HTTP方法</param>
        /// <param name="bodyParam">传入的Body参数</param>
        /// <param name="inputFormat">输入格式，即header里面的Content-Type的内容</param>
        /// <param name="outputFormat">输出格式，即header里面的Accept的内容</param>
        /// <param name="httpContent">HttpContent对象</param>
        /// <returns>执行结果</returns>
        public static HttpResponseMessage RequestHttpClient(Uri uri, Enumerator.HttpMethodEnum httpMethod, string authInfo="", string bodyParam = "", string inputFormat = "json", string outputFormat = "json", HttpContent httpContent = null)
        {
            var httpClient = CreateHttpClient(authInfo, outputFormat);
            StringContent content = null;
            if (!string.IsNullOrEmpty(bodyParam))
                content = new StringContent(bodyParam, Encoding.UTF8, string.Format("application/{0}", inputFormat));
            HttpResponseMessage response = null;
            switch (httpMethod)
            {
                case Enumerator.HttpMethodEnum.Post:
                    response = httpClient.PostAsync(uri, content).Result;
                    break;
                case Enumerator.HttpMethodEnum.Put:
                    if (httpContent != null)
                        response = httpClient.PutAsync(uri, httpContent).Result;
                    else
                        response = httpClient.PutAsync(uri, content).Result;
                    break;
                case Enumerator.HttpMethodEnum.Get:
                    response = httpClient.GetAsync(uri).Result;
                    break;
                case Enumerator.HttpMethodEnum.Delete:
                    response = httpClient.DeleteAsync(uri).Result;
                    break;
                case Enumerator.HttpMethodEnum.Patch:
                    response = httpClient.PatchAsync(uri, content);
                    break;
            }
            return response;
        }


        private static WebRequest CreatePatchRequest(string authInfo, Uri uri)
        {
            var request = WebRequest.Create(uri);
            request.Method = "Patch";
            request.ContentType = "application/json";
            if (!string.IsNullOrEmpty(authInfo))
            {
                request.Headers["Authorization"] = authInfo;
            }
            return request;
        }

        /// <summary>
        /// 创建HttpClient对象
        /// </summary>
        /// <param name="authInfo">用户验证信息</param>
        /// <param name="outputFormat">输出格式，即header里面的Accept的内容</param>
        /// <returns>HttpClient对象</returns>
        private static HttpClient CreateHttpClient(string authInfo, string outputFormat)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(string.Format("application/{0}", outputFormat)));
            if(!string.IsNullOrEmpty(authInfo))
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", authInfo);
            }
            return httpClient;
        }
    }
    public static class HttpClientExtensions
    {
        public static HttpResponseMessage PatchAsync(this HttpClient client, Uri requestUri, HttpContent httpContent)
        {
            var method = new HttpMethod("PATCH");
            var request = new HttpRequestMessage(method, requestUri)
            {
                Content = httpContent,
            };
            if(client.DefaultRequestHeaders!=null && client.DefaultRequestHeaders.Authorization!=null && !string.IsNullOrEmpty(client.DefaultRequestHeaders.Authorization.ToString()))
            {
                request.Headers.Add("Authorization", client.DefaultRequestHeaders.Authorization.ToString());
            }
            return client.SendAsync(request).Result;
        }
    }
}