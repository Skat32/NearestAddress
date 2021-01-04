using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IHttpClientProxy
    {
        /// <summary>
        /// Получить результат выполнения Get запроса
        /// </summary>
        Task<T> GetAsync<T>(Uri uri, string contentType = "application/json") where T : class;

        /// <summary>
        /// Получить результат выполнения Get запроса
        /// </summary>
        Task<T> GetAsync<T>(string url, string contentType = "application/json") where T : class;

        /// <summary>
        /// Получить результат выполнения Get запроса
        /// </summary>
        Task<string> GetStringAsync(string url);

        Task<T> PostAsync<T>(Uri uri, object value, string contentType = "application/json") where T : class;
        
        Task<T> PostAsync<T>(string url, object value, string contentType = "application/json") where T : class;

        Task PostAsync(Uri uri, object value, string contentType = "application/json");
        
        Task PostAsync(string url, object value, string contentType = "application/json");

        void SetAuthorization(AuthenticationHeaderValue headerValue);
    }
}