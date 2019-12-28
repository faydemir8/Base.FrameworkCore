using System.Net;
using Base.Framework.Core.RestSharp.Interfaces;

namespace Base.Framework.Core.RestSharp.Operations
{
    public class RestSharpRestResponse : global::RestSharp.RestResponse, IRestResponse
    {
        public string Data
        {
            get => Content;
            set => Content = value;
        }


        public HttpStatusCode Status
        {
            get => StatusCode;
            set => StatusCode = value;
        }
    }

    public class RestSharpRestResponse<T> : global::RestSharp.RestResponse<T>, IRestResponse<T>
    {
        public new T Data
        {
            get => base.Data;
            set => base.Data = value;
        }

        string IRestResponse.Data
        {
            get => Content;
            set => Content = value;
        }

    }
}