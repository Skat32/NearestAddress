using System.Collections.Generic;
using Newtonsoft.Json;

namespace Model.DTO.Request
{
    public class GetNearestAddressByAddressRequest
    {
        [JsonProperty("address")]
        public IEnumerable<string> Addresses { get; set; }

        [JsonProperty("forAddress")]
        public string ForAddress { get; set; }
    }
}