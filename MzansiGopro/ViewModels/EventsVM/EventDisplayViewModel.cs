using System;
using System.Collections.Generic;
using System.Text;

using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using MzansiGopro.Models.microModel;
using MzansiGopro.Services;
using MzansiGopro.Services.EventsSV;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;


namespace MzansiGopro.ViewModels.EventsVM
{
   public class EventDisplayViewModel : BaseViewModel
    {

        string location;
        string image;
        string name;
        string peopleGoing;
        ObservableCollection<Pin> pins = new ObservableCollection<Pin>();

        public EventDisplayViewModel()
        {
            GetEvent();
        }

        public string PeopleGoing
        {
            get => peopleGoing;
            set
            {
                SetProperty(ref peopleGoing, value);
                OnPropertyChanged(nameof(PeopleGoing));
            }
        }
        public string Name
        {
            get => name;
            set
            {
                SetProperty(ref name, value);
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Location
        {
            get => location;
            set
            {
                SetProperty(ref location, value);
                OnPropertyChanged(nameof(Location));
            }
        }


        public string Image
        {
            get => image;
            set
            {
                SetProperty(ref image, value);
                OnPropertyChanged(nameof(Image));
            }
        }

       public ObservableCollection<Pin> Pins
        {
            get => pins;
            set
            {
                SetProperty(ref pins, value);
                OnPropertyChanged(nameof(Pins));
            }
        }



        void GetEvent()
        {
            EventsListViewModel eventsList = new EventsListViewModel();
            ObservableCollection<Pin> _pins = new ObservableCollection<Pin>();
            var events = eventsList.GetSelectedEvent();

            Location = events.Location;
            Image = events.Cover;
            Name = events.Name;
            PeopleGoing = $"{events.UsersComing} going";
            string EventDateNotify;
            int dateCompare = DateTime.Compare(events.EventDateTime, DateTime.Today);

            if(dateCompare == 0)
            {
                EventDateNotify = "Live Event";
            }
            else
            {
                EventDateNotify = "Upcoming Event";
            }




            var location = events.Location.Split(';');
            Pin pin = new Pin()
            {
                Address = EventDateNotify,
                Label = events.Name,
                Position = new Position(double.Parse(location[0]), double.Parse(location[1])),
                Type = PinType.Place


            };

            _pins.Add(pin);
            Pins = _pins;

            
            

        }


        
      public async void OnGoingToEvent()
        {
            UserDataBase userData = new UserDataBase();
            EventsServices eventsServices = new EventsServices();
            EventsListViewModel eventsList = new EventsListViewModel();
            try
            {
                var events = eventsList.GetSelectedEvent();
                var user = RunTimeUser;

                if (!user.EventsGoing.Contains(events))
                {

                    user.EventsGoing.Add(events);

                    RunTimeUser = user;

                    try
                    {

                    await userData.UpdateUserAsync(RunTimeUser);
                    }

                    catch
                    {

                    }


                    var ChangedEvent = events;
                    ChangedEvent.UsersComing += 1;

                    try
                    {
                      var IsUpdated = await eventsServices.UpdateUserEventstatus(events.ID, events, ChangedEvent);
                    }
                    catch
                    {
                         Shell.Current.ShowPopup(new InternetConnectionPop());
                    }




                }
                else
                {
                    Shell.Current.ShowPopup(new UnexpectedErrorPop());
                }

                


            }
            catch
            {
                await Shell.Current.GoToAsync("LoginPage");
            }


        }


    }
}
