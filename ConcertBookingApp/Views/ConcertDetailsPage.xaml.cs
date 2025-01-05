using ConcertBookingApp.ViewModels;
using ConcertBookingApp.ViewModels.ConcertDetailsViewModels;

namespace ConcertBookingApp.Views;

public partial class ConcertDetailsPage : ContentPage
{

	public ConcertDetailsPage(ConcertDetailsViewModel viewmodel)
	{
		InitializeComponent();
        BindingContext = viewmodel;
	}
}