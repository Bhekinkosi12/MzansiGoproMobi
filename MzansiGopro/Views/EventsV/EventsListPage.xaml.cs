using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MzansiGopro.ViewModels.EventsVM;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views.EventsV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventsListPage : ContentPage
    {
        public EventsListPage()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            var model = BindingContext as EventsListViewModel;

           
           await model.OnRefreshEvents();
            base.OnAppearing();
        }

    }
}