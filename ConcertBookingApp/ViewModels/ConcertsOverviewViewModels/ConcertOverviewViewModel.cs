using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Models;

namespace ConcertBookingApp.ViewModels.ConcertsOverviewViewModels
{
    public partial class ConcertOverviewViewModel : ObservableObject
    {

        private void UpdateConcerts(List<Concert> concerts)
        {
            Concerts.Clear();
            foreach (var concert in concerts)
            {
                Concerts.Add(concert);
            }
            OnPropertyChanged(nameof(Concerts));
        }


        private List<Concert> FilterSearch(string value)
        {
            if (!string.IsNullOrEmpty(value)) 
                _cachedConcerts = ApplyFilter(_cachedConcerts, value);

            CategoryFilter();
            return _cachedConcerts;
           
        }

        private List<Concert> ApplyFilter(List<Concert> cachedConcerts, string value)
        {
            value = value.ToLower();
            return cachedConcerts.Where(c => c.Genre.ToLower().Contains(value) || c.Name.ToLower().Contains(value)).ToList();
        }

        private List<Concert> ApplyFilter(List<Concert> cachedConcerts, DateTime? startDate, DateTime? endDate)
        {
            return cachedConcerts.Where(x => x.Performances.Any(p => p.Date > startDate && p.Date < endDate)).ToList();
        }

        private async Task CategoryFilter()
        {
            List<Concert> filteredConcerts = await Task.Run(() =>
            {
                List<Category> selectedCategories = Categories.Where(x => x.IsSelected == true).ToList();
                return AllConcerts.Where(x => selectedCategories.Any(category => category.Title == x.Genre)).ToList();
            });
            _cachedConcerts = filteredConcerts;
            Concerts = new ObservableCollection<Concert>(_cachedConcerts);
        }
       


    }
}
