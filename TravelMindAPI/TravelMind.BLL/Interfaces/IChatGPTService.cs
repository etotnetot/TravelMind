using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMind.BLL.Interfaces
{
    public interface IChatGPTService
    {
        Task<string> GetTripPlanAsync(string prompt);
    }
}