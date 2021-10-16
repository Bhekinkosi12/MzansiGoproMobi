using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views.CompanyV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessDisplayPage : ContentPage
    {
        public BusinessDisplayPage()
        {
            InitializeComponent();
            
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            SelectedLayout("Card");
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
            if (layout == "Card")
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





    }
}