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
        private Serilog.ILogger Log { get; }

        public ForecastController(IForecastService forecastService, Serilog.ILogger log)
        {
            ForecastService = forecastService;
            Log = log.ForContext<ForecastService>();
        }


        //Rough controller method
        [HttpGet]
        [Route("GetWeather/{startdate}/{duration}/{location}")]
        public async Task<IActionResult> GetWeatherForTrip([FromRoute] string startdate, string duration, string location)
        {
            Log.Verbose("Received request to get weather for a trip");

            //Validate Request
            //TODO create actual validation
            Log.Verbose("Validating request");
            if(string.IsNullOrWhiteSpace(startdate) || string.IsNullOrWhiteSpace(duration) || string.IsNullOrWhiteSpace(location))
            {
                Log.Warning("No parameters attached to request");
                return BadRequest();
            }

            //build object, This will be removed after params are moved on the request.
            var weatherRequest = new WeatherRequest
            {
                Location = location,
                TripDuration = int.Parse(duration),
                TripStartDate = startdate
            };

            //Call Service
            var weatherResult = await ForecastService.GetWeather(weatherRequest);

            if (weatherResult.IsError)
            {
                return StatusCode(500);
            }

            return Ok(weatherResult);

            //Return
        }
    }
}
