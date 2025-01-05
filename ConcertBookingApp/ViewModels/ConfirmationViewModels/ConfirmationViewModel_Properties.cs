using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SharedResources.Models;

namespace ConcertBookingApp.ViewModels.ConfirmationViewModels
{
	public partial class ConfirmationViewModel : ObservableObject
	{
		//Properties to display in the confirmation message
		[ObservableProperty]
		private decimal totalCartCost = 0;


		[ObservableProperty]
		private string name = string.Empty;

		[ObservableProperty]
		private string orderNumber = string.Empty;

		[ObservableProperty]
		private string creditCard = string.Empty;

		[ObservableProperty]
		private string orderDate = DateTime.Now.ToString("yyyy-MM-dd");

		public ObservableCollection<BookingPerformance> Performances { get; set; } = new();
	}
}
