using What2Pack.Api.Models;

namespace What2Pack.Api.Services
{
    public interface IValidationService
    {
        ServiceResult<WeatherRequest> ValidateGetWeatherRequest(string weatherId, string startDate, string duration, string location);
    }

    public class ValidationService : IValidationService
    {
        private Serilog.ILogger Log { get; }

        public ValidationService(Serilog.ILogger log)
        {
            Log = log;
        }

        // As basic as a validator can get massive assumptions on the data,
        // TODO make actual validation rules based around a "Trip" definition and API limitations
        // Death march though the validation and only fail on missing location
        public ServiceResult<WeatherRequest> ValidateGetWeatherRequest(string weatherId, string startDate, string duration, string location)
        {
            var logContext = new { weatherId, startDate, duration, location };
            Log.Information("Received request to validate weather request for {logContext}", logContext);

            Log.Verbose("Validating start date for {logContext}", logContext);
            var validatedStartDate = ValidateStartDate(startDate);

            Log.Verbose("Start date validation successful, validating trip duration {logContext}", logContext);
            var validatedDuration = ValidateTripDuration(duration);

            Log.Verbose("Trip duration validation successful, validating location {logContext}", logContext);
            var validatedLocation = ValidateLocation(location);
            if (validatedLocation == "invalid")
            {
                Log.Error("Weather location is invalid, {logContext}", logContext);
                return new ServiceResult<WeatherRequest>()
                    .AddError()
                    .AddMessage("Request location is missing");
            }

            Log.Verbose("Weather request is valid, building weather request {logContext}", logContext);
            var request = new WeatherRequest
            {
                RequestId = weatherId,
                TripStartDate = validatedStartDate,
                TripDuration = validatedDuration,
                Location = validatedLocation

            };

            Log.Information("Returning weather request object");
            return new ServiceResult<WeatherRequest>().SetValue(request);
        }

        public DateTime ValidateStartDate(string startDate)
        {
            if(DateTime.TryParse(startDate, out var validStartDate))
            {
                return validStartDate;
            }

            Log.Information("Invalid start date, assigning datetime.now");
            return DateTime.Now.Date;
        }

        public int ValidateTripDuration(string duration)
        {
            if(int.TryParse(duration, out var tripDuration))
            {
                return tripDuration;
            }

            Log.Information("Invalid duration, assigning 1");
            return 1;
        }

        public string ValidateLocation(string location)
        {
            if (string.IsNullOrWhiteSpace(location))
            {
                Log.Information("Invalid location");
                return "invalid";
            }

            return location;
        }
    }
}
