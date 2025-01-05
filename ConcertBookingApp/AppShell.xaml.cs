using ConcertBookingApp.ViewModels;
using ConcertBookingApp.Views;

namespace ConcertBookingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ConcertOverviewPage", typeof(ConcertOverviewPage));
            Routing.RegisterRoute("ConcertDetailsPage", typeof(ConcertDetailsPage));
            Routing.RegisterRoute("CheckoutPage", typeof(CheckoutPage));
            Routing.RegisterRoute("BookingsPage", typeof(BookingsPage));
            Routing.RegisterRoute("PaymentPage", typeof(PaymentPage));

        }
    }
}
