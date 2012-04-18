namespace JsMinifier.Handler.Cache
{
    public interface ICache
    {
        void Insert(string identifier, string content);
        string Get(string identifier);
        bool Exists(string identifier);

    }
}