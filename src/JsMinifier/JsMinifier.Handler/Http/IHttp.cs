using System.Web;

namespace JsMinifier.Handler.Http
{
    public interface IHttp
    {
        HttpContextBase Context { get; }   
    }
}