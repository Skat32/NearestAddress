using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Logic.Extension;
using Logic.Interfaces;
using Newtonsoft.Json;

namespace Logic.Services
{
    public class HttpClientProxy : IHttpClientProxy
    {
        private readonly HttpClient _httpClient;

        public HttpClientProxy()
        {
            _httpClient = new HttpClient();
        }

        #region | Public methdos |
        
        #region get methods

        public async Task<T> GetAsync<T>(Uri uri, string contentType = "application/json") where T : class
        {
            _httpClient.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.ContentType), contentType);
            var response = await _httpClient.GetAsync(uri);
            
            return await GetResponseAsync<T>(response.Content);
        }

        public async Task<T> GetAsync<T>(string url, string contentType = "application/json") where T : class
        {
            return await GetAsync<T>(new Uri(url), contentType);
        }
        
        public async Task<string> GetStringAsync(string url)
        {
            return await _httpClient.GetStringAsync(url);
        }

        #endregion

        #region post methods

        public async Task<T> PostAsync<T>(Uri uri, object value, string contentType = "application/json") where T : class
        {
            var response = await GetResponseAsync(uri, value, contentType);
            
            return await GetResponseAsync<T>(response.Content);
        }

        public async Task<T> PostAsync<T>(string url, object value, string contentType = "application/json") where T : class
        {
            return await PostAsync<T>(new Uri(url), value, contentType);
        }

        public async Task PostAsync(Uri uri, object value, string contentType = "application/json")
        {
            await GetResponseAsync(uri, value, contentType);
        }

        public async Task PostAsync(string url, object value, string contentType = "application/json")
        {
            await GetResponseAsync(new Uri(url), value, contentType);
        }

        #endregion

        public void SetAuthorization(AuthenticationHeaderValue headerValue)
        {
            _httpClient.DefaultRequestHeaders.Authorization = headerValue;
        }

        #endregion

        #region | Private methods |

        private async Task<HttpResponseMessage> GetResponseAsync(Uri uri, object value,
            string contentType = "application/json")
        {
            _httpClient.DefaultRequestHeaders.Add(nameof(HttpRequestHeader.ContentType), contentType);
            var response = await _httpClient.PostAsync(uri, new StringContent(value.SerializeObject()));
            
            if (!response.IsSuccessStatusCode)
                throw new Exception(response.Content.SerializeObject());

            return response;
        }
        
        private static async Task<T> GetResponseAsync<T>(HttpContent content) where T : class
        {
            return content != null ? JsonConvert.DeserializeObject<T>(await content.ReadAsStringAsync()) : null;
        }

        #endregion
    }
}