using System;
using System.Collections.Generic;
using System.Text;
using MzansiGopro.Models.CollectiveModel;
using MzansiGopro.Models.microModel;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using MzansiGopro.Services.BusinessData;
using MzansiGopro.Services;
using Xamarin.Essentials;
using System.IO;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;


namespace MzansiGopro.ViewModels.BusinessVM
{
    public class AdminBusinessViewModel : BaseViewModel
    {

        public static ProductListModel OfferSelected { get; set; }
        public static image Seletedimage { get; set; }
        
        public AdminBusinessViewModel()
        {
           
            GetAdminShop();
             EditProductList = new Command<ProductListModel>(OnEditOffer);
            SelectImage = new Command<image>(OnSelectedImage);
            AddStoreImage = new Command(OnAddStoreImage);
            AddOffer = new Command(OnAddOffer);
        }

        string name;
        string coverImage;
        string location;
        string email; 
        string number;
        string points;
        string visits;
        string bio;
        
       ObservableCollection<ProductListModel> productModel;
        ObservableCollection<ProductListModel> productModelList;
        ObservableCollection<image> storeImages = new ObservableCollection<image>(); 

        public Command<ProductListModel> EditProductList { get; set; }
        public Command<image> SelectImage { get; set; }
        public Command AddStoreImage { get; }

        public Command AddOffer { get; }


        public string Bio
        {
            get => bio;
            set
            {
                SetProperty(ref bio, value);
                OnPropertyChanged(nameof(Bio));
            }
        }


        public string Points
        {
            get => points;
            set
            {
                SetProperty(ref points, value);
            }
        }
        public string Visits
        {
            get => visits;
            set
            {
                SetProperty(ref visits, value);
                OnPropertyChanged(nameof(Visits));
            }
        }
             
        public ObservableCollection<image> StoreImages
        {
            get => storeImages;
            set
            {
                SetProperty(ref storeImages, value);
                OnPropertyChanged(nameof(StoreImages));
            }
        }

        public ObservableCollection<ProductListModel> ProductModelList
        {
            get => productModelList;
            set
            {
                SetProperty(ref productModelList, value);
                OnPropertyChanged(nameof(ProductModelList));

            }
        }
        public ObservableCollection<ProductListModel> Productmodel
        {
            get => productModel;
            set
            {
                SetProperty(ref productModel, value);
                OnPropertyChanged(nameof(Productmodel));
            }
        }


        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                OnPropertyChanged(nameof(Name));
            }
        }
        public string CoverImage
        {
            get => coverImage;
            set
            {
                SetProperty(ref coverImage, value);
                OnPropertyChanged(nameof(CoverImage));
            }
        }

        public string Location
        {
            get => location;
            set
            {
                SetProperty(ref location, value);
                OnPropertyChanged(nameof(Location));
            }
        }
        public string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                OnPropertyChanged(nameof(Email));
            }
        }

        public string Number
        {
            get => number;
            set
            {
                SetProperty(ref number, value);
                OnPropertyChanged(nameof(Number));
            }
        }






        void OnAddOffer()
        {
            var productlist = new ProductListModel()
            {
                Layout = "Card",
                ListName = "Offer name edit",
                Products = new List<Models.Products>(),
                ListID = ""
            };

            var shop = RunTimeBusiness;

            List<ProductListModel> list_product = new List<ProductListModel>();

           var list = shop.BusinessOffers.OfferList;
            list_product.Add(productlist);

            foreach(var i in list)
            {
                list_product.Add(i);
            }

            shop.BusinessOffers.OfferList = list_product;

            RunTimeBusiness = shop;

            GetAdminShop();
        }



       public void NewbusinessStart()
        {
            ObservableCollection<ProductListModel> proMV = new ObservableCollection<ProductListModel>();

            foreach (var offer in RunTimeBusiness.Offers)
            {
                var _offer = new ProductListModel()
                {
                     ListID = Guid.NewGuid().ToString(),
                      ListName = offer.Name,
                       Layout = "List"
                };
                proMV.Add(_offer);
            }

            Productmodel = proMV;

        }


       async void OnSelectedImage(image _image)
        {
            Seletedimage = _image;
            await Shell.Current.GoToAsync("BusinessImagesEdit");
        }

      async void OnEditOffer(ProductListModel _productList)
        {
            try
            {

            if(_productList != null)
            {
                OfferSelected = _productList;
                await Shell.Current.GoToAsync("BusinessOfferEditPage");
            }
            }
            catch
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }
        }


        public ProductListModel GetSelectedModel()
        {
            return OfferSelected;
        }
        public image GetSelectedImage()
        {
            return Seletedimage;
        }


        public async void SaveSelectedOffer(ProductListModel _productListModel)
        {
            IsBusy = true;
         

           

            UserDataBase dataBase = new UserDataBase();
           
            try
            {

           await dataBase.UpDateBusinessAsync(RunTimeBusiness);
            }
            catch
            {
                Shell.Current.ShowPopup(new InternetConnectionPop());

            }
            finally
            {

            GetAdminShop();

            IsBusy = false;
            }

        }

      




      
       public  void GetAdminShop()
        {
            try
            {

                ObservableCollection<image> images_ = new ObservableCollection<image>();
                CoverImage = RunTimeBusiness.Cover_Img;
                Name = RunTimeBusiness.Name;
                Email = RunTimeBusiness.Email;
                Number = RunTimeBusiness.Number;
                Visits = $"{RunTimeBusiness.Visits}";
                Points = $"{RunTimeBusiness.Points}pts";
                foreach(var item in RunTimeBusiness.StoreImage)
                {
                    images_.Add(item);

                }
                StoreImages = images_;


                ObservableCollection<ProductListModel> listModels = new ObservableCollection<ProductListModel>();
                ObservableCollection<ProductListModel> listModelsList = new ObservableCollection<ProductListModel>();

                foreach (var iten in RunTimeBusiness.BusinessOffers.OfferList)
                {
                    if(iten.Layout == "Card")
                    {

                    listModels.Add(iten);
                    }
                    else
                    {
                        listModelsList.Add(iten);
                    }
                }


                Productmodel = listModels;
                ProductModelList = listModelsList;
            }
            catch
            {
               // await Shell.Current.GoToAsync("LoginPage");
            }

        }



        async void OnAddStoreImage()
        {
            IsBusy = true;

            UserDataStorage storage = new UserDataStorage();
            UserDataBase userData = new UserDataBase();

            try
            {
                PickOptions pickOption = new PickOptions()
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Please select images or video"
                };
                var picked = await storage.PickMedia(pickOption, "nameAndemail");

                var file = await picked.OpenReadAsync();



                var link = await storage.AddStoreStream(RunTimeUser.Email, picked.FileName, file as FileStream);

                var _image = new image()
                {
                     Url = link
                };

                var shop = RunTimeBusiness;
                shop.StoreImage.Add(_image);

                RunTimeBusiness = shop;


              await userData.UpDateBusinessAsync(RunTimeBusiness);

                // CoverImage = link;

                StoreImages.Add(_image);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Unexpected Error", "OK");
            }

            IsBusy = false;


        }


    }
}
