using System.Configuration;

namespace JsMinifier.Handler.Configuration
{
    public class JsMinifierConfiguration
    {

        public static JsMinifierConfiguration GetConfiguration()
        {
            var config = (JsMinifierConfiguration)ConfigurationManager.GetSection("JsMinifier");

            if (config == null)
                return GetDefault();

            return config;
        }

        public static JsMinifierConfiguration GetDefault()
        {
            return new JsMinifierConfiguration() {Cache = true};
        }
        /// <summary>
        /// Use cache
        /// </summary>
        public bool Cache { get; set; }
    }
}