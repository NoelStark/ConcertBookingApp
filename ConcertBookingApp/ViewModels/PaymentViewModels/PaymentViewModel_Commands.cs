using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

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
            {
                if (_userService.CurrentUser != null)
                {
                    _userService.CurrentUser = new User
                    {
                        Email = Email,
                        Name = FirstName + " " + LastName,
                    };
                }
                await Shell.Current.GoToAsync("ConcertOverviewPage");

            }
            shouldSwitchSection = !shouldSwitchSection;
            IsVisible = !IsVisible;
            ValidateForm();
        }
    }
}
