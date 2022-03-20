using Autofac;
using What2Pack.Api.Services;

namespace What2Pack.Api.Modules
{
    public class ServicesModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ForecastService>()
                .As<IForecastService>()
                .SingleInstance();

            builder.RegisterType<LocationService>()
                .As<ILocationService>()
                .SingleInstance();

            builder.RegisterType<ValidationService>()
                .As<IValidationService>()
                .SingleInstance();

            builder.RegisterType<WeatherstackService>()
                .As<IWeatherstackService>()
                .SingleInstance();
        }
    }
}
