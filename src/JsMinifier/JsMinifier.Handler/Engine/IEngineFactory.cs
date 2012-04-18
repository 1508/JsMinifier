using System;

namespace JsMinifier.Handler.Engine
{
    public interface IEngineFactory
    {
        IEngine GetEngine();
    }
}