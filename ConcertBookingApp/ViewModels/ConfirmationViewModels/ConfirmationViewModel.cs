using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ConcertBookingApp.Services;
using SharedResources.Models;

namespace ConcertBookingApp.ViewModels.ConfirmationViewModels
{
    [QueryProperty(nameof(TotalCartCost), "totalPrice")]
    public partial class ConfirmationViewModel : ObservableObject
	{
		private readonly UserService _userService;
        private readonly BookingService _bookingService;
		public ConfirmationViewModel(UserService userService, BookingService bookingService)
		{
			_userService = userService;
			_bookingService = bookingService;
			GenerateOrderNumber();
			Reinitialize();
		}

		/// <summary>
		/// Method to re-initialize everything when the View appears
		/// </summary>
		public void Reinitialize()
        {
            Performances.Clear();
            foreach (var performance in _bookingService.CurrentBooking.BookingPerformances)
            {
				Performances.Add(performance);
            }
            CreditCard = _userService.CurrentUser.CreditCardType + " **** " + _userService.CurrentUser.CreditCardNumber.Substring(_userService.CurrentUser.CreditCardNumber.Length - 4);
        }

		/// <summary>
		/// Method to generate a random ordernumber being 12 characters long
		/// </summary>
		private void GenerateOrderNumber()
		{
			Random random = new Random();
			string ordernumber = string.Empty;
			string source = "0123456789ABCDEFGHIJKLMNOPQRSTUV";

			for (int i = 1; i <= 12; i++)
			{
				int randomIndex = random.Next(source.Length);
				ordernumber += source[randomIndex];
			}

            OrderNumber = ordernumber;
        }

	}
}
