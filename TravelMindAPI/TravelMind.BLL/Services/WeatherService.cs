using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TravelMind.BLL.Interfaces;
using TravelMind.Shared.Models;

namespace TravelMind.BLL.Services
{
    public class WeatherService: IWeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WeatherInformation> GetWeatherAsync(string city)
        {
            var apiKey = "YOUR_API_KEY";
            var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={apiKey}&units=metric";

            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var weatherData = JsonSerializer.Deserialize<JsonElement>(content);

            return new WeatherInformation
            {
                City = city,
                WeatherCondition = weatherData.GetProperty("weather")[0].GetProperty("description").GetString(),
                Temperature = weatherData.GetProperty("main").GetProperty("temp").GetDouble()
            };
        }
    }
}