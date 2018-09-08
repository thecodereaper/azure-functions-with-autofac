namespace Demo.Core
{
    public interface ICoreConfiguration
    {
        string DocumentDbEndpoint { get; }
        string DocumentDbKey { get; }
    }
}