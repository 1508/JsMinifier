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
        private readonly IEngine _engine;
        private readonly IReader _reader;
        private readonly IResponse _response;

        public JsMinifier()
        {
            this._logger = new NullLogger();
        }

        public JsMinifier(ILogger logger, IHttp http, Engine.IEngine engine, Reader.IReader reader, Response.IResponse response)
        {
            _logger = logger;
            _http = http;
            _engine = engine;
            _reader = reader;
            _response = response;
        }

        public void Execute()
        {
            var jsPath = this._http.Context.Request.Url.LocalPath;

            var source = this._reader.ReadContent(jsPath);

            this._response.WriteJs(this._engine.Run(source, jsPath));
        }
    }
}