using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMind.Shared.Models
{
    public class RouteRequest
    {
        public string StartCity { get; set; }
        public string EndCity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}