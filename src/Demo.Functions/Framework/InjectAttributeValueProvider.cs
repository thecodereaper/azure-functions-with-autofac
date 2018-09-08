using System;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Azure.WebJobs.Host.Bindings;

namespace Demo.Functions.Framework
{
    internal sealed class InjectAttributeValueProvider : IValueProvider
    {
        private readonly ParameterInfo _parameterInfo;
        private readonly IContainer _container;

        public InjectAttributeValueProvider(ParameterInfo parameterInfo, IContainer container)
        {
            _parameterInfo = parameterInfo;
            _container = container;
        }

        public Type Type => _parameterInfo.ParameterType;

        public Task<object> GetValueAsync()
        {
            return Task.FromResult(_container.Resolve(Type));
        }

        public string ToInvokeString()
        {
            return Type.ToString();
        }
    }
}