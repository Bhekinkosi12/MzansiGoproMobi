using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.ViewModels.BusinessVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;

namespace MzansiGopro.Views.CompanyV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BusinessOfferEditPage : ContentPage
    {
        public BusinessOfferEditPage()
        {
            var model = BindingContext as BusinessOfferEditViewModel;
           
            InitializeComponent();
        }

        private void editName_Tapped(object sender, EventArgs e)
        {
            if(labelEdit.Text == "Edit")
            {
                labelEdit.Text = "Done";
                labelOfferFrame.IsVisible = false;
                editNameFrame.IsVisible = true;
            }
            else
            {
                labelEdit.Text = "Edit";
                labelOfferFrame.IsVisible = true;
                editNameFrame.IsVisible = false;
            }
        }

        private async void productsave_Clicked(object sender, EventArgs e)
        {
            var model = BindingContext as BusinessOfferEditViewModel;


            try
            {

                if (!string.IsNullOrEmpty(model.SelectedName) && !string.IsNullOrEmpty(model.SelectedImage) )
                {
                    model.OnSaveEditedProduct();
                model.IsSelected = false;
                }
                else
                {

                }
            }
            catch
            {
               
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }






        }

        private void productCancel_Clicked(object sender, EventArgs e)
        {
            var model = BindingContext as BusinessOfferEditViewModel;

            model.IsSelected = false;
        }

        private void selectImageBox_Tapped(object sender, EventArgs e)
        {
            var model = BindingContext as BusinessOfferEditViewModel;
            model.OnAddMedia();
        }

        private async void CancelOffer_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void EntryPrice_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(EntryPrice.Text,out double a))
            {
                EntryPrice.TextColor = Color.Green;
            }
            else
            {
                EntryPrice.TextColor = Color.Red;
            }
        }

        private void cardBTN_Clicked(object sender, EventArgs e)
        {
            onCardView();
        }

        private void listBTN_Clicked(object sender, EventArgs e)
        {
            onListView();
        }



       void onListView()
        {
            listBTN.BackgroundColor = Color.FromHex("#591da9");
            listBTN.TextColor = Color.White;

            cardBTN.BackgroundColor = Color.White;
            cardBTN.TextColor = Color.FromHex("#591da9");
            cardViewFrame.IsVisible = false;
           listViewFrame.IsVisible = true;
        }
        void onCardView()
        {
            cardBTN.BackgroundColor = Color.FromHex("#591da9");
            cardBTN.TextColor = Color.White;

            listBTN.BackgroundColor = Color.White;
            listBTN.TextColor = Color.FromHex("#591da9");
           cardViewFrame.IsVisible = true;
           listViewFrame.IsVisible = false;
        }
    }
}