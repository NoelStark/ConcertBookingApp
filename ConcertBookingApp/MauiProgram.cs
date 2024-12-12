using ConcertBookingApp.ViewModels;
using ConcertBookingApp.Views;
using Microsoft.Extensions.Logging;

namespace ConcertBookingApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            builder.Services.AddSingleton<ConcertOverviewPage>();
            builder.Services.AddSingleton<ConcertOverviewViewModel>();
            builder.Services.AddSingleton<ConcertDetailsViewModel>();
            builder.Services.AddSingleton<ConcertDetailsPage>();
            builder.Services.AddSingleton<BookingsPage>();
            builder.Services.AddSingleton<BookingsPageViewModel>();
            Microsoft.Maui.Handlers.EntryHandler.Mapper.AppendToMapping("NoUnderline", (h, v) =>
            {
#if ANDROID
				h.PlatformView.BackgroundTintList =
				Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToPlatform());
#endif
            });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
