using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp.Views;

public partial class BookingsPage : ContentPage
{
	public BookingsPage()
	{
		InitializeComponent();
		BindingContext = new BookingsPageViewModel();
	}
}