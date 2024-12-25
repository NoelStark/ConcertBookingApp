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
using ConcertBookingApp.DTOs;
using ConcertBookingApp.Models;
using ConcertBookingApp.Services;

namespace ConcertBookingApp.ViewModels.ConcertsOverviewViewModels
{
    public partial class ConcertOverviewViewModel : ObservableObject
    {
        private readonly IMapper _mapper;
        private List<ConcertDTO> _concertDTOs;
        private readonly ConcertService _concertService;
        public List<Concert> filteredConcerts;

        public ConcertOverviewViewModel(ConcertService concertService, IMapper mapper)
        {
            _mapper = mapper;
            _concertService = concertService;
            _concertDTOs = _concertService.GetAllConcerts();
            concerts = _mapper.Map<List<Concert>>(_concertDTOs).ToList();


            Concerts = new ObservableCollection<ConcertDTO>(_concertDTOs);
            filteredConcerts = new List<Concert>(concerts);
            UpdateConcerts(_concertDTOs);
        }

        private void UpdateConcerts(List<ConcertDTO> concerts)
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

        private void FilterConcerts(string? searchText = null, List<Concert>? concerts = null)
        {
            concerts ??= this.concerts;

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
            var DTOs = _mapper.Map<List<ConcertDTO>>(filteredConcerts);
            UpdateConcerts(DTOs);
        }




    }
}
