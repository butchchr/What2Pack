using NSubstitute;
using System;
using What2Pack.Api.Models;
using What2Pack.Api.Services;
using Xunit;

namespace What2Pack.Api.Tests
{
    [Trait("Category", "Unit")]
    public class ValidationServiceTests
    {
        private static WeatherRequest GetWeatherRequest()
        {
            return new WeatherRequest
            {
                RequestId = "123456",
                TripStartDate = DateTime.Now.Date,
                TripDuration = 3,
                Location = "Copper Harbor"
            };
        }

        [Fact]
        public void ValidateGetWeatherRequest_ReturnsWeatherRequest_GivenGoodData()
        {
            var logger = Substitute.For<Serilog.ILogger>();
            var sut = new ValidationService(logger);
            var weatherRequest = GetWeatherRequest();

            var result = sut.ValidateGetWeatherRequest(weatherRequest.RequestId, weatherRequest.TripStartDate.ToString(), weatherRequest.TripDuration.ToString(), weatherRequest.Location);

            Assert.IsType<WeatherRequest>(result.Value);
            Assert.Equal(weatherRequest.RequestId, result.Value.RequestId);
            Assert.Equal(weatherRequest.TripStartDate, result.Value.TripStartDate);
            Assert.Equal(weatherRequest.TripDuration, result.Value.TripDuration);
            Assert.Equal(weatherRequest.Location, result.Value.Location);
        }

        [Theory]
        [InlineData("123456", "string", "string", "Copper Harbor")]
        [InlineData("123456", " ", " ", "Copper Harbor")]
        [InlineData("123456", "1", "potato", "Copper Harbor")]
        [InlineData("123456", "potato", "1", "Copper Harbor")]
        [InlineData("123456", "null", "null", "Copper Harbor")]
        [InlineData("123456", null, null, "Copper Harbor")]
        public void ValidateGetWeatherRequest_DeathMarches_GivenBadDataOtherThanLocation(string a, string b, string c, string d)
        {
            var logger = Substitute.For<Serilog.ILogger>();
            var sut = new ValidationService(logger);
            var weatherRequest = GetWeatherRequest();

            var result = sut.ValidateGetWeatherRequest(a, b, c, d);

            Assert.IsType<WeatherRequest>(result.Value);
            Assert.Equal(weatherRequest.RequestId, result.Value.RequestId);
            Assert.Equal(weatherRequest.TripStartDate, result.Value.TripStartDate);
            Assert.Equal(1, result.Value.TripDuration);
            Assert.Equal(weatherRequest.Location, result.Value.Location);
        }


        [Fact]
        public void ValidateStartDate_ReturnsDate_GivenGoodData()
        {
            var logger = Substitute.For<Serilog.ILogger>();
            var sut = new ValidationService(logger);

            var result = sut.ValidateStartDate(DateTime.Now.Date.ToString());

            Assert.IsType<DateTime>(result);
            Assert.Equal(DateTime.Now.Date, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("           ")]
        [InlineData("Potato")]
        [InlineData("123456")]
        [InlineData("2022-02-31")]
        [InlineData("20221201")]
        public void ValidateStartDate_ReturnsNow_GivenBadData(string value)
        {
            var logger = Substitute.For<Serilog.ILogger>();
            var sut = new ValidationService(logger);

            var result = sut.ValidateStartDate(value);

            Assert.IsType<DateTime>(result);
            Assert.Equal(DateTime.Now.Date, result);
        }

        [Fact]
        public void ValidateTripDuration_Returns3_GivenGoodData()
        {
            var logger = Substitute.For<Serilog.ILogger>();
            var sut = new ValidationService(logger);

            var result = sut.ValidateTripDuration("3");

            Assert.IsType<int>(result);
            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("           ")]
        [InlineData("Potato")]
        [InlineData("3 Days")]
        [InlineData("2022-02-31")]
        [InlineData(")&#YR@")]
        public void ValidateTripDuration_Returns1_GivenBadData(string value)
        {
            var logger = Substitute.For<Serilog.ILogger>();
            var sut = new ValidationService(logger);

            var result = sut.ValidateTripDuration(value);

            Assert.IsType<int>(result);
            Assert.Equal(1, result);
        }

        [Fact]
        public void ValidateLocation_ReturnsLocation_GivenGoodData()
        {
            var logger = Substitute.For<Serilog.ILogger>();
            var sut = new ValidationService(logger);

            var result = sut.ValidateLocation("Copper Harbor");

            Assert.IsType<string>(result);
            Assert.Equal("Copper Harbor", result);
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData("           ")]
        public void ValidateLocation_ReturnsInvalid_GivenBadData(string value)
        {
            var logger = Substitute.For<Serilog.ILogger>();
            var sut = new ValidationService(logger);

            var result = sut.ValidateLocation(value);

            Assert.IsType<string>(result);
            Assert.Equal("invalid", result);
        }
    }
}