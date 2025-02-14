using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMind.Shared.Models
{
    public class TripPlanResponse
    {
        public List<string> SuggestedStops { get; set; }

        public List<string> Attractions { get; set; }

        public List<TransportPlan> TransportPlan { get; set; }
    }
}