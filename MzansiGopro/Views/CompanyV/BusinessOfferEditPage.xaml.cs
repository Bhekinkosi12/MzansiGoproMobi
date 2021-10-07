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
    public partial class BusinessOfferEditPage : ContentPage
    {
        public BusinessOfferEditPage()
        {
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
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
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
    }
}