using ConcertBookingApp.ViewModels.LoginViewModels;

namespace ConcertBookingApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        BindingContext = loginViewModel;
	}
}