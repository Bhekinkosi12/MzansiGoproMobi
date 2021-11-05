using System;
using System.Collections.Generic;
using System.Text;

namespace MzansiGopro.ViewModels.BusinessVM
{
   public class LocatioinViewModel : BaseViewModel
    {
        string location;
        static string VM;


        public string Location
        {
            get => location;
            set
            {
                SetProperty(ref location, value);
                OnPropertyChanged(Location);
            }
        }







        
    }
}
