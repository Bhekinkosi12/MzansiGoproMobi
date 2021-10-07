using System;
using System.Collections.Generic;
using System.Text;
using Syncfusion.XForms.TabView;
using MzansiGopro.Views;
using MzansiGopro.Views.CompanyV;
using MzansiGopro.Views.EventsV;

namespace MzansiGopro.ViewModels
{
   public class TabbedViewModel
    {

        public TabItemCollection Pages { get; set; }


        public TabbedViewModel()
        {


            Pages = new TabItemCollection();

            var page1 = new SfTabItem
            {
                Content = new MainPage().Content,
                Title = "Main",

            };

            var page2 = new SfTabItem
            {
                Content = new EventsListPage().Content,
                Title = "Events",

            };


            Pages.Add(page1);
            Pages.Add(page2);
            

           

        }



    }
}
