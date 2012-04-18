using System.Linq;
using JsMinifier.Handler.Configuration;
using JsMinifier.Handler.Engine.Minify;
using JsMinifier.Handler.Engine.Plain;
using JsMinifier.Handler.Http;

namespace JsMinifier.Handler.Engine
{
    class IncludeExcludeEngineFactory : IEngineFactory
    {
        private readonly IPlainEngine _plainEngine;
        private readonly IMinifyEngine _minifyEngine;
        private readonly IHttp _http;
        private readonly JsMinifierConfiguration _configuration;

        public IncludeExcludeEngineFactory(IPlainEngine plainEngine, Engine.Minify.IMinifyEngine minifyEngine, Http.IHttp http, Configuration.JsMinifierConfiguration configuration)
        {
            _plainEngine = plainEngine;
            _minifyEngine = minifyEngine;
            _http = http;
            _configuration = configuration;
        }

        public IEngine GetEngine()
        {
            string localPath = this._http.Context.Request.Url.LocalPath;

            if(this._configuration.Excludes.Any(t => localPath.Contains(t)))
            {
                return this._plainEngine;
            }
            else
            {
                return this._minifyEngine;
            }

            return this._minifyEngine;
        }
    }
}