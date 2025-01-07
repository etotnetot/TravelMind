using Microsoft.AspNetCore.Mvc;
using TravelMind.BLL.Interfaces;
using TravelMind.Shared.Models;

namespace TravelMind.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RouteController : ControllerBase
    {
        private readonly IRoutePlanningService _routePlanningService;

        public RouteController(IRoutePlanningService routePlanningService)
        {
            _routePlanningService = routePlanningService;
        }

        [HttpPost("plan-route")]
        public async Task<ActionResult<RouteResponse>> PlanRoute([FromBody] RouteRequest request)
        {
            var response = await _routePlanningService.PlanRouteAsync(request);

            return Ok(response);
        }
    }
}