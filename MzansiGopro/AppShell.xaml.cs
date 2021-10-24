using MzansiGopro.ViewModels;
using MzansiGopro.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using MzansiGopro.Views.EventsV;
using MzansiGopro.Views.AuthenticationV;
using MzansiGopro.Views.CompanyV;
using MzansiGopro.Views.ProfileV;
using MzansiGopro.Views.EventsV.AdminEventsV;

namespace MzansiGopro
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            // Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            //  Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            //  Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SignInPage), typeof(SignInPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(StoreSetupPage), typeof(StoreSetupPage));
            Routing.RegisterRoute(nameof(CompanyListPage), typeof(CompanyListPage));
            Routing.RegisterRoute(nameof(EventsListPage), typeof(EventsListPage));
            Routing.RegisterRoute(nameof(CompanyMainPage), typeof(CompanyMainPage));
            Routing.RegisterRoute(nameof(BusinessOfferEditPage), typeof(BusinessOfferEditPage));
            Routing.RegisterRoute(nameof(DisplayEventPage), typeof(DisplayEventPage));
            Routing.RegisterRoute(nameof(BusinessDisplayPage), typeof(BusinessDisplayPage));
            Routing.RegisterRoute(nameof(BusinessImagesEdit), typeof(BusinessImagesEdit));
            Routing.RegisterRoute(nameof(MainUserProfilePage), typeof(MainUserProfilePage));
            Routing.RegisterRoute(nameof(MainAdminEventsPage), typeof(MainAdminEventsPage));
            Routing.RegisterRoute(nameof(MainAddEventPage), typeof(MainAddEventPage));
            Routing.RegisterRoute(nameof(AdminDisplayEventPage), typeof(AdminDisplayEventPage));
            Routing.RegisterRoute(nameof(ForgotPasswordPage), typeof(ForgotPasswordPage));

        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}
