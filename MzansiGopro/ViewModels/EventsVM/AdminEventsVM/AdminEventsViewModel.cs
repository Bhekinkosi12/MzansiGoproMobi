using System;
using System.Collections.Generic;
using System.Text;
using MzansiGopro.Models;
using MzansiGopro.Services;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Xamarin.Essentials;
using System.IO;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using MzansiGopro.Views.PopupV.SuccessNotifyV;


namespace MzansiGopro.ViewModels.EventsVM.AdminEventsVM
{
   public class AdminEventsViewModel : BaseViewModel
    {

        string name = "";
        string location = "";
        DateTime eventDate = new DateTime(2021, 08, 01);
        string cover = "";
        ObservableCollection<Events> hostedEvents = new ObservableCollection<Events>();



        static Events SelectedEvent { get; set; }
        public Command<Events> SelectEvent { get; set; }

        public AdminEventsViewModel()
        {
            SelectEvent = new Command<Events>(OnSelectedEvent);
        }




        public DateTime EventDate
        {
            get => eventDate;
            set
            {
                SetProperty(ref eventDate, value);
                OnPropertyChanged(nameof(EventDate));
            }
        }
        public string Cover
        {
            get => cover;
            set
            {
                SetProperty(ref cover, value);
                OnPropertyChanged(nameof(Cover));
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





        public ObservableCollection<Events> HostedEvents
        {
            get => hostedEvents;
            set
            {
                SetProperty(ref hostedEvents, value);
                OnPropertyChanged(nameof(HostedEvents));
            }
        }

        
        public async void OnLoadPage()
        {
            HostedEvents = new ObservableCollection<Events>();

            ObservableCollection<Events> _events = new ObservableCollection<Events>();

            try
            {

            foreach(var item in RunTimeUser.EventsHoted)
            {
                _events.Add(item);
            }

            HostedEvents = _events;
            }
            catch
            {

                var _user = RunTimeUser;

                _user.EventsHoted = new List<Events>();

                RunTimeUser = _user;



            }

        }



        public async void OnAddEvent()
        {
            SelectedEvent = null;

            var _event = new Events()
            {
                 Location = Location,
                  Name = Name,
                   Cover = Cover,
                    EventDateTime = EventDate,
                     ID = $"{RunTimeUser.Name};;{RunTimeUser.Email}"
            };


            var _user = RunTimeUser;

           

            


            UserDataBase userData = new UserDataBase();

            try
            {

           await userData.UpdateUserAsync(RunTimeUser);


                if(SelectedEvent != null)
                {
                    await userData.UpdateEvent(SelectedEvent, _event);
                    _user.EventsHoted.Remove(SelectedEvent);
                    _user.EventsHoted.Add(_event);
                }
                else
                {

                await userData.AddEventAsync(_event);
                    _user.EventsHoted.Add(_event);
                }

                RunTimeUser = _user;

                await Shell.Current.GoToAsync("..");
            }
            catch
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());

            }


            IsBusy = false;

        }


        public void GetSelectedEvent()
        {
           if(SelectedEvent != null)
            {
                Cover = SelectedEvent.Cover;
                Name = SelectedEvent.Name;
                EventDate = SelectedEvent.EventDateTime;
                Location = SelectedEvent.Location;
            }
            else
            {

            }

        }

       async void OnSelectedEvent(Events _event)
        {
            SelectedEvent = _event;
            await Shell.Current.GoToAsync("AdminDisplayEventPage");
        }
       public Events ReturnSelectedEvent()
        {
            return SelectedEvent;
        }
       public async void OnAddMediaCover()
        {
            IsBusy = true;

            UserDataStorage storage = new UserDataStorage();

            try
            {
                PickOptions pickOption = new PickOptions()
                {
                    FileTypes = FilePickerFileType.Images,
                    PickerTitle = "Please select images or video"
                };
                var picked = await storage.PickMedia(pickOption, "nameAndemail");

                var file = await picked.OpenReadAsync();

                var link = await storage.AddStoreStream(RunTimeUser.Email, picked.FileName, file as FileStream);



                Cover = link;

            }
            catch (Exception ex)
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }

            IsBusy = false;


        }





    }
}
