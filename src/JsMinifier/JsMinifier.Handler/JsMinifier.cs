using System;
using JsMinifier.Handler.Engine;
using JsMinifier.Handler.Http;
using JsMinifier.Handler.Logger;
using JsMinifier.Handler.Reader;
using JsMinifier.Handler.Response;

namespace JsMinifier.Handler
{
    public class JsMinifier
    {
        private readonly ILogger _logger;
        private readonly IHttp _http;
        private readonly IReader _reader;
        private readonly IResponse _response;
        private readonly Func<IEngineFactory> _engineFactory;

        public JsMinifier()
        {
            this._logger = new NullLogger();
        }

        public JsMinifier(ILogger logger, IHttp http,Reader.IReader reader, Response.IResponse response, Func<IEngineFactory> engineFactory)
        {
            _logger = logger;
            _http = http;
            _reader = reader;
            _response = response;
            _engineFactory = engineFactory;
        }

        public void Execute()
        {
            var jsPath = this._http.Context.Request.Url.LocalPath;

            var source = this._reader.ReadContent(jsPath);

            this._response.WriteJs(this._engineFactory().GetEngine().Run(source, jsPath));
        }
    }
}