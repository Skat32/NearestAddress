using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IHttpClientProxy
    {
        /// <summary>
        /// Получить результат выполнения Get запроса
        /// </summary>
        Task<T> GetAsync<T>(string url, string contentType = "application/json") where T : class;
    }
}