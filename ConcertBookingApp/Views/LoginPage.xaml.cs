using ConcertBookingApp.ViewModels.LoginViewModels;

namespace ConcertBookingApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        BindingContext = loginViewModel;
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        //Shell.SetTabBarIsVisible(Application.Current.MainPage, false);
    }
}