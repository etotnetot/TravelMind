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

        public async Task<TripPlanResponse> PlanRouteAsync(RouteRequest request)
        {
            var prompt = $"Привет! Я планирую путешествие из {request.StartCity} в {request.EndCity}. Поездка будет длиться с {request.StartDate:dd.MM.yyyy} по {request.EndDate:dd.MM.yyyy}. Хочу увидеть все достопримечательности по пути. Составь пожалуйста план моей поездки исходя из погоды на данную дату и предложи, каким видом транспорта будет дешевле и быстрее добраться до данной точки.";

            var chatGptResponse = await _chatGptService.GetTripPlanAsync(prompt);

            var suggestedStops = new List<string>();
            var attractions = new List<string>();
            var transportPlan = string.Empty;


            return new TripPlanResponse
            {
                SuggestedStops = suggestedStops,
                Attractions = attractions,
                TransportPlan = { }
            };
        }
    }
}