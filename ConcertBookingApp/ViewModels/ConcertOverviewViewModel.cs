using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        private static readonly List<Concert> _concerts = new List<Concert>() 
        {
            new Concert
            {
                ConcertId = 1, Description = "A high-energy event celebrating chart-topping hits and electrifying performances by popular pop artists.",
                Genre = "Pop", ImageUrl = "edm.png", Name = "Pop Pulse Festival"
            },
            new Concert
            {
                ConcertId = 2, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                Genre = "Jazz", ImageUrl = "edm.png", Name = "Starlight Pop Jazz"
            },
            new Concert
            {
                ConcertId = 3, Description = "A vibrant concert featuring a mix of iconic pop hits and fresh, emerging talent under dazzling lights.",
                Genre = "Classical", ImageUrl = "edm.png", Name = "Classical"
            }
        };


        public ObservableCollection<Concert> Concerts { get; set; } = new ObservableCollection<Concert>(_concerts);
      

        [RelayCommand]
        private async void InspectConcert(Concert concert)
        {
            string serializedConcert = JsonSerializer.Serialize(concert);
            string encodedConcert = Uri.EscapeDataString(serializedConcert);
            await Shell.Current.GoToAsync($"///ConcertDetailsPage?concert={encodedConcert}");
        }

        [RelayCommand]
        private async void SelectedFilter(Category value)
        {
            Category? category = Categories.FirstOrDefault(x => x.Title == value.Title);
            if (category == null) return;
            
            category.IsSelected = !category.IsSelected;
            OnPropertyChanged(nameof(category.IsSelected));
            bool isAnySelected = Categories.Any(x => x.IsSelected == true);
            if (isAnySelected)
            {
                await FilterConcerts();
            }
            else
            {
                Concerts = new ObservableCollection<Concert>(_concerts);
            }
            OnPropertyChanged(nameof(Concerts));
            
        }

        private async Task FilterConcerts()
        {
            List<Concert> filteredConcerts = await Task.Run(() =>
            {
                List<Category> selectedCategories = Categories.Where(x => x.IsSelected == true).ToList();
                return _concerts.Where(x => selectedCategories.Any(category => category.Title == x.Genre)).ToList();
            });
            Concerts = new ObservableCollection<Concert>(filteredConcerts);
        }
        [RelayCommand]
        private async void MakeFavorite(Concert value)
        {
            Concert? concert = Concerts.FirstOrDefault(x => x.Name== value.Name);
            if (concert == null) return;
            concert.IsFavorite = !concert.IsFavorite;
            OnPropertyChanged(nameof(concert.IsFavorite));
        }


    }
}
