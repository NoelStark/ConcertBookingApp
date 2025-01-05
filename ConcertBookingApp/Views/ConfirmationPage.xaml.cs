using ConcertBookingApp.ViewModels.ConfirmationViewModels;
namespace ConcertBookingApp.Views;

public partial class ConfirmationPage : ContentPage
{
	public ConfirmationPage(ConfirmationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		if (BindingContext is ConfirmationViewModel viewModel)
		{
			viewModel.Reinitialize();
		}
	}
}