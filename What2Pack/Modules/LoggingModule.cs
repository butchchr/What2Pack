using Autofac;
using Serilog;

namespace What2Pack.Api.Modules
{
    public class LoggingModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(ConfigureLogger)
                .AsSelf();

            builder.Register(CreateLogger)
                .As<Serilog.ILogger>();
        }

        /// <summary>
        /// Overly complex logger but setup to be able to swap in a more detailed log config later
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private LoggerConfiguration ConfigureLogger(IComponentContext context)
        {
            return new LoggerConfiguration();
        }

        private Serilog.ILogger CreateLogger(IComponentContext context)
        {
            var config = context.Resolve<LoggerConfiguration>();
            return config.CreateLogger();
        }
    }
}
