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
            Log.Verbose("Received request to get weather for {location}", weatherRequest.Location);

            var placeholder = new WeatherResponse
            {
                Location = "Copper Harbor",
                Temperature = 2,
                WeatherDescription = "Sunny",
                Precip =0
            };

            return new ServiceResult<WeatherResponse>().SetValue(placeholder);
        }
    }
}