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
        /// <summary>
        /// If the edit button is pressed, the UI redirects to personal information
        /// </summary>

        [RelayCommand]
        void Edit()
        {
            IsVisible = !IsVisible;
            shouldSwitchSection = true;
            ValidateForm();
        }

        [RelayCommand]
        async void GoBack()
        {
            await Shell.Current.GoToAsync("///CheckoutPage");
        }

        /// <summary>
        /// If all fields are valid and Next button is pressed,
        /// either next section shows or the purchase is completed
        /// </summary>
        [RelayCommand(CanExecute = nameof(IsValidForm))]
        async void SavePerson()
        {
            //If Payment information is current section being shown
            if (!shouldSwitchSection)
            {
                //Assuming a user is logged in, the credit card number and type is updated
                if (_userService.CurrentUser != null)
                {
                    _userService.CurrentUser.CreditCardNumber = CreditCardNumber;
                    _userService.CurrentUser.CreditCardType = typeofCard;
                }
                //Saves the booking and gets the assigned booking id to then save the tickets to the database
                int bookingId = await _bookingService.SaveBooking(_bookingService.CurrentBooking);
                await _bookingService.SavePerformances(_bookingService.CurrentBooking.BookingPerformances, bookingId);
                await Shell.Current.GoToAsync($"///ConfirmationPage?totalPrice={TotalCartCost}");

            }
            else
            {
                //If Personal information is shown, it switches to Payment information
                shouldSwitchSection = !shouldSwitchSection;
                IsVisible = !IsVisible;
                ValidateForm();
            }
        }
    }
}
