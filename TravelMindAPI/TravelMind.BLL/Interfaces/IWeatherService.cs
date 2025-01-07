using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMind.Shared.Models;

namespace TravelMind.BLL.Interfaces
{
    public interface IWeatherService
    {
        Task<WeatherInformation> GetWeatherAsync(string city);
    }
}