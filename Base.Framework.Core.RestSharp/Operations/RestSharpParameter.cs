using Base.Framework.Core.RestSharp.Interfaces;

namespace Base.Framework.Core.RestSharp.Operations
{
    public class RestSharpParameter : global::RestSharp.Parameter, IParameter
    {
        public new string Value
        {
            get => base.Value.ToString();
            set => base.Value = value;
        }
    }
}