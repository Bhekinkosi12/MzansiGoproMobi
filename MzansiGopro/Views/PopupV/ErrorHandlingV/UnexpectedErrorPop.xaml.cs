using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views.PopupV.ErrorHandlingV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UnexpectedErrorPop : Popup
    {
        public UnexpectedErrorPop()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}