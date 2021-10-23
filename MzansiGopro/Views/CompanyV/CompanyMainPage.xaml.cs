using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.ViewModels.BusinessVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views.CompanyV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyMainPage : ContentPage
    {
        public CompanyMainPage()
        {
            InitializeComponent();
            SelectedLayout("Card");
        }

        protected override void OnAppearing()
        {
            var pageModel = BindingContext as AdminBusinessViewModel;
            pageModel.GetAdminShop();
            base.OnAppearing();
        }

        private void listBTN_Clicked(object sender, EventArgs e)
        {
            SelectedLayout("List");
        }

        private void cardBTN_Clicked(object sender, EventArgs e)
        {
            SelectedLayout("Card");
        }


        void SelectedLayout(string layout)
        {
            if(layout == "Card")
            {
                carfFrame.IsVisible = true;
                listFrame.IsVisible = false;

                cardBTN.BackgroundColor = Color.FromHex("#591da9");
                cardBTN.TextColor = Color.White;

                listBTN.BackgroundColor = Color.White;
                listBTN.BorderColor = Color.FromHex("#591da9");
                listBTN.BorderWidth = 1;
                listBTN.TextColor = Color.FromHex("#591da9");
                
            }
            else
            {
                carfFrame.IsVisible = false;
                listFrame.IsVisible = true;

                listBTN.BackgroundColor = Color.FromHex("#591da9");
                listBTN.TextColor = Color.White;

                cardBTN.BackgroundColor = Color.White;
                cardBTN.BorderColor = Color.FromHex("#591da9");
                cardBTN.BorderWidth = 1;
                cardBTN.TextColor = Color.FromHex("#591da9");
            }
        }


        void IsOpen(bool isOpen)
        {
            if (isOpen)
            {

                open.BackgroundColor = Color.FromHex("#591da9");
                open.TextColor = Color.White;

                close.BackgroundColor = Color.White;
                close.BorderColor = Color.FromHex("#591da9");
                close.BorderWidth = 1;
                close.TextColor = Color.FromHex("#591da9");

            }
            else
            {
                close.BackgroundColor = Color.FromHex("#591da9");
                close.TextColor = Color.White;

                open.BackgroundColor = Color.White;
                open.BorderColor = Color.FromHex("#591da9");
                open.BorderWidth = 1;
                open.TextColor = Color.FromHex("#591da9");
            }


        }


        private void open_Clicked(object sender, EventArgs e)
        {
            var pageModel = BindingContext as AdminBusinessViewModel;
            pageModel.IsOpen = true;

            IsOpen(true);
            

        }

        private void close_Clicked(object sender, EventArgs e)
        {
            var pageModel = BindingContext as AdminBusinessViewModel;
            pageModel.IsOpen = true;

            IsOpen(false);


        }
    }
}