namespace JsMinifier.Handler.Engine
{
    class Engine : IEngine
    {
        public virtual string Run(string source, string path)
        {
            return Yahoo.Yui.Compressor.JavaScriptCompressor.Compress(source);
        }
    }
}