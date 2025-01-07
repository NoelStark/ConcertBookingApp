using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConcertBookingApp.ViewModels.ConfirmationViewModels
{
	public partial class ConfirmationViewModel : ObservableObject
	{
		/// <summary>
		/// Method that handles what happens when the 'Confirm' button is pressed with the purpose
		/// of sending user back to the Overview page
		/// </summary>
		/// <returns></returns>
		[RelayCommand]
		async Task Confirm()
		{
			_bookingService.CurrentBooking = null;
            await Shell.Current.GoToAsync("///ConcertOverviewPage");

        }

    }
}
