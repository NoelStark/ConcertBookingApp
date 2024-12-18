using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp.Views;

public partial class PaymentPage : ContentPage
{
	public PaymentPage(PaymentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
}