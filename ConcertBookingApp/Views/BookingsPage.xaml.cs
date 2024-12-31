using ConcertBookingApp.ViewModels.BookingViewModels;
using Microsoft.Maui.Platform;

namespace ConcertBookingApp.Views;

public partial class BookingsPage : ContentPage
{
	public BookingsPage(BookingViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        

    }
}