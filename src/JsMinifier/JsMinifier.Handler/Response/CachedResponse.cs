using System;
using System.Web;
using JsMinifier.Handler.Cache;
using JsMinifier.Handler.Http;

namespace JsMinifier.Handler.Response
{
    class CachedResponse : NonCachedResponse
    {
        private readonly IHttp _http;

        public CachedResponse(IHttp http, ICache cache, bool isCompressionHandledByResponse) : base(http, isCompressionHandledByResponse)
        {
            _http = http;
        }

        public override void WriteJs(string js)
        {
            _http.Context.Response.Cache.SetCacheability(HttpCacheability.Public);
            _http.Context.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(60*60));
            _http.Context.Response.Cache.SetETagFromFileDependencies();
            _http.Context.Response.Cache.SetLastModifiedFromFileDependencies();

            //response.Cache.SetOmitVaryStar(true);
            _http.Context.Response.Cache.SetVaryByCustom("Accept-Encoding");
            base.WriteJs(js);
        }

    }
}