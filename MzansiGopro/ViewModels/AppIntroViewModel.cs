using System;
using System.Collections.Generic;
using System.Text;
using Syncfusion.SfCarousel.XForms;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MzansiGopro.ViewModels
{
   public class AppIntroViewModel : BaseViewModel
    {

        public Command Next { get; }
        bool isCarouselVisible = false;

       public bool IsCarouselVisible
        {
            get => isCarouselVisible;
            set
            {
                SetProperty(ref isCarouselVisible, value);
                OnPropertyChanged(nameof(IsCarouselVisible));
            }
        }
        public int TabIndex { get; set; } = 0;
        public ObservableCollection<SfCarouselItem> CarouselList { get; set; }
        public ObservableCollection<SplashData> SplashData { get; set; }

        public AppIntroViewModel()
        {
            Next = new Command(OnNext);
            SplashData = new ObservableCollection<SplashData>()
            {
                 new SplashData
                 {
                       Image = "standOut.png",
                       Description = "Learn to showcase your business and stand out from the crowd"
                 },
                new SplashData
                 {
                       Image = "locationReview.png",
                       Description = "Help customers find your business and events"
                 },
                new SplashData
                 {
                       Image = "shopTab.png",
                       Description = "Tap the image to pin point the business before you visit it"
                 },
                new SplashData
                 {
                       Image = "",
                       Description = ""
                 }
            };

        }


       

        void OnNext()
        {
            if (IsCarouselVisible)
            {
                TabIndex += 1;
                OnPropertyChanged(nameof(TabIndex));
            }
            else
            {
                IsCarouselVisible = true;
            }
        }

    }

    public class SplashData
    {
        public string Image { get; set; }
        public string Description { get; set; }

    }
}
