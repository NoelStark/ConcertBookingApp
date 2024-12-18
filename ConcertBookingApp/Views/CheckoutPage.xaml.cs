using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp.Views;

public partial class CheckoutPage : ContentPage
{
	//private readonly BookingsPageViewModel viewmodel;
	public CheckoutPage(CheckoutViewModel checkoutviewmodel)
	{
		InitializeComponent();
        BindingContext = checkoutviewmodel;
	}
}