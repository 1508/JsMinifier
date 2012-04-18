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
            _http.Context.Response.AddFileDependency(this._fileResolver.GetFullPath(path));

            _http.Context.Cache.Add(path, content, null, System.Web.Caching.Cache.NoAbsoluteExpiration,
                                    TimeSpan.FromMinutes(30), CacheItemPriority.Default, null);
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