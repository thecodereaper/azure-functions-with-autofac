using System.Configuration;
using Demo.Core;

namespace Demo.Functions
{
    internal sealed class CoreConfiguration : ICoreConfiguration
    {
        public string DocumentDbEndpoint => ConfigurationManager.AppSettings["DocumentDbEndpoint"];
        public string DocumentDbKey => ConfigurationManager.AppSettings["DocumentDbKey"];
    }
}