using System.Threading.Tasks;
using Base.Framework.Core.Common.Extensions;
using Base.Framework.Core.RestSharp.Enum;
using RestSharp;
using IRestRequest = Base.Framework.Core.RestSharp.Interfaces.IRestRequest;
using IRestResponse = Base.Framework.Core.RestSharp.Interfaces.IRestResponse;
using RestClient = RestSharp.RestClient;


namespace Base.Framework.Core.RestSharp.Operations
{
    public class RestSharpRestClient : RestClient, Base.Framework.Core.RestSharp.Interfaces.IRestClient
    {
        public RestSharpRestClient()
        {
        }
        public RestSharpRestClient(string url) : base(url)
        {
        }

        public IRestResponse Get(IRestRequest request)
        {
            return Send(request, MethodType.GET);
        }

        public IRestResponse Post(IRestRequest request)
        {
            return Send(request, MethodType.POST);
        }

        public IRestResponse Put(IRestRequest request)
        {
            return Send(request, MethodType.PUT);
        }

        public IRestResponse Delete(IRestRequest request)
        {
            return Send(request, MethodType.DELETE);
        }

        public IRestResponse Send(IRestRequest request, MethodType method)
        {
            var restsharpRequest = (RestSharpRestRequest)request;
            restsharpRequest.Method = (Method)(int)method;
            AddHandler("application/json", new NewtonSoftJsonDeserializer());
            return Execute((RestRequest)request).CopyValuesTo<RestSharpRestResponse>();
        }

        public Interfaces.IRestResponse<T> Get<T>(IRestRequest request) where T : new()
        {
            return Send<T>(request, MethodType.GET);
        }

        public Interfaces.IRestResponse<T> Post<T>(IRestRequest request) where T : new()
        {
            return Send<T>(request, MethodType.POST);
        }

        public Interfaces.IRestResponse<T> Put<T>(IRestRequest request) where T : new()
        {
            return Send<T>(request, MethodType.PUT);
        }

        public Interfaces.IRestResponse<T> Delete<T>(IRestRequest request) where T : new()
        {
            return Send<T>(request, MethodType.DELETE);
        }

        public Interfaces.IRestResponse<T> Send<T>(IRestRequest request, MethodType method) where T : new()
        {
            var restsharpRequest = (RestSharpRestRequest)request;
            restsharpRequest.Method = (Method)(int)method;
            AddHandler("application/json", new NewtonSoftJsonDeserializer());
            return Execute<T>((RestRequest)request).CopyValuesTo<RestSharpRestResponse<T>>();
        }

        public Task<IRestResponse> GetAsync(IRestRequest request)
        {
            return SendAsync(request, MethodType.GET);
        }

        public Task<IRestResponse> PostAsync(IRestRequest request)
        {
            return SendAsync(request, MethodType.POST);
        }

        public Task<IRestResponse> PutAsync(IRestRequest request)
        {
            return SendAsync(request, MethodType.PUT);
        }

        public Task<IRestResponse> DeleteAsync(IRestRequest request)
        {
            return SendAsync(request, MethodType.DELETE);
        }

        public async Task<IRestResponse> SendAsync(IRestRequest request, MethodType method)
        {
            var restsharpRequest = (RestSharpRestRequest)request;
            restsharpRequest.Method = (Method)(int)method;
            AddHandler("application/json", new NewtonSoftJsonDeserializer());
            var response = await ExecuteTaskAsync((RestRequest)request);
            return response.CopyValuesTo<RestSharpRestResponse>();
        }

        public Task<Interfaces.IRestResponse<T>> GetAsync<T>(IRestRequest request) where T : new()
        {
            return SendAsync<T>(request, MethodType.GET);
        }

        public Task<Interfaces.IRestResponse<T>> PostAsync<T>(IRestRequest request) where T : new()
        {
            return SendAsync<T>(request, MethodType.POST);
        }

        public Task<Interfaces.IRestResponse<T>> PutAsync<T>(IRestRequest request) where T : new()
        {
            return SendAsync<T>(request, MethodType.PUT);
        }

        public Task<Interfaces.IRestResponse<T>> DeleteAsync<T>(IRestRequest request) where T : new()
        {
            return SendAsync<T>(request, MethodType.DELETE);
        }

        public async Task<Interfaces.IRestResponse<T>> SendAsync<T>(IRestRequest request, MethodType method) where T : new()
        {
            var restsharpRequest = (RestSharpRestRequest)request;
            restsharpRequest.Method = (Method)(int)method;
            AddHandler("application/json", new NewtonSoftJsonDeserializer());
            var response = await ExecuteTaskAsync<T>((RestRequest)request);
            return response.CopyValuesTo<RestSharpRestResponse<T>>();
        }

        public IRestRequest Request(string resource)
        {
            return new RestSharpRestRequest(resource);
        }
    }
}