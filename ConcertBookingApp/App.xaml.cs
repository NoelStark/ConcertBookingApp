using ConcertBookingApp.Data.Database;
using ConcertBookingApp.ViewModels.LoginViewModels;
using ConcertBookingApp.Views;

namespace ConcertBookingApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }
    }
}
