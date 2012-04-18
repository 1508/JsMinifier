using System;

namespace JsMinifier.Handler.Logger
{
    public class TraceLogger : ILogger
    {
        public void Log(string message)
        {
            System.Diagnostics.Trace.WriteLine(message);
        }
    }
}