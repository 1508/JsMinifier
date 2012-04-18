using System;
using System.Web;

namespace JsMinifier.Handler.FileResolver
{
    public interface IFileResolver
    {
        string GetFullPath(string path);
    }

    class AbsolutePathResolver : IFileResolver
    {
        public string GetFullPath(string path)
        {
            return HttpContext.Current.Server.MapPath(path);
        }
    }
}