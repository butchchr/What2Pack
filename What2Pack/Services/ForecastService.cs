using What2Pack.Api.Models;

namespace What2Pack.Api.Services
{
    public interface IForecastService 
    { 
        Task<ServiceResult<WeatherResponse>> GetWeather(WeatherRequest weatherRequest);
    }

    public class ForecastService : IForecastService
    {
        private IWeatherstackService WeatherstackService { get; }
        private Serilog.ILogger Log { get; }
        

        public ForecastService(IWeatherstackService weatherstackService, Serilog.ILogger log)
        {   
            WeatherstackService= weatherstackService;   
            Log = log;
        }

        // This class exists with the assumption that we will be doing something with the forecast object after it comes back from weatherstack
        // Currently just passing the basic weather back 
        public async Task<ServiceResult<WeatherResponse>> GetWeather(WeatherRequest weatherRequest)
        {
            Log.Verbose("Received request to get weather for {location}", weatherRequest.Location);

            // Call weatherstackService


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