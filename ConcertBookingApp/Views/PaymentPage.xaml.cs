using System.ComponentModel;
using ConcertBookingApp.ViewModels;

namespace ConcertBookingApp.Views;

public partial class PaymentPage : ContentPage
{
	public PaymentPage(PaymentViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
        viewModel.PropertyChanged += Field_PropertyChanged;
    }

    private void Field_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        Entry? entry = null;
        if (e.PropertyName == nameof(PaymentViewModel.CreditCardNumber))
            entry = CreditCardNumber;
        if (entry != null)
        {
            Dispatcher.Dispatch(() =>
            {
                entry.CursorPosition = entry.Text.Length;
            });
        }
    }
}