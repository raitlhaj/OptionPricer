using Newtonsoft.Json;

namespace Infrastructure
{
    public interface ISerialization
    { 
    string Serialize<T>(T obj);
    T Deserialize<T>(string str );
    }
    public class Serialization: ISerialization
    {
         
        public string Serialize<T>(T obj)
        {
            return JsonConvert.SerializeObject(obj);
        }

        public T Deserialize<T>(string str)
        { 
            return JsonConvert.DeserializeObject<T>(str);
        }
    }
}