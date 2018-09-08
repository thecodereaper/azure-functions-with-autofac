using System.Threading.Tasks;
using Autofac;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace Demo.Functions.Framework
{
    internal sealed class InjectAttributeBindingProvider : IBindingProvider
    {
        private readonly IContainer _container;

        public InjectAttributeBindingProvider(IContainer container)
        {
            _container = container;
        }

        public Task<IBinding> TryCreateAsync(BindingProviderContext context)
        {
            return Task.FromResult<IBinding>(new InjectAttributeBinding(context.Parameter, _container));
        }
    }
}