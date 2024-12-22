using CommunityToolkit.Maui;
using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels;
using ConcertBookingApp.ViewModels.ConcertsOverviewViewModels;
using ConcertBookingApp.ViewModels.PaymentViewModels;
using ConcertBookingApp.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using Microsoft.Maui.Platform; // Provides the ToPlatform() extension method

namespace ConcertBookingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ConcertOverviewPage>();
            builder.Services.AddSingleton<ConcertOverviewViewModel>();
            builder.Services.AddSingleton<ConcertDetailsViewModel>();
            builder.Services.AddSingleton<ConcertDetailsPage>();
            builder.Services.AddTransient<CheckoutPage>();
            builder.Services.AddTransient<CheckoutViewModel>();
            builder.Services.AddSingleton<BookingService>();
            builder.Services.AddSingleton<PaymentViewModel>();
            builder.Services.AddSingleton<PaymentPage>();
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
                h.PlatformView.BackgroundTintList =
                Android.Content.Res.ColorStateList.ValueOf(Microsoft.Maui.Graphics.Colors.Transparent.ToPlatform());
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
