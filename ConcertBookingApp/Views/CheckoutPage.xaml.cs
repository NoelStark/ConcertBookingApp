using ConcertBookingApp.ViewModels.CheckoutViewModels;
using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp.Views;

public partial class CheckoutPage : ContentPage
{
    private CheckoutViewModel _checkoutViewModel;
    public CheckoutPage(CheckoutViewModel checkoutviewmodel)
	{
		InitializeComponent();
        _checkoutViewModel = checkoutviewmodel;
        BindingContext = checkoutviewmodel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _checkoutViewModel.Initialize();
    }
}