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
        public ConcertOverviewViewModel()
        {
            UpdateConcerts(AllConcerts);
        }

        private void UpdateConcerts(List<Concert> concerts)
        {
            Concerts.Clear();
            foreach (var concert in concerts)
            {
                Concerts.Add(concert);
            }

            ConcertCount = Concerts.Count;
            OnPropertyChanged(nameof(Concerts));
            OnPropertyChanged(nameof(ConcertCount));
        }

        private List<Concert> filteredConcerts = new List<Concert>(AllConcerts);
        private void FilterConcerts(string? searchText = null)
        {
            if (!string.IsNullOrEmpty(searchText))
            {
                if (startDate != null && endDate != null)
                {
                    filteredConcerts = AllConcerts.Where(x => x.Performances.Any(p => p.Date > startDate && p.Date < endDate) && x.Genre.ToLower().Contains(searchText)).ToList();
                }
                else
                {
                    filteredConcerts = filteredConcerts.Where(c => c.Genre.ToLower().Contains(searchText) || c.Name.ToLower().Contains(searchText)).ToList();

                }
                Categories.ForEach(c => c.IsSelected = false);
                OnPropertyChanged(nameof(Categories));
                _lastSearchInput = searchText;
            }
            else
            {
                if (!string.IsNullOrEmpty(_lastSearchInput))
                {
                    if (startDate != null && endDate != null)
                    {
                        filteredConcerts = filteredConcerts.Where(x => x.Performances.Any(p => p.Date > startDate && p.Date < endDate)).ToList();
                    }
                    else
                        filteredConcerts = AllConcerts;
                }
                if (_selectedCategories.Any())
                {
                    List<Category> categories = Categories.Where(x => x.IsSelected == true).ToList();
                    filteredConcerts = filteredConcerts.Where(x => categories.Any(category => category.Title == x.Genre)).ToList();
                    
                }
                else
                {
                    filteredConcerts = AllConcerts;
                }
            }

            if (startDate != null && endDate != null)
            {
                filteredConcerts = filteredConcerts.Where(x => x.Performances.Any(p => p.Date > startDate && p.Date < endDate)).ToList();
            }


            _cachedConcerts = filteredConcerts;
            UpdateConcerts(filteredConcerts);
        }
       


    }
}
