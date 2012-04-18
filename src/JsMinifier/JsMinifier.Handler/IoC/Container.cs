using Autofac;
using JsMinifier.Handler.Configuration;
using JsMinifier.Handler.Logger;

namespace JsMinifier.Handler.IoC
{
    public class Container
    {
        private static IContainer _instance;
        public static IContainer Instance
        {
            get
            {
                if (_instance == null)
                {
                    Create(JsMinifierConfiguration.GetDefault());
                }
                return _instance;
            }
            set { _instance = value; }
        }



        public static void Create(JsMinifierConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TraceLogger>().As<ILogger>();
            builder.RegisterType<Http.Http>().As<Http.IHttp>();
            builder.RegisterType<FileResolver.AbsolutePathResolver>().As<FileResolver.IFileResolver>();
            if (config.Cache)
            {
                builder.RegisterType<Response.CachedResponse>().As<Response.IResponse>()
                .WithParameter(new NamedParameter("isCompressionHandledByResponse", true));
                builder.RegisterType<Engine.EngineWithCache>().As<Engine.IEngine>();
            }
            else
            {
                builder.RegisterType<Response.NonCachedResponse>().As<Response.IResponse>()
                    .WithParameter(new NamedParameter("isCompressionHandledByResponse", true));
                builder.RegisterType<Engine.Engine>().As<Engine.IEngine>();

            }

            builder.RegisterType<Cache.AspCache>().As<Cache.ICache>();
            builder.RegisterType<Reader.Reader>().As<Reader.IReader>();
            builder.RegisterType<JsMinifier>();
            Instance = builder.Build();

        }
    }
}