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
            shouldSwitchSection = true;
            ValidateForm();

        }

        [RelayCommand]
        void GoBack()
        {

        }
        [RelayCommand(CanExecute = nameof(IsValidForm))]
        async void SavePerson()
        {
            if (!shouldSwitchSection)
                await Shell.Current.GoToAsync("ConcertOverviewPage");
            shouldSwitchSection = !shouldSwitchSection;
            IsVisible = !IsVisible;
            ValidateForm();
        }
    }
}
