using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp.Views;

public partial class ConcertOverviewPage : ContentPage
{
	public ConcertOverviewPage(ConcertOverviewViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}