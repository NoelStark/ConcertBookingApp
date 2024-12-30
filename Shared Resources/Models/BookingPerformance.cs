using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace SharedResources.Models
{
    public partial class BookingPerformance : ObservableObject
    {
        public int BookingId { get; set; }
        public int PerformanceId { get; set; }
        

        [ObservableProperty]
        private int seatsBooked = 0;
        public Booking Booking { get; set; } = null!;
        public Performance Performance { get; set; } = null!;
        [ObservableProperty]
        public string imageURL = string.Empty;
        [ObservableProperty]
        public string title = string.Empty;
    }
}
