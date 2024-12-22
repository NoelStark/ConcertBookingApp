using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.ViewModels.PaymentViewModels
{
    public partial class PaymentViewModel
    {

        [RelayCommand]
        void Edit()
        {
            IsVisible = !IsVisible;
        }

        [RelayCommand]
        void GoBack()
        {

        }
        [RelayCommand]
        void SavePerson()
        {
            IsVisible = !IsVisible;
        }
    }
}
