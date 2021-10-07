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

namespace MzansiGopro.ViewModels
{
   public class ShopViewModel : BaseViewModel
    {

        private bool isExpanded = false;
        bool isRefreashing = false;



       

        public Command<Shop> ShopTap { get; set; }
        public Command<Shop> ShopVisit { get; set; }
        public Command Expand { get; }
        public Command RefreshShop { get; }




        public ObservableCollection<Shop> ShopList { get; set; }
        public ObservableCollection<Pin> Pins { get; set; }
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
        }

      
        async void setData()
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
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
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
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Error While loading", "OK");
            }

            IsRefreashing = false;













        }



        



    }
}
