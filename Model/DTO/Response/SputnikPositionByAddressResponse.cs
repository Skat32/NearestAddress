using System.Collections.Generic;
using Newtonsoft.Json;

namespace Model.DTO.Response
{
    public class SputnikPositionByAddressResponse
    {
        [JsonProperty("result")]
        public IEnumerable<Model> Result { get; set; }
    }

    public class Model
    {
        [JsonProperty("position")]
        public Position Position { get; set; }
    }

    public class Position
    {
        [JsonProperty("lat")]
        public double Lat { get; set; }
        
        [JsonProperty("lon")]
        public double Lon { get; set; } 
    }
}