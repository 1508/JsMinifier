using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace JsMinifier.Handler.Configuration
{
    public class JsMinifierConfiguration : IConfigurationSectionHandler
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
            var jsMinifierConfiguration = new JsMinifierConfiguration();
            jsMinifierConfiguration.Cache = true;
            return jsMinifierConfiguration;
        }

        /// <summary>
        /// Use cache
        /// </summary>
        public bool Cache { get; set; }

        private List<string> _excludes;

        /// <summary>
        /// Excludes path - only checks if the path contains the value
        /// Default: .min.
        /// </summary>
        public List<string> Excludes
        {
            get
            {
                if (this._excludes == null)
                {
                    this._excludes = new List<string>();
                }
                return _excludes;
            }
        }

        public object Create(object parent, object configContext, XmlNode section)
        {
            this.Cache = section.Attributes["Cache"] != null ? bool.Parse(section.Attributes["Cache"].Value) : false;
            XmlNode nodeExcludes = section.SelectSingleNode("Excludes");

            if (nodeExcludes != null)
            {
                XmlNodeList xmlNodeList = nodeExcludes.SelectNodes("Path");
                foreach (XmlNode exclude in xmlNodeList)
                {
                    this.Excludes.Add(exclude.InnerText);
                }
            }
            else
            {
                this.Excludes.Add(".min.");
            }
            return this;
        }
    }
}