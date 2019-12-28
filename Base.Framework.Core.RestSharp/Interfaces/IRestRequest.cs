namespace Base.Framework.Core.RestSharp.Interfaces
{
    public interface IRestRequest
    {
        IRestRequest AddParameter(params IParameter[] parameters);
        IRestRequest AddBody(object body);
        IRestRequest AddCookie(string name, string value);
        IRestRequest AddFile(string name, string path, string contentType = null);
        IRestRequest AddFile(string name, byte[] bytes, string fileName, string contentType = null);
        IRestRequest AddXmlBody(object obj, string xmlNamespace);
        IRestRequest AddXmlBody(object obj);
        IRestRequest AddHeader(string name, string value);
    }
}