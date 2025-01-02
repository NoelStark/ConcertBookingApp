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
        public Booking? CurrentBooking { get; set; } =  null;
    }
}
