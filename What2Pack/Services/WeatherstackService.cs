using What2Pack.Api.Models;

namespace What2Pack.Api.Services
{
    public interface IWeatherstackService
    {
        ServiceResult<WeatherResponse> GetHistoricalWeather(WeatherRequest weatherRequest);
    }

    public class WeatherstackService : IWeatherstackService
    {
        private Serilog.ILogger Log { get; }

        public WeatherstackService(Serilog.ILogger log)
        {
            Log = log;
        }

        public ServiceResult<WeatherResponse> GetHistoricalWeather(WeatherRequest weatherRequest)
        {
            // Ran out of time to put together more.  Would use rest sharp to call out the API. 
            // Get JSON back, access the fields I need and return the object.
            // There is some business logic to do to take a start date and duration. 
            // Another endpoint option would be to take a start and end date


            // Query from postman soooooo not encoded.  I am shocked it worked. 
            // {{BaseUrl}}/historical?access_key={{AccessKey}}&query=Copper Harbor&historical_date_start=2021-06-05&historical_date_end=2021-06-11&units=f

            return new ServiceResult<WeatherResponse>();
        }
    }
}
