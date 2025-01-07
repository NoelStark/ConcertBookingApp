using ConcertBookingApp.ViewModels.ConcertsOverviewViewModels;
using System.Diagnostics;

namespace ConcertBookingApp.Views;

public partial class ConcertOverviewPage : ContentPage
{
	public ConcertOverviewPage(ConcertOverviewViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}
    protected override void OnAppearing()
    {
        base.OnAppearing();
        //Shell.SetTabBarIsVisible(Application.Current.MainPage, true);
    }
}