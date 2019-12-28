using Base.Framework.Core.RestSharp.Interfaces;

namespace Base.Framework.Core.RestSharp.Operations
{
    public class RestSharpRestRequest : global::RestSharp.RestRequest, IRestRequest
    {
        public RestSharpRestRequest(string resource) : base(resource)
        {
        }

        public IRestRequest AddParameter(params IParameter[] parameters)
        {
            foreach (var parameter in parameters)
            {
                AddQueryParameter(parameter.Name, parameter.Value);
            }
            return this;
        }

        IRestRequest IRestRequest.AddBody(object body)
        {
            AddJsonBody(body);
            return this;
        }

        public new IRestRequest AddCookie(string name, string value)
        {
            base.AddCookie(name, value);
            return this;
        }

        public new IRestRequest AddFile(string name, string path, string contentType = null)
        {
            base.AddFile(name, path, contentType);
            return this;
        }

        IRestRequest IRestRequest.AddFile(string name, byte[] bytes, string fileName, string contentType)
        {
            AddFile(name, bytes, fileName, contentType);
            return this;
        }

        public new IRestRequest AddXmlBody(object obj, string xmlNamespace)
        {
            base.AddXmlBody(obj, xmlNamespace);
            return this;
        }

        public new IRestRequest AddXmlBody(object obj)
        {
            base.AddXmlBody(obj);
            return this;
        }

        public new IRestRequest AddHeader(string name, string value)
        {
            base.AddHeader(name, value);
            return this;
        }
    }
}