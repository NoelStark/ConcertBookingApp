using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace ConcertBookingApp.Services
{
    public class BookingService
    {
        public List<Booking> Bookings { get; set; } = new List<Booking>();
    }
}
