using ConcertBookingApp.ViewModels.BookingViewModels;
using Microsoft.Maui.Platform;

namespace ConcertBookingApp.Views;

public partial class BookingsPage : ContentPage
{
    private BookingViewModel _bookingViewModel;
	public BookingsPage(BookingViewModel viewModel)
	{
		InitializeComponent();
        _bookingViewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _= _bookingViewModel.AddPerformances();
    }
}