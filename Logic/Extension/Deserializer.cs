using Newtonsoft.Json;

namespace Logic.Extension
{
    public static class Deserializer
    {
        public static object DeserializeObject(this object obj)
        {
            return JsonConvert.DeserializeObject(obj.ToString() ?? string.Empty,
                new JsonSerializerSettings { Formatting = Formatting.Indented });
        }

        public static string SerializeObject(this object obj, NullValueHandling nullValueHandling = NullValueHandling.Ignore, DefaultValueHandling defaultValueHandling = DefaultValueHandling.Include)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, new JsonSerializerSettings{NullValueHandling = nullValueHandling, DefaultValueHandling = defaultValueHandling});
        }

        public static T DeserializeObject<T>(this object obj)
        {
            return JsonConvert.DeserializeObject<T>(obj.ToString() ?? string.Empty,
                new JsonSerializerSettings {Formatting = Formatting.Indented});
        }

        public static T DeserializeString<T>(this string str)
        {
            return JsonConvert.DeserializeObject<T>(str, new JsonSerializerSettings {Formatting = Formatting.Indented});
        }
    }
}