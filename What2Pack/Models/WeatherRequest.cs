﻿namespace What2Pack.Api.Models
{
    public class WeatherRequest
    {
        public DateTime TripStartDate { get; set; }
        public int TripDuration { get; set; }
        public string Location { get; set; }
    }
}
