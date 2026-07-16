using Autofac;
using NoviCode.Application.Interfaces;
using NoviCode.Infrastructure.Persistencies.Command.Players;

namespace NoviCode.Infrastructure.Modules
{
    public class InfrastructureModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CreatePlayerPersistence>().As<ICreatePlayerPersistence>().InstancePerLifetimeScope();

            builder.RegisterDecorator(typeof(CreatePlayersPersistenceCachingDecorator), typeof(ICreatePlayerPersistence));
        }
    }
}
