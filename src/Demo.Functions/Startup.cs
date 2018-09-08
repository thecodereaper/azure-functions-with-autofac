using Autofac;
using Demo.Core;
using Demo.Functions.Framework;
using Demo.Infrastructure;
using Microsoft.Azure.WebJobs.Host.Config;

namespace Demo.Functions
{
    public sealed class Startup : IExtensionConfigProvider
    {
        public void Initialize(ExtensionConfigContext context)
        {
            ContainerBuilder builder = new ContainerBuilder();
            ConfigureServices(builder);

            IContainer container = builder.Build();
            context.AddBindingRule<InjectAttribute>().Bind(new InjectAttributeBindingProvider(container));
        }

        private static void ConfigureServices(ContainerBuilder builder)
        {
            builder.RegisterType<CoreConfiguration>().As<ICoreConfiguration>();

            builder.RegisterModule<CoreModule>();
            builder.RegisterModule<InfrastructureModule>();
        }
    }
}