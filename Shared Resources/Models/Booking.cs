using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharedResources.Models
{
    public class Booking
    {
        [Key]
        public int BookingId { get; set; } = 0;
        public List<BookingPerformance> BookingPerformances { get; set; } = new();
        public int UserId { get; set; } = new User().UserId;
        public DateTime BookingDate { get; set; } = new DateTime();

    }
}
