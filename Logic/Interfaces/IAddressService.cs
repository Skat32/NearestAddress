using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Interfaces
{
    public interface IAddressService
    {
        /// <summary>
        /// Получение координат по заданному адрессу
        /// </summary>
        /// <param name="address"> адресс </param>
        /// <returns></returns>
        Task<KeyValuePair<string, (double lat, double lon)>> GetPositionByAddressAsync(string address);

        /// <summary>
        /// Получаем отсортированный список по ближайшему значению адрессов для выбранно адреса
        /// </summary>
        /// <param name="address"> список адрессов, которые необходимо проверить </param>
        /// <param name="forAddress"> ближайший аддресс </param>
        /// <returns></returns>
        Task<IDictionary<string, string>> GetNearestAddressByAddressAsync(IEnumerable<string> address, string forAddress);
    }
}