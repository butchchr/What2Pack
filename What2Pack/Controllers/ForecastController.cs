using Microsoft.AspNetCore.Mvc;
using Serilog;
using What2Pack.Api.Models;
using What2Pack.Api.Services;

namespace What2Pack.Api.Controllers
{
    [Route("~/api/v1")]

    public class ForecastController : Controller
    {
        private IForecastService ForecastService { get; }
        private IValidationService ValidationService { get; }
        private Serilog.ILogger Log { get; }

        public ForecastController(IForecastService forecastService, IValidationService validationService, Serilog.ILogger log)
        {
            ForecastService = forecastService;
            ValidationService = validationService;
            Log = log.ForContext<ForecastService>();
        }


        //Rough controller method
        [HttpGet]
        [Route("GetWeather/{startDate}/{duration}/{location}")]
        public async Task<IActionResult> GetWeatherForTrip([FromRoute] string startDate, string duration, string location)
        {
            var weatherId = new Guid().ToString();
            var logContext = new { weatherId, startDate, duration, location };

            Log.Information("Received request to get weather for trip {logCOntext}", logContext);

            //Validate Request
            //TODO create actual validation
            Log.Verbose("Validating weather request");
            var weatherRequest = ValidationService.ValidateGetWeatherRequest(weatherId, startDate, duration, location);

            if(weatherRequest.IsError)
            {
                Log.Error("One or more parameters of the request are invalid");
                return BadRequest();
            }

            //getting a possible null ref warning here TODO test this as with the validator in place it should not get here/be null
            var weatherResult = await ForecastService.GetWeather(weatherRequest.Value);

            if (weatherResult.IsError)
            {
                return StatusCode(500);
            }

            return Ok(weatherResult);

            //Return
        }
    }
}
