using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Services;
using SharedResources.DTOs;
using SharedResources.Models;

namespace ConcertBookingApp.ViewModels.ConcertsOverviewViewModels
{
    public partial class ConcertOverviewViewModel : ObservableObject
    {
        private readonly IMapper _mapper;
        private List<Concert> _concertDTOs;
        private readonly ConcertService _concertService;
        private readonly UserService _userService;
        public List<Concert> filteredConcerts;

        public ConcertOverviewViewModel(ConcertService concertService, IMapper mapper, UserService userService)
        {
            _mapper = mapper;
            _concertService = concertService;
            _userService = userService;
            Initialize();


        }

        private async Task Initialize()
        {
            Name = _userService.CurrentUser.Name;
            _concertDTOs = await _concertService.GetAllConcerts();
            Concerts = new ObservableCollection<Concert>(_concertDTOs);
            filteredConcerts = new List<Concert>(_concertDTOs);
            UpdateConcerts(_concertDTOs);
        }

        private void UpdateConcerts(List<Concert> updatedConcerts)
        {
            Concerts.Clear();
            OnPropertyChanged(nameof(Concerts));

            foreach (var concert in updatedConcerts)
            {
                Concerts.Add(concert);
            }

            ConcertCount = Concerts.Count;
            OnPropertyChanged(nameof(Concerts));
        }


        private void FilterConcerts(string? searchText = null, List<Concert>? concerts = null)
        {
            concerts ??= this._concertDTOs;

            if (!string.IsNullOrEmpty(searchText))
            {
                concerts = concerts.Where(c =>
                    c.Genre.ToLower().Contains(searchText.ToLower()) ||
                    c.Name.ToLower().Contains(searchText.ToLower())).ToList();
            }

            if (startDate != null && endDate != null)
            {
                concerts = concerts.Where(c =>
                    c.Performances.Any(p => p.Date > startDate && p.Date < endDate)).ToList();
            }

            if (_selectedCategories.Any())
            {
                var selectedCategories = Categories.Where(x => x.IsSelected).ToList();
                concerts = concerts.Where(c =>
                    selectedCategories.Any(category => category.Title == c.Genre)).ToList();
            }
            filteredConcerts = concerts;
            UpdateConcerts(filteredConcerts);
        }




    }
}
