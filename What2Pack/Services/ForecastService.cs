using What2Pack.Api.Models;

namespace What2Pack.Api.Services
{
    public interface IForecastService 
    { 
        Task<ServiceResult<WeatherResponse>> GetWeather(WeatherRequest weatherRequest);
    }

    public class ForecastService : IForecastService
    {
        private Serilog.ILogger Log { get; }

        public ForecastService(Serilog.ILogger log)
        {
            Log = log;
        }

        public async Task<ServiceResult<WeatherResponse>> GetWeather(WeatherRequest weatherRequest)
        {
            throw new NotImplementedException();
        }
    }
}