using System;
using System.Collections.Generic;
using System.Text;
using MzansiGopro.Models;
using MzansiGopro.Services;
using System.Collections;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using MzansiGopro.Models.microModel;
using System.Threading.Tasks;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using Xamarin.CommunityToolkit.Extensions;

namespace MzansiGopro.ViewModels
{
   public class ShopViewModel : BaseViewModel
    {

        private bool isExpanded = false;
        bool isRefreashing = false;
        ObservableCollection<Shop> shopList;
        ObservableCollection<Pin> pins;





        public Command<Shop> ShopTap { get; set; }
        public Command<Shop> ShopVisit { get; set; }
        public Command Expand { get; }
        public Command RefreshShop { get; }
        public Command<offer> FilterTap { get; set; }




        public ObservableCollection<Shop> ShopList
        {
            get => shopList;
            set
            {
                SetProperty(ref shopList, value);
                OnPropertyChanged(nameof(ShopList));
            }
        }

        public ObservableCollection<Pin> Pins
        {
            get => pins;
            set
            {
                SetProperty(ref pins, value);
                OnPropertyChanged(nameof(Pins));
            }
        }
        public ObservableCollection<offer> Filter { get; set; }



        public bool IsExpanded
        {
            get => isExpanded;
            set
            {
                SetProperty(ref isExpanded, value);
                OnPropertyChanged(nameof(IsExpanded));
            }
        }

        public bool IsRefreashing
        {
            get => isRefreashing;
            set
            {
                SetProperty(ref isRefreashing, value);

                if (IsRefreashing)
                {

                }

                OnPropertyChanged(nameof(IsRefreashing));

            }
        }




     


        public string CurrentLocation { get; set; }

        UserDataBase userData = new UserDataBase();

        
        public ShopViewModel()
        {
        
            setData();
            ShopTap = new Command<Shop>(OnShopTap);
            defualtSearch();
            Expand = new Command(OnExpand);

            RefreshShop = new Command(UpDateShopList);

            ShopVisit = new Command<Shop>(OnShopVisit);
            FilterTap = new Command<offer>(async (e) => await OnFilterTap(e));
        }






      
         void setData()
        {

            Pins = new ObservableCollection<Pin>();

            try
            {
                ObservableCollection<Pin> _pins = new ObservableCollection<Pin>();
                ObservableCollection<Shop> shops = new ObservableCollection<Shop>();
                MockDataStore data = new MockDataStore();

                var items = data.ReturnList();

            foreach(var i in items)
            {
                    var location = i.Location.Split(';');
                    Pin pin = new Pin()
                    {
                        Address = toString(i.Offers),
                        Label = i.Name,
                        Position = new Position(double.Parse(location[0]), double.Parse(location[1])),
                        Type = PinType.Place
                        
                        
                    };


                    _pins.Add(pin);
                shops.Add(i);
                    
            }
                Pins = _pins;
                ShopList = shops;



            }
            catch (Exception ex)
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }


        }



        void OnShopTap(Shop stop)
        {
            CurrentLocation = stop.Location;
        }


        string toString(List<offer> lists)
        {
            string a = "";
            foreach(var str in lists)
            {
                a += str.Name;
            }
            return a;
        }


        void defualtSearch()
        {

            Filter = new ObservableCollection<offer>()
            {
                new offer
                {
                    Name = "Kota"
                }, new offer
                {
                    Name = "Shisa Nyama"
                }, new offer
                {
                    Name = "Salon"
                }, new offer
                {
                    Name = "Smooties"
                }, 
                new offer
                {
                    Name = "Biskits"
                },
            };





        }




        void OnExpand()
        {
            if (IsExpanded)
            {
                IsExpanded = false;

            }
            else
            {
                IsExpanded = true;
            }
        }

       async void OnShopVisit(Shop shop)
        {
            SelectedShop = shop;
            await Shell.Current.GoToAsync("BusinessDisplayPage");

        }



        public async void UpDateShopList()
        {
            IsRefreashing = true;
            UserDataBase userDB = new UserDataBase();
            try
            {
               var _shopList = await userData.GetAllBusiness();


                if(_shopList != null)
                {
                    ObservableCollection<Pin> _pins = new ObservableCollection<Pin>();
                    ShopList.Clear();
                    foreach(var shop in _shopList)
                    {
                        ShopList.Add(shop);







                        var location = shop.Location.Split(';');
                        Pin pin = new Pin()
                        {
                            Address = toString(shop.Offers),
                            Label = shop.Name,
                            Position = new Position(double.Parse(location[0]), double.Parse(location[1])),
                            Type = PinType.Place


                        };


                        _pins.Add(pin);
                       





                    }
                    Pins.Clear();
                    Pins = _pins;
                }

            }
            catch 
            {
                Shell.Current.ShowPopup(new InternetConnectionPop());
            }
            finally
            {

            IsRefreashing = false;
            }














        }


        public async Task OnFilterTap(offer _offer)
        {
            try
            {

                string offerName = _offer.Name.ToLower();
                var _shopList = ShopList;
                ShopList.Clear();

                foreach(var item in _shopList)
                {
                    foreach(var i in item.Offers)
                    {
                        if (i.Name.ToLower().Contains(offerName))
                        {
                            ShopList.Add(item);
                        }
                    }
                }
            }
            catch
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }

        }

        



    }
}
