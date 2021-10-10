﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MzansiGopro.Views.PopupV.IntroV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppIntroPop : Popup
    {
        public AppIntroPop()
        {
            InitializeComponent();
        }

        private void skip_Clicked(object sender, EventArgs e)
        {
            Dismiss(null);
        }

        private void next_Clicked(object sender, EventArgs e)
        {
            
        }

        private void carouselView_SwipeEnded(object sender, EventArgs e)
        {
            Dismiss(null);
        }
    }
}