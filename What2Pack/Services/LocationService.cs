using Serilog;

namespace What2Pack.Api.Services
{
    public interface ILocationService
    {

    }

    public class LocationService : ILocationService
    {
        private Serilog.ILogger Log { get; }

        public LocationService(Serilog.ILogger log)
        {
            Log = log;
        }
    }
}
