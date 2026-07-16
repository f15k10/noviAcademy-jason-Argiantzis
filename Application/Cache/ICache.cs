namespace NoviCode.Application.Cache
{
    public interface ICache
    {
        //Try to read a cached value . If there is not the value return false
        bool TryGet<T>(string key, out T value);

        void Set<T>(string key, T value,TimeSpan ttl);

        void Remove(string key);
    }
}
