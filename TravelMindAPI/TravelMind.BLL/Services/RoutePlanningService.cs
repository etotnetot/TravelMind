using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelMind.BLL.Interfaces;
using TravelMind.Shared.Models;

namespace TravelMind.BLL.Services
{
    public class RoutePlanningService : IRoutePlanningService
    {
        private readonly IWeatherService _weatherService;
        private readonly IChatGPTService _chatGptService;

        public RoutePlanningService(IWeatherService weatherService, IChatGPTService chatGptService)
        {
            _weatherService = weatherService;
            _chatGptService = chatGptService;
        }

        public async Task<RouteResponse> PlanRouteAsync(RouteRequest request)
        {
            // Prepare the prompt for ChatGPT
            var prompt = $"Привет! Я планирую путешествие из {request.StartCity} в {request.EndCity}. Поездка будет длиться с {request.StartDate:dd.MM.yyyy} по {request.EndDate:dd.MM.yyyy}. Хочу увидеть все достопримечательности по пути. Составь пожалуйста план моей поездки исходя из погоды на данную дату и предложи, каким видом транспорта будет дешевле и быстрее добраться до данной точки.";

            // Get response from ChatGPT
            var chatGptResponse = await _chatGptService.GetTripPlanAsync(prompt);

            // Parse ChatGPT response 
            var suggestedStops = new List<string>();
            var attractions = new List<string>();
            var transportPlan = string.Empty;

            if (!string.IsNullOrEmpty(chatGptResponse))
            {
                var responseLines = chatGptResponse.Split('\n');
                foreach (var line in responseLines)
                {
                    if (line.StartsWith("Stops:", StringComparison.OrdinalIgnoreCase))
                    {
                        suggestedStops.AddRange(line.Replace("Stops:", "").Split(','));
                    }
                    else if (line.StartsWith("Attractions:", StringComparison.OrdinalIgnoreCase))
                    {
                        attractions.AddRange(line.Replace("Attractions:", "").Split(','));
                    }
                    else if (line.StartsWith("Transport:", StringComparison.OrdinalIgnoreCase))
                    {
                        transportPlan = line.Replace("Transport:", "").Trim();
                    }
                }
            }

            return new RouteResponse
            {
                SuggestedStops = suggestedStops,
                Attractions = attractions,
                TransportPlan = transportPlan
            };
        }
    }
}