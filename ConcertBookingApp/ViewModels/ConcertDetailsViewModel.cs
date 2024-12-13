using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.ViewModels
{
    [QueryProperty(nameof(_totalCartCost), "totalCartCost")]
    public partial class ConcertDetailsViewModel : ObservableObject
    {
        private Concert? _totalCartCost;
        private Concert _concert;

        private ObservableCollection<Performance> Concerts = new ObservableCollection<Performance>();

        [ObservableProperty]
        private int amountOfTickets = 0;
        public ConcertDetailsViewModel()
        {
            _concert = new Concert();
        }

        [RelayCommand]
        private async Task BuyTickets()
        {
            //Om ingen biljet är vald - Felhantering att det ej går att trycka på knappen

            //Om det finns

            //await Shell.Current.GoToAsync($"//BookingsPage");
        }

        [RelayCommand]
        void IncreaseQuantity()
        {
            //Vald Concert +1
            AmountOfTickets += 1;
        }

        [RelayCommand]
        void DecreaseQuantity()
        {
            //Vald Concert -1, så länge det inte är 0
            if (AmountOfTickets > 0)
                AmountOfTickets -= 1;
        }
    }
}
