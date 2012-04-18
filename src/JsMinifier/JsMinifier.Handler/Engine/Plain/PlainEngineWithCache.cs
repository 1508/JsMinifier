using JsMinifier.Handler.Cache;

namespace JsMinifier.Handler.Engine.Plain
{
    class PlainEngineWithCache : PlainEngine
    {
        private readonly ICache _cache;

        public PlainEngineWithCache(ICache cache)
        {
            _cache = cache;
        }

        public override string Run(string source, string path)
        {
            if (this._cache.Exists(path))
                return this._cache.Get(path);

            string minifiedSource = base.Run(source, path);

            this._cache.Insert(path, minifiedSource);
            return this._cache.Get(path);
        }
    }
}