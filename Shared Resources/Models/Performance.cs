using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public partial class Performance : ObservableObject
    {
        public int PerformanceId { get; set; } = 0;
        public int ConcertId { get; set; } = 0;
        public DateTime Date { get; set; } = new DateTime();
        public string Location { get; set; } = string.Empty;
        public int TotalSeats { get; set; } = 0;

        [ObservableProperty]
        private int availableSeats = 0;
        public double Price { get; set; } = 0;
        public Concert Concert { get; set; } = null;

        public BookingPerformance BookingPerformance { get; set; }
    }
}
