using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ConcertBookingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConcertBookingApp.ViewModels
{
    partial class BookingsPageViewModel : ObservableObject
    {
        [ObservableProperty]
        private Performance performance;

        public BookingsPageViewModel() 
        { 

        }

        //[RelayCommand]
        //public void ShowPopup()
        //{
        //    PopupView? popup = new PopupView(this);
        //    Application.Current?.MainPage?.ShowPopup(popup);
        //}
    }
}
