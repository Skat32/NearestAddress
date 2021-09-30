using System;
using System.Net;
using System.Net.Http;
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
            if (content is null)
                return null;
            
            var text = await content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(text);
        }
    }
}