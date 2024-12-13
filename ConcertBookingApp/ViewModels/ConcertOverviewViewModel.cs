using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
                ConcertId = 1, Description = "Lorem ipsum dolor sit amet consectetur.",
                Genre = "Pop", ImageUrl = "edm.png", Name = "Lorem Ipsum"
            },
            new Concert
            {
                ConcertId = 1, Description = "Lorem ipsum dolor sit amet consectetur.",
                Genre = "Pop", ImageUrl = "edm.png", Name = "Lorem Ipsum"
            }
        };

        public List<Performance> Performances = new List<Performance>()
        {

        };
        [RelayCommand]
        private async void InspectConcert(Concert concert)
        {
            await Shell.Current.GoToAsync($"///ConcertDetailsPage?concert={concert}");
        }
    }
}
