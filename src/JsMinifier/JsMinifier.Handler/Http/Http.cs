using System;
using System.Web;

namespace JsMinifier.Handler.Http
{
    class Http : IHttp
    {
        private HttpContextBase _context;

        public HttpContextBase Context
        {
            get
            {
                if (this._context == null)
                {
                    this._context = new HttpContextWrapper(HttpContext.Current);
                }
                return this._context;
            }
        }
    }
}