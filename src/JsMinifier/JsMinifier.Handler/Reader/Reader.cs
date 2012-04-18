using System.IO;
using System.Web.Hosting;

namespace JsMinifier.Handler.Reader
{
    class Reader : IReader
    {
        public string ReadContent(string path)
        {
            var virtualPathProvider = HostingEnvironment.VirtualPathProvider;
            var virtualFile = virtualPathProvider.GetFile(path);
            using (var streamReader = new StreamReader(virtualFile.Open()))
            {
                return streamReader.ReadToEnd();
            }
        }
    }
}