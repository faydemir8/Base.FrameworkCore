using System.Net;

namespace Base.Framework.Core.RestSharp.Interfaces
{
    public interface IRestResponse<T> : IRestResponse
    {
        new T Data { get; set; }
    }

    public interface IRestResponse
    {
        string Data { get; set; }
        HttpStatusCode StatusCode { get; set; }
    }
}