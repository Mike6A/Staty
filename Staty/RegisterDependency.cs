using Autofac;
using Staty.Data;
using Staty.Handlers;
using Staty.Services;
using Staty.Utils;

namespace Staty
{
    public class RegisterDependency
    {
        public static IContainer Register(ContainerBuilder builder)
        {
            builder.RegisterType<DataPathManager>().As<IDataPathManager>().SingleInstance();

            builder.RegisterType<ReadStateService>().As<IReadStateService>().SingleInstance();
            builder.RegisterType<StateService>().As<IStateService>().SingleInstance();


            builder.RegisterType<DataHandler>().As<IDataHandler>().SingleInstance();
            builder.RegisterType<ProgramHandler>().As<IProgramHandler>().SingleInstance();
            builder.RegisterType<Pager>().As<IPager>().SingleInstance();
            builder.RegisterType<ConsoleHandler>().As<IConsoleHandler>().SingleInstance();

            builder.RegisterType<App>().SingleInstance();

            return builder.Build();
        }
        
    }
}