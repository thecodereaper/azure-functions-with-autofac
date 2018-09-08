using Autofac;
using Demo.Core.Services;

namespace Demo.Core
{
    public sealed class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<HeroService>().As<IHeroService>();
        }
    }
}