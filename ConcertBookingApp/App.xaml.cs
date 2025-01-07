using ConcertBookingApp.Data.Database;
using ConcertBookingApp.ViewModels.LoginViewModels;
using ConcertBookingApp.Views;

namespace ConcertBookingApp
{
    public partial class App : Application
    {
        private readonly UnitOfWork _unitOfWork;
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
            //MainPage = new NavigationPage(new LoginPage(new LoginViewModel()));
        }
    }
}
