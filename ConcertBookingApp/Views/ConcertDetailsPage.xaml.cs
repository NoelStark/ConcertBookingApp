using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp.Views;

public partial class ConcertDetailsPage : ContentPage
{
	public ConcertDetailsPage()
	{
		InitializeComponent();
		BindingContext = new ConcertDetailsViewModel();
	}
}