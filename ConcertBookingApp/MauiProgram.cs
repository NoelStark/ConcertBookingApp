using CommunityToolkit.Maui;
using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels;
using ConcertBookingApp.ViewModels.ConcertsOverviewViewModels;
using ConcertBookingApp.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;

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
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
				//h.PlatformView.BackgroundTintList =
				//Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
