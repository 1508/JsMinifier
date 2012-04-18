using Autofac;
using JsMinifier.Handler.Configuration;
using JsMinifier.Handler.Engine;
using JsMinifier.Handler.Engine.Minify;
using JsMinifier.Handler.Engine.Plain;
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
            builder.RegisterType<Engine.IncludeExcludeEngineFactory>().As<Engine.IEngineFactory>();

            builder.RegisterType<FileResolver.AbsolutePathResolver>().As<FileResolver.IFileResolver>();
            if (config.Cache)
            {
                builder.RegisterType<Response.CachedResponse>().As<Response.IResponse>()
                .WithParameter(new NamedParameter("isCompressionHandledByResponse", true));
                builder.RegisterType<MinifyEngineWithCache>().As<IMinifyEngine>();
                builder.RegisterType<PlainEngineWithCache>().As<IPlainEngine>();
            }
            else
            {
                builder.RegisterType<Response.NonCachedResponse>().As<Response.IResponse>()
                    .WithParameter(new NamedParameter("isCompressionHandledByResponse", true));
                builder.RegisterType<MinifyEngine>().As<IMinifyEngine>();
                builder.RegisterType<PlainEngine>().As<IPlainEngine>();
            }

            builder.RegisterType<Cache.AspCache>().As<Cache.ICache>();
            builder.RegisterType<Reader.Reader>().As<Reader.IReader>();
            builder.RegisterInstance(config).As<Configuration.JsMinifierConfiguration>();
            builder.RegisterType<JsMinifier>();
            Instance = builder.Build();

        }
    }
}