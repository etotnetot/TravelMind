using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelMind.Shared.Models
{
    public class TransportPlan
    {
        public string TransportType { get; set; } // Тип транспорта, например, "Поезд"

        public decimal Price { get; set; } // Цена билета

        public string DepartureCity { get; set; } // Город отправления

        public DateTime DepartureTime { get; set; } // Время отправления

        public string ArrivalCity { get; set; } // Город прибытия

        public DateTime ArrivalTime { get; set; } // Время прибытия

        public string Duration { get; set; } // Продолжительность поездки
    }
}