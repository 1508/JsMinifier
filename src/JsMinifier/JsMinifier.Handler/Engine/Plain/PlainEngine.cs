namespace JsMinifier.Handler.Engine.Plain
{
    class PlainEngine : IPlainEngine
    {
        public virtual string Run(string source, string path)
        {
            return source;
        }
    }
}