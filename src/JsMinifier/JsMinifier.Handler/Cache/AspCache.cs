using System;
using System.Web.Caching;
using JsMinifier.Handler.FileResolver;
using JsMinifier.Handler.Http;

namespace JsMinifier.Handler.Cache
{
    class AspCache : ICache
    {
        private readonly IHttp _http;
        private readonly IFileResolver _fileResolver;

        public AspCache(IHttp http, IFileResolver fileResolver)
        {
            _http = http;
            _fileResolver = fileResolver;
        }

        public void Insert(string path, string content)
        {
            string filePath = this._fileResolver.GetFullPath(path);
            _http.Context.Response.AddFileDependency(filePath);

            _http.Context.Cache.Insert(path, content, new CacheDependency(filePath));
        }

        public string Get(string path)
        {
            return _http.Context.Cache[path] as string;
        }

        public bool Exists(string path)
        {
            return this.Get(path) != null;
        }
    }
}