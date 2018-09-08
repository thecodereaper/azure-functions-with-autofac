using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Azure.WebJobs.Host.Bindings;
using Microsoft.Azure.WebJobs.Host.Protocols;

namespace Demo.Functions.Framework
{
    internal sealed class InjectAttributeBinding : IBinding
    {
        private readonly ParameterInfo _parameterInfo;
        private readonly IContainer _container;

        public InjectAttributeBinding(ParameterInfo parameterInfo, IContainer container)
        {
            _parameterInfo = parameterInfo;
            _container = container;
        }

        public bool FromAttribute => true;

        public Task<IValueProvider> BindAsync(object value, ValueBindingContext context)
        {
            return BindAsync();
        }

        public Task<IValueProvider> BindAsync(BindingContext context)
        {
            return BindAsync();
        }

        public ParameterDescriptor ToParameterDescriptor()
        {
            return new ParameterDescriptor
            {
                Name = _parameterInfo.Name
            };
        }

        private Task<IValueProvider> BindAsync()
        {
            return Task.FromResult<IValueProvider>(new InjectAttributeValueProvider(_parameterInfo, _container));
        }
    }
}