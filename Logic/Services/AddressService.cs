using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Logic.Interfaces;
using Model.DTO.Response;
using Model.Options;
using System.Device.Location;

namespace Logic.Services
{
    public class AddressService : IAddressService
    {
        private readonly IHttpClientProxy _httpClientProxy;
        private readonly Uri _uri;
        
        public AddressService(IHttpClientProxy httpClientProxy, AddressFinderOptions finderOptions)
        {
            _httpClientProxy = httpClientProxy;
            _uri = finderOptions.Uri;
        }

        public async Task<KeyValuePair<string, (double lat, double lon)>> GetPositionByAddressAsync(string address)
        {
            var result = await _httpClientProxy.GetAsync<SputnikPositionByAddressResponse>(_uri + address);

            return new KeyValuePair<string, (double lat, double lon)>(address, 
                (result.Result.First().Position.Lat, result.Result.First().Position.Lon));
        }

        public async Task<IDictionary<string, string>> GetNearestAddressByAddressAsync(IEnumerable<string> address, string forAddress)
        {
            var (_, (lat1, lon1)) = await GetPositionByAddressAsync(forAddress);

            var positions = new Dictionary<string, double>();

            foreach (var s in address.Distinct())
            {
                var (key, (lat, lon)) = await GetPositionByAddressAsync(s);

                var distance = Distance(lat1, lon1, lat,
                    lon);
                
                positions.Add(key, distance);
            }
            
            return positions.OrderBy(pair => pair.Value).ToDictionary(pair => pair.Key,
                pair => $"{(pair.Value < 1000 ? pair.Value : pair.Value / 1000):F} {(pair.Value < 1000 ? "м." : "км.")}");
        }

        /// <summary>
        /// Получаем дистанцию от одной до другой координаты в метрах
        /// </summary>
        /// <param name="lat1"></param>
        /// <param name="lon1"></param>
        /// <param name="lat2"></param>
        /// <param name="lon2"></param>
        /// <returns></returns>
        private static double Distance(double lat1, double lon1, double lat2, double lon2)
        {
            return new GeoCoordinate(lat1, lon1).GetDistanceTo(new GeoCoordinate(lat2, lon2));
        }
    }
}