using Newtonsoft.Json;
using RestSharp.Deserializers;

namespace Base.Framework.Core.RestSharp.Operations
{
  public class NewtonSoftJsonDeserializer : IDeserializer
  {
    public T Deserialize<T>(global::RestSharp.IRestResponse response)
    {
      return JsonConvert.DeserializeObject<T>(response.Content);
    }

    public string RootElement { get; set; }
    public string Namespace { get; set; }
    public string DateFormat { get; set; }
  }
}