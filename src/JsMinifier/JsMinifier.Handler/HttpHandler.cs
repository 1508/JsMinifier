using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Autofac;
using JsMinifier.Handler.Configuration;
using JsMinifier.Handler.IoC;

namespace JsMinifier.Handler
{
    public class HttpHandler : IHttpHandler
    {
        public HttpHandler()
        {
            IoC.Container.Create(JsMinifierConfiguration.GetConfiguration());
        }

        public void ProcessRequest(HttpContext context)
        {
            var jsMinifier = IoC.Container.Instance.Resolve<JsMinifier>();

            jsMinifier.Execute();
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
