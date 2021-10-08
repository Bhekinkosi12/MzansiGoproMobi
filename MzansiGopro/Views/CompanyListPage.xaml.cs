using MzansiGopro.Models;
using MzansiGopro.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyListPage : ContentPage
    {
        public CompanyListPage()
        {
            InitializeComponent();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

            var model = BindingContext as ShopViewModel;
            ObservableCollection<Shop> _shops = new ObservableCollection<Shop>();
            List<Shop> _shopsList = new List<Shop>();

            _shopsList = model.ShopList.ToList();

            _shops = model.ShopList;
            var searchTerm = SearchBar.Text;
            if (string.IsNullOrWhiteSpace(searchTerm))
            {

                searchTerm = string.Empty;
            }


            searchTerm = searchTerm.ToLowerInvariant();





            var filteredItems = model.ShopList.Where(x => x.Name.ToLowerInvariant().Contains(searchTerm)).ToList();



            /*
            
            foreach(var _value in _shopsList)
            {
                if (!filteredItems.Contains(_value))
                {
                    _shopsList.Remove(_value);
                }
                else if (!_shopsList.Contains(_value))
                {
                    _shopsList.Add(_value);
                }
            }
            */


            for (var i = 0; i < _shopsList.Count; i++)
            {

                if (!filteredItems.Contains(_shopsList[i]))
                {
                    _shopsList.Remove(_shopsList[i]);
                }

                else if (!_shopsList.Contains(_shopsList[i]))
                {
                    _shopsList.Add(_shopsList[i]);
                }


            }




            /*
            foreach(var item in _shopsList)
            {
                _shops.Add(item);
            }

            */

            //model.ShopList.Clear();
            // model.ShopList = _shops;


            storeCollective.ItemsSource = _shopsList;

        }
    }
}