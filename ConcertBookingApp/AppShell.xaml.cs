using ConcertBookingApp.ViewModels;
using ConcertBookingApp.ViewModels.LoginViewModels;
using ConcertBookingApp.Views;

namespace ConcertBookingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
            Routing.RegisterRoute("ConcertOverviewPage", typeof(ConcertOverviewPage));
            Routing.RegisterRoute("ConcertDetailsPage", typeof(ConcertDetailsPage));
            Routing.RegisterRoute("CheckoutPage", typeof(CheckoutPage));
            Routing.RegisterRoute("BookingsPage", typeof(BookingsPage));
            Routing.RegisterRoute("PaymentPage", typeof(PaymentPage));
            Routing.RegisterRoute("ConfirmationPage", typeof(ConfirmationPage));

        }

        public void RemoveTab()
        {
            var tabbar = Items.FirstOrDefault(x => x.Route == "D_FAULT_TabBar10");
            var loginpage = tabbar.Items[0];
            tabbar.Items.Remove(loginpage);
            Console.WriteLine();
            //Items.Remove();
        }
    }
}
