namespace What2Pack.Api.Models
{
    /// <summary>
    /// Basic weather current weather conditions
    /// </summary>
    public class WeatherResponse
    {
        public string Location { get; set; }
        public int Temperature { get; set; }
        public string WeatherDescriptions { get; set; } //TODO: convert to list
        public int Precip { get; set; }
    }
}
