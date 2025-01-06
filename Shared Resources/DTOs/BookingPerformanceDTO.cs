using CommunityToolkit.Mvvm.ComponentModel;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared_Resources.DTOs
{
    public class BookingPerformanceDTO
    {
        public int BookingId { get; set; }
        public int PerformanceId { get; set; }
        public int SeatsBooked { get; set; }
       
    }
}
