using System;
using System.Collections.Generic;
using System.Text;
using MzansiGopro.Models.CollectiveModel;
using MzansiGopro.Models.microModel;
using MzansiGopro.Models;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using MzansiGopro.Services.BusinessData;
using MzansiGopro.Services;

namespace MzansiGopro.ViewModels.BusinessVM
{
   public class BusinessDisplayViewModel : BaseViewModel
    {

        string name;
        string coverImage;
        string location;
        string email;
        string number;

        ObservableCollection<ProductListModel> productModel = new ObservableCollection<ProductListModel>();

        ObservableCollection<ProductListModel> productModelList = new ObservableCollection<ProductListModel>();
        ObservableCollection<image> storeImages = new ObservableCollection<image>();

        public BusinessDisplayViewModel()
        {
            Name = SelectedShop.Name;
            CoverImage = SelectedShop.Cover_Img;
            Location = SelectedShop.Location;
            Email = SelectedShop.Email;
            Number = SelectedShop.Number;

            ObservableCollection<ProductListModel> CardList = new ObservableCollection<ProductListModel>();
            ObservableCollection<image> _storeImages = new ObservableCollection<image>();

            foreach (var item in SelectedShop.BusinessOffers.OfferList)
            {
                CardList.Add(item);
            }

            productModel = CardList;

            foreach(var item in SelectedShop.StoreImage)
            {
                _storeImages.Add(item);
            }
            StoreImages = _storeImages;




        }


        public BusinessDisplayViewModel(Shop shop)
        {
            
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
        public ObservableCollection<image> StoreImages
        {
            get => storeImages;
            set
            {
                SetProperty(ref storeImages, value);
                OnPropertyChanged(nameof(StoreImages));
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







    }
}
