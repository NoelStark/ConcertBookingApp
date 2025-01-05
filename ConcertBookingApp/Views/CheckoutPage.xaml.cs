using ConcertBookingApp.ViewModels.CheckoutViewModels;
using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels;
using ConcertBookingApp.ViewModels.CheckoutViewModels;

namespace ConcertBookingApp.Views;

public partial class CheckoutPage : ContentPage
{
	public CheckoutPage(CheckoutViewModel checkoutviewmodel)
	{
		InitializeComponent();
        BindingContext = checkoutviewmodel;
	}
}