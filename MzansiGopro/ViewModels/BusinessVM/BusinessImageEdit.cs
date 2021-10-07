using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using MzansiGopro.Models.microModel;
using MzansiGopro.Services;


namespace MzansiGopro.ViewModels.BusinessVM
{
   public class BusinessImageEdit : BaseViewModel
    {

        string image;

       static string originalImage { get; set; }

        public string Image
        {
            get => image;
            set
            {
                SetProperty(ref image, value);
                OnPropertyChanged(nameof(Image));
            }
        }


        public Command Delete { get;  }
        public Command Save { get; }
        public Command Cancel { get; }


        public BusinessImageEdit()
        {
            GetImage();
            Delete = new Command(OnDelete);
            Save = new Command(OnSave);
            Cancel = new Command(OnCancel);
        }

        async void OnCancel()
        {
            await Shell.Current.GoToAsync("..");
        }

       async void OnDelete()
        {
            AdminBusinessViewModel adminBusiness = new AdminBusinessViewModel();
            var imageModel = adminBusiness.GetSelectedImage();
            var shop = RunTimeBusiness;
            var Deleteimage = new image()
            {
                 Url = originalImage
            };

            shop.StoreImage.Remove(imageModel);

            RunTimeBusiness = shop;

            UserDataBase dataBase = new UserDataBase();

            try
            {

                await dataBase.UpDateBusinessAsync(RunTimeBusiness);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Check Internet Connection and try again", "OK");
            }



           

        }

       async void OnSave()
        {
            var shop = RunTimeBusiness;
            var Saveimage = new image()
            {
                Url = Image
            };

            shop.StoreImage.Add(Saveimage);

            RunTimeBusiness = shop;

            UserDataBase dataBase = new UserDataBase();

            try
            {

                await dataBase.UpDateBusinessAsync(RunTimeBusiness);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Check Internet Connection and try again", "OK");
            }



           
        }


        void GetImage()
        {
            AdminBusinessViewModel adminBusiness = new AdminBusinessViewModel();
            var imageModel = adminBusiness.GetSelectedImage();

            Image = imageModel.Url;
            originalImage = imageModel.Url;

        }



    }
}
