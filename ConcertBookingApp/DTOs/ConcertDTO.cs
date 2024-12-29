using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.DTOs
{
    public class ConcertDTO
    {
        public int ConcertId { get; set; } = 0;
        public string Name { get; set; } = string.Empty;
        public string Genre { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public DateTime Date { get; set; } = new DateTime();
        //public List<DateTime> Date { get; set; } = new List<DateTime>();
        public string Location { get; set; } = string.Empty;
    }
}
