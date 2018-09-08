using System.Collections.Generic;
using Autofac;
using Demo.Core.Repositories;
using Demo.Infrastructure.Framework;
using Demo.Infrastructure.Repositories;

namespace Demo.Infrastructure
{
    public sealed class InfrastructureModule : Module
    {
        private readonly IDictionary<string, string> _collections = new Dictionary<string, string>
        {
            { "Demo.Core.Models.Heroes.Hero", "Heroes" }
        };

        private const string _database = "HeroesDB";

        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<Connection>()
                .WithParameter("database", _database)
                .WithParameter("collections", _collections)
                .As<IConnection>();

            builder.RegisterType<Repository>().As<IRepository>();
            builder.RegisterType<HeroRepository>().As<IHeroRepository>();
        }
    }
}