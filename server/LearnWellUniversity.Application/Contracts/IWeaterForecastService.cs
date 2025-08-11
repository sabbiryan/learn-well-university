using LearnWellUniversity.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnWellUniversity.Application.Contracts
{
    public interface IWeaterForecastService : IApplicationServiceBase
    {
        /// <summary>
        /// Gets the weather forecast for the next 5 days.
        /// </summary>
        /// <returns>A list of weather forecasts.</returns>
        IEnumerable<WeatherForecast> GetWeatherForecast();
    }
}