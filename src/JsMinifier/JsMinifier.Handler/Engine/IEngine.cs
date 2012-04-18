using System;

namespace JsMinifier.Handler.Engine
{
    public interface IEngine
    {
        string Run(string source, string path);
    }
}