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
    public partial class CompanyMainPage : ContentPage
    {
        public CompanyMainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            var pageModel = BindingContext as AdminBusinessViewModel;
            pageModel.GetAdminShop();
            base.OnAppearing();
        }



    }
}