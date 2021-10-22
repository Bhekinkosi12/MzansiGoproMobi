using MzansiGopro.Models;
using MzansiGopro.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using MzansiGopro.Models.CollectiveModel;



namespace MzansiGopro.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {

       public string APIKEY = "AIzaSyDmxGAWsjTu7ei3RJjJtiKCjsFWtnZL4O0";

        bool isBusy = false;
        public bool IsBusy
        {
            get { return isBusy; }
            set { SetProperty(ref isBusy, value); OnPropertyChanged(nameof(IsBusy)); }
        }

        string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetProperty(ref title, value); }
        }

       public static User RunTimeUser { get; set; }
        public static Shop RunTimeBusiness { get; set; }
        public static BusinessOffers RunTimeAdminOffer { get; set; }

        public static Shop SelectedShop { get; set; }

       static User currentUser = new User();



        public  User CurrentUser
        {
            get => currentUser;
            set
            {
                SetProperty(ref currentUser, value);
                
            }
        }

        

        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName] string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            var changed = PropertyChanged;
            if (changed == null)
                return;

            changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
