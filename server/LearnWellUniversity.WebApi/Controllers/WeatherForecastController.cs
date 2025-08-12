using LearnWellUniversity.Application.Contracts;
using LearnWellUniversity.Application.Models.Common;
using LearnWellUniversity.Domain.Entities;
using LearnWellUniversity.WebApi.Controllers.Bases;
using Microsoft.AspNetCore.Mvc;

namespace LearnWellUniversity.WebApi.Controllers
{
    public class WeatherForecastController(ILogger<WeatherForecastController> logger, IWeaterForecastService weaterForecastService) : ApiControllerV1
    {
      
        [HttpGet]
        public ApiResponse<IEnumerable<WeatherForecast>> Get()
        {

            var weatherForecasts = weaterForecastService.GetWeatherForecast();

            logger.LogInformation("Resposne with {count} weather forecasts.", weatherForecasts.Count());

            return new ApiResponse<IEnumerable<WeatherForecast>>(weatherForecasts);
        }
    }
}
