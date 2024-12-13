using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace ConcertBookingApp.Models
{
    public partial class Category : ObservableObject
    {
        public string ImageSource { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;

        [ObservableProperty] private bool isSelected = false;
    }
}
