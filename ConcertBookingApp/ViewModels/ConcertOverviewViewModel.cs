using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Models;

namespace ConcertBookingApp.ViewModels
{
    public partial class ConcertOverviewViewModel : ObservableObject
    {
        public List<Category> Categories { get; set; }= new List<Category>()
        {
            new Category { ImageSource = "violin.png", Title = "Classical" },
            new Category { ImageSource = "saxophone.png", Title = "Jazz" },
            new Category { ImageSource = "guitar.png", Title = "Pop" },
            new Category { ImageSource = "headphones.png", Title = "EDM" }
        };

        public List<Concert> Concerts { get; set; } = new List<Concert>()
        {
            new Concert
            {
                ConcertId = 1, Description = "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.",
                Genre = "Pop", ImageUrl = "edm.png", Name = "Pop Pulse Festival"
            },
            new Concert
            {
                ConcertId = 1, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                Genre = "Pop", ImageUrl = "edm.png", Name = "Starlight Pop Jam"
            }
        };

        [RelayCommand]
        private async void InspectConcert(Concert concert)
        {
            string serializedConcert = JsonSerializer.Serialize(concert);
            string encodedConcert = Uri.EscapeDataString(serializedConcert);
            await Shell.Current.GoToAsync($"///ConcertDetailsPage?concert={encodedConcert}");
        }
    }
}
