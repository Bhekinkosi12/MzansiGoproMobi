using System;
using System.Collections.Generic;
using System.Text;
using MzansiGopro.Models;
using MzansiGopro.Models.microModel;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Linq;
using MzansiGopro.Models.CollectiveModel;
using MzansiGopro.Services;
using Xamarin.Essentials;
using System.IO;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using MzansiGopro.Services.AuthSercurity;

namespace MzansiGopro.ViewModels.BusinessVM
{
   public class BusinessOfferEditViewModel : BaseViewModel
    {

        string offerName;
        string layout;
        List<Products> products = new List<Products>();
        Products selectedProduct;



        string selectedImage;
        string selectedName;
        string selectedPrice;
        bool isSelected = false;
        bool isDelete = false;
        string offerInput;




        ObservableCollection<offer> offer = new ObservableCollection<offer>();
        ObservableCollection<Products> productList = new ObservableCollection<Products>();
      

        public Command<Products> SelectItem { get; set; }
        public Command<Products> DeleteRequest { get; set; }

        public Command DeleteConfirm { get; }
        public Command DeleteDenied { get; }

        public Command AddItem { get; }
        public Command SaveEditedProduct { get; }

        public Command SaveEditedOffer { get; }

        public Command AddProduct { get; }

        public Command<offer> DeleteOffer { get; set; }





        public BusinessOfferEditViewModel()
        {
            defaultItem();
            GetData();
            SelectItem = new Command<Products>(OnSelectItem);
            AddItem = new Command(onAddItem);

            SaveEditedProduct = new Command(OnSaveEditedProduct);

            DeleteRequest = new Command<Products>(OnDeleteRequest);
            DeleteConfirm = new Command(OnDeleteItem);
            DeleteDenied = new Command(OnCancelDelete);
            SaveEditedOffer = new Command(OnSaveOffer);

            AddProduct = new Command(OnAddProduct);
            DeleteOffer = new Command<offer>(OnDeleteOffer);

            
        }
       static Products DeleteItem { get; set; }
        public ObservableCollection<Products> ProductList
        {
            get => productList;
            set
            {
                SetProperty(ref productList, value);
                OnPropertyChanged(nameof(ProductList));
            }
        }

        public bool IsDelete
        {
            get => isDelete;
            set
            {
                SetProperty(ref isDelete, value);
                OnPropertyChanged(nameof(IsDelete));
            }
        }

        public string OfferInput
        {
            get => offerInput;
            set
            {
                SetProperty(ref offerInput, value);
                OnPropertyChanged(nameof(OfferInput));
            }
        }

        public ObservableCollection<offer> Offer
        {
            get => offer;
            set
            {
                SetProperty(ref offer, value);
                OnPropertyChanged(nameof(Offer));
            }
        }


        public bool IsSelected
        {
            get => isSelected;
            set
            {
                SetProperty(ref isSelected, value);
                OnPropertyChanged(nameof(IsSelected));
            }
        }

        public string SelectedImage
        {
            get => selectedImage;
            set
            {
                SetProperty(ref selectedImage, value);
                OnPropertyChanged(nameof(SelectedImage));

            }
        }
        public string SelectedName
        {
            get => selectedName;
            set
            {
                SetProperty(ref selectedName, value);
                OnPropertyChanged(nameof(SelectedName));
            }
        }
        public string SelectedPrice
        {
            get => selectedPrice;
            set
            {
                SetProperty(ref selectedPrice, value);
                OnPropertyChanged(nameof(SelectedPrice));
            }
        }


        public Products SelectedProduct
        {
            get => selectedProduct;
            set
            {
                SetProperty(ref selectedProduct, value);
                OnPropertyChanged(nameof(SelectedProduct));
            }
        }

        public string OfferName
        {
            get => offerName;
            set
            {
                SetProperty(ref offerName, value);
                OnPropertyChanged(nameof(OfferName));
            }
        }
        public string Layout
        {
            get => layout;
            set
            {
                SetProperty(ref layout, value);
                OnPropertyChanged(nameof(Layout));
            }
        }

        public List<Products> Products
        {
            get => products;
           
            set
            {
                SetProperty(ref products, value);
                OnPropertyChanged(nameof(Products));
            }
        }

        ProductListModel LastSelectedListModel;



        void OnDeleteOffer(offer _offer)
        {
            Offer.Remove(_offer);
        }


        void GetData()
        {
            try
            {

                AdminBusinessViewModel admin = new AdminBusinessViewModel();
              var main =  admin.GetSelectedModel();
                LastSelectedListModel = main;;
                OfferName = main.ListName;
                Products = main.Products;
                Layout = main.Layout;


                foreach(var _product in main.Products)
                {
                    ProductList.Add(_product);
                }
            
            }
            catch (Exception ex)
            {
                
            }

                
        }
        

        void OnSelectItem(Products product)
        {
            if (products == null)
                return;

            IsSelected = true;
            SelectedImage = product.Image;
            SelectedName = product.Name;
            SelectedPrice = product.Price.ToString();
            SelectedProduct = product;

            foreach(var item in product.Ingredient)
            {
                Offer.Add(item);
            }
        }



        void defaultItem()
        {

            Offer = new ObservableCollection<offer>();
            ProductList = new ObservableCollection<Products>();

        }




        void onAddItem()
        {

           

            ObservableCollection<offer> _offer = new ObservableCollection<offer>();


            if (!string.IsNullOrEmpty(OfferInput)  && OfferInput.Length > 0)
            {
                var Currentitem = new offer()
                {
                    Name = OfferInput
                };

              
                    Offer.Add(Currentitem);


                



                OfferInput = "";


            }
            else
            {
                
            }

        }





       public void OnSaveEditedProduct()
        {
            List<offer> offers = new List<offer>();
            List<Products> _products = new List<Products>();
            ObservableCollection<Products> newProduct = new ObservableCollection<Products>();

            foreach(var _offer in Offer)
            {
                offers.Add(_offer);
            }


            var _product = new Products()
            {
                Id = Guid.NewGuid().ToString(),
                Image = SelectedImage,
                 Name = SelectedName,
                 Price = double.Parse(SelectedPrice),
                 Ingredient = offers

            };

            try
            {
            _products = Products;
            _products.Add(_product);


            newProduct.Add(_product);
                foreach(var item in ProductList)
                {
                    newProduct.Add(item);
            
                }

                newProduct.Remove(SelectedProduct);
                 _products.Remove(SelectedProduct);
            }
            catch
            {

            }
            
            
            ProductList.Clear();
            ProductList = newProduct;
           


            


      

            Products = _products;

           
        }






        public void OnDeleteRequest(Products _product)
        {
            IsDelete = true;
            if(_product != null)
            DeleteItem = _product;



        }

      public void OnDeleteItem()
        {
            ProductList.Remove(DeleteItem);
            IsDelete = false;
        }
        void OnCancelDelete()
        {
            IsDelete = false;
        }








       public async void OnAddMedia()
        {

            UserDataStorage storage = new UserDataStorage();




            try
            {

                PickOptions pickOption = new PickOptions()
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Please select images or video"
                };
                var picked = await storage.PickMedia(pickOption, "nameAndemail");

                var file = await picked.OpenReadAsync();



                var link = storage.AddStoreStream("Covers", picked.FileName, file as FileStream);

                

                SelectedImage = await link;

            }
            catch (Exception)
            {
                Shell.Current.ShowPopup(new InternetConnectionPop());
            }



        }




        public async void OnSaveOffer()
        {
            AdminBusinessViewModel businessViewModel = new AdminBusinessViewModel();

            List<Products> productslist = new List<Products>();

            foreach(var item in ProductList)
            {
                productslist.Add(item);
            }


            var product = new ProductListModel()
            {
                 Layout = Layout,
                  ListName = OfferName,
                   Products = productslist
            };





            ObservableCollection<ProductListModel> productLists = new ObservableCollection<ProductListModel>();
            List<ProductListModel> listModels = new List<ProductListModel>();


            var runTimeShop = RunTimeBusiness;


            
            listModels.Add(product);


            foreach (var item in RunTimeBusiness.BusinessOffers.OfferList)
            {
                if(item != LastSelectedListModel)
                {

                    listModels.Add(item);
                 }

             }

            listModels.Remove(LastSelectedListModel);
            runTimeShop.BusinessOffers.OfferList.Clear();
            runTimeShop.BusinessOffers.OfferList = listModels;

            RunTimeBusiness = runTimeShop;













            try
            {
                businessViewModel.SaveSelectedOffer(product);
                await Shell.Current.GoToAsync("../../CompanyMainPage");
            }
            catch 
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }


        }


        void OnAddProduct()
        {
            IsSelected = true;
            
        }




    }
}
