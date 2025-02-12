﻿using CommunityToolkit.Maui;
using ConcertBookingApp.Services;
using ConcertBookingApp.ViewModels;
using ConcertBookingApp.ViewModels.ConcertsOverviewViewModels;
using ConcertBookingApp.ViewModels.PaymentViewModels;
using ConcertBookingApp.Views;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using Microsoft.Maui.Platform;
using SharedResources.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using ConcertBookingApp.ViewModels.BookingViewModels;
using Microsoft.EntityFrameworkCore.Storage;
using ConcertBookingApp.Data.Database;
using ConcertBookingApp.ViewModels.CheckoutViewModels;
using ConcertBookingApp.ViewModels.ConcertDetailsViewModels;
using ConcertBookingApp.ViewModels.ConfirmationViewModels;
using ConcertBookingApp.ViewModels.LoginViewModels;


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


            builder.Services.AddSingleton<ConcertOverviewViewModel>();
            builder.Services.AddTransient<ConcertDetailsViewModel>();
            builder.Services.AddTransient<CheckoutViewModel>();
            builder.Services.AddTransient<PaymentViewModel>();
            builder.Services.AddTransient<BookingViewModel>();
            builder.Services.AddTransient<ConfirmationViewModel>();
            builder.Services.AddTransient<LoginViewModel>();

            builder.Services.AddAutoMapper(typeof(MappingProfile));

            builder.Services.AddSingleton<BookingService>();
            builder.Services.AddSingleton<ConcertService>();
            builder.Services.AddSingleton<UserService>();


            builder.Services.AddSingleton<ConcertOverviewPage>();
            builder.Services.AddTransient<ConcertDetailsPage>();
            builder.Services.AddTransient<CheckoutPage>();
            builder.Services.AddTransient<PaymentPage>();
            builder.Services.AddTransient<BookingsPage>();
            builder.Services.AddTransient<ConfirmationPage>();
            builder.Services.AddTransient<LoginPage>();

            


            builder.Services.AddSingleton(x =>
                new HttpClient { BaseAddress = new Uri("http://10.0.2.2:7139") });
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
            Preferences.Clear();
           
            return builder.Build();
        }
    }
}
