using CommunityToolkit.Mvvm.ComponentModel;
using SharedResources.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Services;

namespace ConcertBookingApp.ViewModels.BookingViewModels
{
    public partial class BookingViewModel : ObservableObject
    {
        [ObservableProperty] private double opacity = 0.6;
        [ObservableProperty] private string searchInput = string.Empty;
        [ObservableProperty] private string subHeader = string.Empty;
        private readonly BookingService _bookingService;
        private readonly ConcertService _concertService;
        private readonly UserService _userService;
        private List<BookingPerformance> _performances = new List<BookingPerformance>();
        public ObservableCollection<BookingPerformance> Performances { get; private set; } = new ObservableCollection<BookingPerformance>();

    }
}
