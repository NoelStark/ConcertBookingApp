using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConcertBookingApp.Data.Models
{
    public partial class BookingPerformance
    {
        [Key]
        public int BookingId { get; set; }
        public int PerformanceId { get; set; }
        

        //[ObservableProperty]
        private int seatsBooked = 0;
        public Booking Booking { get; set; } = null!;
        public Performance Performance { get; set; } = null!;
        //[ObservableProperty]
        public string imageURL = string.Empty;
        //[ObservableProperty]
        public string title = string.Empty;
    }
}
