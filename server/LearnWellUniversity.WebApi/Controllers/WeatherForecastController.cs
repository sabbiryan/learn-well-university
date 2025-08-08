using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, IWeaterForecastService weaterForecastService) : ApiControllerBaseV1
    {
      
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {

            var weatherForecasts = weaterForecastService.GetWeatherForecast();

            logger.LogInformation("Resposne with {count} weather forecasts.", weatherForecasts.Count());

            return weatherForecasts;
        }
    }
}
