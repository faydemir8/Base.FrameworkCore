using System;
using System.Threading.Tasks;
using Base.Framework.Core.RestSharp.Enum;

namespace Base.Framework.Core.RestSharp.Interfaces
{
    public interface IRestClient
    {
        Uri BaseUrl { get; set; }
        IRestRequest Request(string resource);

        IRestResponse Get(IRestRequest request);
        IRestResponse Post(IRestRequest request);
        IRestResponse Put(IRestRequest request);
        IRestResponse Delete(IRestRequest request);
        IRestResponse Send(IRestRequest request, MethodType method);

        IRestResponse<T> Get<T>(IRestRequest request) where T : new();
        IRestResponse<T> Post<T>(IRestRequest request) where T : new();
        IRestResponse<T> Put<T>(IRestRequest request) where T : new();
        IRestResponse<T> Delete<T>(IRestRequest request) where T : new();
        IRestResponse<T> Send<T>(IRestRequest request, MethodType method) where T : new();

        Task<IRestResponse> GetAsync(IRestRequest request);
        Task<IRestResponse> PostAsync(IRestRequest request);
        Task<IRestResponse> PutAsync(IRestRequest request);
        Task<IRestResponse> DeleteAsync(IRestRequest request);
        Task<IRestResponse> SendAsync(IRestRequest request, MethodType method);

        Task<IRestResponse<T>> GetAsync<T>(IRestRequest request) where T : new();
        Task<IRestResponse<T>> PostAsync<T>(IRestRequest request) where T : new();
        Task<IRestResponse<T>> PutAsync<T>(IRestRequest request) where T : new();
        Task<IRestResponse<T>> DeleteAsync<T>(IRestRequest request) where T : new();
        Task<IRestResponse<T>> SendAsync<T>(IRestRequest request, MethodType method) where T : new();
    }
}