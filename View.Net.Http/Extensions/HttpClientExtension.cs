using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using View.Core.Extensions;
using View.Core.Models;

namespace View.Net.Http.Extensions
{
    /// <summary>
    /// <see cref="HttpClient"/> 扩展类。
    /// </summary>
    public static class HttpClientExtension
    {
        private static Action<bool, HttpStatusCode, string> ErrorAction = null;

        /// <summary>
        /// 设置 Http 错误处理方法。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="action">
        /// <para>
        /// <see cref="bool"/> : 是否是 Http 请求错误。<br />
        /// <see cref="HttpStatusCode"/> : Http 响应错误代码。<br />
        /// <see cref="string"/> : 请求错误的原因 或 响应错误的 url。<br />
        /// </para>
        /// </param>
        /// <returns></returns>
        public static HttpClient SetErrorHandle(this HttpClient client, Action<bool, HttpStatusCode, string> action)
        {
            ErrorAction = action;
            return client;
        }

        /// <summary>
        /// 添加 <see langword="application/json"/> 的请求头。
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static HttpClient AddJsonAcceptHeader(this HttpClient client)
        {
            var header = client.DefaultRequestHeaders.Accept.ToList().Find(_header => _header.MediaType == "application/json");
            if (header == null)
            { client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); }
            return client;
        }
        /// <summary>
        /// 移除 <see langword="application/json"/> 的请求头。
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static HttpClient RemoveJsonAcceptHeader(this HttpClient client)
        {
            var header = client.DefaultRequestHeaders.Accept.ToList().Find(_header => _header.MediaType == "application/json");
            if (header != null)
            { client.DefaultRequestHeaders.Accept.Remove(header); }
            return client;
        }

        /// <summary>
        /// 设置指定的 Authorization 请求标头。
        /// </summary>
        /// <param name="client"></param>
        /// <param name="scheme">scheme</param>
        /// <param name="parameter">parameter</param>
        /// <returns></returns>
        public static HttpClient SetAuthorization(this HttpClient client, string scheme, string parameter)
        {
            if (!scheme.IsNullOrWhiteSpace() && !parameter.IsNullOrEmpty() &&
                (client.DefaultRequestHeaders.Authorization == null ||
                client.DefaultRequestHeaders.Authorization.Scheme != scheme ||
                client.DefaultRequestHeaders.Authorization.Parameter != parameter))
            { client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(scheme, parameter); }
            return client;
        }
        /// <summary>
        /// 将 Authorization 请求标头置空。
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        public static HttpClient ClearAuthorization(this HttpClient client)
        {
            client.DefaultRequestHeaders.Authorization = null;
            return client;
        }


        /// <summary>
        /// 以异步方式发送一个 GET 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TResponse">接收对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> GetJsonModelAsync<TResponse>(this HttpClient client, string requestUri)
             where TResponse : class
            => await client.GetJsonModelAsync<TResponse>(requestUri, HttpCompletionOption.ResponseContentRead, new CancellationToken(false));
        /// <summary>
        /// 以异步方式发送一个 GET 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TResponse">接收对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> GetJsonModelAsync<TResponse>(this HttpClient client, Uri requestUri)
             where TResponse : class
            => await client.GetJsonModelAsync<TResponse>(requestUri, HttpCompletionOption.ResponseContentRead, new CancellationToken(false));
        /// <summary>
        /// 以异步方式用 HTTP 完成选项发送一个 GET 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TResponse">接收对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="completionOption">HTTP 完成选项值。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> GetJsonModelAsync<TResponse>(this HttpClient client, string requestUri,
            HttpCompletionOption completionOption)
             where TResponse : class
            => await client.GetJsonModelAsync<TResponse>(requestUri, completionOption, new CancellationToken(false));
        /// <summary>
        /// 以异步方式用 HTTP 完成选项发送一个 GET 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TResponse">接收对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="completionOption">HTTP 完成选项值。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> GetJsonModelAsync<TResponse>(this HttpClient client, Uri requestUri,
            HttpCompletionOption completionOption)
             where TResponse : class
            => await client.GetJsonModelAsync<TResponse>(requestUri, completionOption, new CancellationToken(false));
        /// <summary>
        /// 以异步方式用取消标记发送一个 GET 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TResponse">接收对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="cancellationToken">用以接收取消通知的取消标记。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> GetJsonModelAsync<TResponse>(this HttpClient client, string requestUri,
            CancellationToken cancellationToken)
             where TResponse : class
            => await client.GetJsonModelAsync<TResponse>(requestUri, HttpCompletionOption.ResponseContentRead, cancellationToken);
        /// <summary>
        /// 以异步方式用取消标记发送一个 GET 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TResponse">接收对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="cancellationToken">用以接收取消通知的取消标记。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> GetJsonModelAsync<TResponse>(this HttpClient client, Uri requestUri,
            CancellationToken cancellationToken)
             where TResponse : class
            => await client.GetJsonModelAsync<TResponse>(requestUri, HttpCompletionOption.ResponseContentRead, cancellationToken);
        /// <summary>
        /// 以异步方式用 HTTP 完成选项和取消标记发送一个 GET 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TResponse">接收对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="completionOption">HTTP 完成选项值。</param>
        /// <param name="cancellationToken">用以接收取消通知的取消标记。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> GetJsonModelAsync<TResponse>(this HttpClient client, string requestUri,
            HttpCompletionOption completionOption, CancellationToken cancellationToken)
            where TResponse : class
        {
            try
            {
                client.AddJsonAcceptHeader();

                var responseMessage = await client.GetAsync(requestUri, completionOption, cancellationToken);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseJson = await responseMessage.Content.ReadAsStringAsync();
                    TResponse responseModel = default;
                    if (typeof(TResponse) == typeof(string))
                    { responseModel = responseJson as TResponse; }
                    else { responseModel = JsonConvert.DeserializeObject<TResponse>(responseJson); }
                    if (responseModel != null)
                    {
                        return responseModel;
                    }
                    else
                    {
                        var uri = Uri.TryCreate(client.BaseAddress, requestUri, out Uri _uri) ? _uri.AbsoluteUri : requestUri;
                        ErrorAction?.Invoke(false, responseMessage.StatusCode, uri);
                        return default;
                    }
                }
                else
                {
                    ErrorAction?.Invoke(false, responseMessage.StatusCode, requestUri);
                    return default;
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorAction?.Invoke(true, HttpStatusCode.RequestTimeout, ex.ToString());
                return default;
            }
        }
        /// <summary>
        /// 以异步方式用 HTTP 完成选项和取消标记发送一个 GET 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TResponse">接收对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="completionOption">HTTP 完成选项值。</param>
        /// <param name="cancellationToken">用以接收取消通知的取消标记。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> GetJsonModelAsync<TResponse>(this HttpClient client, Uri requestUri,
            HttpCompletionOption completionOption, CancellationToken cancellationToken)
            where TResponse : class
        {
            try
            {
                client.AddJsonAcceptHeader();

                var responseMessage = await client.GetAsync(requestUri, completionOption, cancellationToken);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseJson = await responseMessage.Content.ReadAsStringAsync();
                    TResponse responseModel = default;
                    if (typeof(TResponse) == typeof(string))
                    { responseModel = responseJson as TResponse; }
                    else { responseModel = JsonConvert.DeserializeObject<TResponse>(responseJson); }
                    if (responseModel != null)
                    {
                        return responseModel;
                    }
                    else
                    {
                        ErrorAction?.Invoke(false, responseMessage.StatusCode, requestUri.AbsoluteUri);
                        return default;
                    }
                }
                else
                {
                    ErrorAction?.Invoke(false, responseMessage.StatusCode, requestUri.AbsoluteUri);
                    return default;
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorAction?.Invoke(true, HttpStatusCode.RequestTimeout, ex.ToString());
                return default;
            }
        }

        /// <summary>
        /// 以异步方式发送一个 POST 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TRequest">接收对象类型</typeparam>
        /// <typeparam name="TResponse">发送对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="postData">要发送的对象</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> PostJsonModelAsync<TResponse, TRequest>(this HttpClient client, string requestUri,
            TRequest postData)
            where TResponse : Entity where TRequest : Entity
            => await client.PostJsonModelAsync<TResponse, TRequest>(requestUri, postData, new CancellationToken(false));
        /// <summary>
        /// 以异步方式发送一个 POST 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TRequest">接收对象类型</typeparam>
        /// <typeparam name="TResponse">发送对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="postData">要发送的对象</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> PostJsonModelAsync<TResponse, TRequest>(this HttpClient client, Uri requestUri,
            TRequest postData)
            where TResponse : Entity where TRequest : Entity
            => await client.PostJsonModelAsync<TResponse, TRequest>(requestUri, postData, new CancellationToken(false));
        /// <summary>
        /// 以异步方式用取消标记发送一个 POST 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TRequest">接收对象类型</typeparam>
        /// <typeparam name="TResponse">发送对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="postData">要发送的对象</param>
        /// <param name="cancellationToken">用以接收取消通知的取消标记。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> PostJsonModelAsync<TResponse, TRequest>(this HttpClient client, string requestUri,
            TRequest postData, CancellationToken cancellationToken)
            where TResponse : Entity where TRequest : Entity
        {
            try
            {
                client.AddJsonAcceptHeader();

                var responseMessage = await client.PostAsync(requestUri, postData.ToFormUrlEncodedContent(), cancellationToken);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseJson = await responseMessage.Content.ReadAsStringAsync();
                    TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(responseJson);
                    if (responseModel != null)
                    {
                        return responseModel;
                    }
                    else
                    {
                        var uri = Uri.TryCreate(client.BaseAddress, requestUri, out Uri _uri) ? _uri.AbsoluteUri : requestUri;
                        ErrorAction?.Invoke(false, responseMessage.StatusCode, uri);
                        return default;
                    }
                }
                else
                {
                    ErrorAction?.Invoke(false, responseMessage.StatusCode, requestUri);
                    return default;
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorAction?.Invoke(true, HttpStatusCode.RequestTimeout, ex.ToString());
                return default;
            }
        }
        /// <summary>
        /// 以异步方式用取消标记发送一个 POST 请求到指定的 URI。
        /// </summary>
        /// <typeparam name="TRequest">接收对象类型</typeparam>
        /// <typeparam name="TResponse">发送对象类型</typeparam>
        /// <param name="client"></param>
        /// <param name="requestUri">指定的 URI</param>
        /// <param name="postData">要发送的对象</param>
        /// <param name="cancellationToken">用以接收取消通知的取消标记。</param>
        /// <exception cref="ArgumentNullException"/>
        /// <returns></returns>
        public static async Task<TResponse> PostJsonModelAsync<TResponse, TRequest>(this HttpClient client, Uri requestUri,
            TRequest postData, CancellationToken cancellationToken)
            where TResponse : Entity where TRequest : Entity
        {
            try
            {
                client.AddJsonAcceptHeader();

                var responseMessage = await client.PostAsync(requestUri, postData.ToFormUrlEncodedContent(), cancellationToken);
                if (responseMessage.IsSuccessStatusCode)
                {
                    string responseJson = await responseMessage.Content.ReadAsStringAsync();
                    TResponse responseModel = JsonConvert.DeserializeObject<TResponse>(responseJson);
                    if (responseModel != null)
                    {
                        return responseModel;
                    }
                    else
                    {
                        ErrorAction?.Invoke(false, responseMessage.StatusCode, requestUri.AbsoluteUri);
                        return default;
                    }
                }
                else
                {
                    ErrorAction?.Invoke(false, responseMessage.StatusCode, requestUri.AbsoluteUri);
                    return default;
                }
            }
            catch (HttpRequestException ex)
            {
                ErrorAction?.Invoke(true, HttpStatusCode.RequestTimeout, ex.ToString());
                return default;
            }
        }
    }
}
