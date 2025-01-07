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
                    _userService.CurrentUser.CreditCardNumber = CreditCardNumber;
                    _userService.CurrentUser.CreditCardType = typeofCard;
                }
                
                int bookingId = await _bookingService.SaveBooking(_bookingService.CurrentBooking);
                await _bookingService.SavePerformances(_bookingService.CurrentBooking.BookingPerformances, bookingId);
                await Shell.Current.GoToAsync($"///ConfirmationPage?totalPrice={TotalCartCost}");

            }
            shouldSwitchSection = !shouldSwitchSection;
            IsVisible = !IsVisible;
            ValidateForm();
        }
    }
}
