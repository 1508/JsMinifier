namespace JsMinifier.Handler.Engine.Minify
{
    class MinifyEngine : IMinifyEngine
    {
        public virtual string Run(string source, string path)
        {
            return Yahoo.Yui.Compressor.JavaScriptCompressor.Compress(source);
        }
    }
}