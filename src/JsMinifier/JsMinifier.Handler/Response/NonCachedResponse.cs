using System;
using System.IO.Compression;
using JsMinifier.Handler.Http;

namespace JsMinifier.Handler.Response
{
    class NonCachedResponse : IResponse
    {
        private readonly IHttp _http;
        private readonly bool _isCompressionHandledByResponse;

        public NonCachedResponse(IHttp http, bool isCompressionHandledByResponse)
        {
            _http = http;
            _isCompressionHandledByResponse = isCompressionHandledByResponse;
        }

        public virtual void WriteJs(string js)
        {
            if(this._isCompressionHandledByResponse)
                this.HandleCompression();

            var response = this._http.Context.Response;
            response.ContentType = "application/javascript";
            response.Write(js);
        }

        /// <summary>
        ///  deal with the request accept encoding and add the necessary filter to the response
        /// </summary>
        protected void HandleCompression()
        {
            var context = this._http.Context;

            /// load encodings from header
            QValueList encodings = new QValueList(context.Request.Headers["Accept-Encoding"]);

            /// get the types we can handle, can be accepted and
            /// in the defined client preference
            QValue preferred = encodings.FindPreferred("gzip", "deflate", "identity");

            /// if none of the preferred values were found, but the
            /// client can accept wildcard encodings, we'll default
            /// to Gzip.
            if (preferred.IsEmpty && encodings.AcceptWildcard && encodings.Find("gzip").IsEmpty)
            {
                preferred = new QValue("gzip");
            }

            // handle the preferred encoding
            switch (preferred.Name.ToLowerInvariant())
            {
                case "gzip":
                    context.Response.AppendHeader("Content-Encoding", "gzip");
                    context.Response.Filter = new GZipStream(context.Response.Filter, CompressionMode.Compress);
                    break;
                case "deflate":
                    context.Response.AppendHeader("Content-Encoding", "deflate");
                    context.Response.Filter = new DeflateStream(context.Response.Filter, CompressionMode.Compress);
                    break;
                case "identity":
                default:
                    break;
            }
        }
    }
}