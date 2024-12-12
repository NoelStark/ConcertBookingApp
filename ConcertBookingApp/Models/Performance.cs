using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Models
{
    public class Performance
    {
        public int PerformanceId { get; set; } = 0;
        public int ConcertId { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
        public string Location { get; set; } = string.Empty;
        public int TotalSeats { get; set; } = 0;
        public int AvailableSeats { get; set; } = 0;
    }
}
