using Newtonsoft.Json;
using System.Text;
using TravelMind.BLL.Interfaces;
using TravelMind.Shared.Models;

namespace TravelMind.BLL.Services
{
    public class ChatGPTService : IChatGPTService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey = ""; 

        public ChatGPTService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TripPlanResponse> GetTripPlanAsync(string prompt)
        {
            string apiKey = "";

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");

            var request = new
            {
                prompt = prompt,
                temperature = 0.5,
                max_tokens = 500
            };

            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("https://api.openai.com/v1/engines/davinci-codex/completions", content);
            var responseBody = await response.Content.ReadAsStringAsync();

            Console.WriteLine(responseBody);

            return ParseRouteResponse(responseBody);
        }

        public TripPlanResponse ParseRouteResponse(string jsonResponse)
        {
            try
            {
                var data = JsonConvert.DeserializeObject<TripPlanResponse>(jsonResponse);
                return data;
            }
            catch (Newtonsoft.Json.JsonException ex)
            {
                Console.WriteLine($"Ошибка при парсинге ответа: {ex.Message}");
                return null;
            }
        }

        /*public async Task<RouteResponse> GetTripPlanAsync(string prompt)
        {
            var request = new
            {
                prompt = prompt,
                temperature = 0.5,
                max_tokens = 1000
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://api.openai.com/v1/completions", content);
            response.EnsureSuccessStatusCode();

            var responseBody = await response.Content.ReadAsStringAsync();

            // Парсинг JSON-ответа в объект TripResponse
            var tripResponse = JsonSerializer.Deserialize<RouteResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return tripResponse;
        }*/
    }
}