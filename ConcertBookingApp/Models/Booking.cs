using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.Models
{
    public class Booking
    {
        public int BookingId { get; set; } = 0;
        public List<Performance> Performances { get; set; } = new();
        public int UserId { get; set; } = new User().UserId;
        public DateTime BookingDate { get; set; } = new DateTime();
        public ICollection<BookingPerformance> BookingPerformances { get; set; }= new List<BookingPerformance>();
    
    }
}
