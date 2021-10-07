using System;
using System.Collections.Generic;
using System.Text;
using MzansiGopro.Services.EventsSV;
using System.Collections.ObjectModel;
using MzansiGopro.Models;
using MzansiGopro.Services;
using Xamarin.Forms;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using MzansiGopro.Views.PopupV.SuccessNotifyV;

namespace MzansiGopro.ViewModels.EventsVM
{
   public class EventsListViewModel : BaseViewModel
    {


         bool _isRefreshing;
        public bool IsRefreshingEvent
        {
            get => _isRefreshing;
            set
            {
                SetProperty(ref _isRefreshing, value);
                OnPropertyChanged(nameof(IsRefreshingEvent));
            }
        }

        ObservableCollection<Events> eventList = new ObservableCollection<Events>();

       public ObservableCollection<Events> EventsList
        {
            get => eventList;
            set
            {
                SetProperty(ref eventList, value);
                OnPropertyChanged(nameof(EventsList));
            }
        }
        public ObservableCollection<News> NewsList { get; set; }

        static Events SelectedEvent { get; set; }

        public Command<Events> SelectEvent { get; set; }
        public Command RefreshEvents { get; set; }

        public EventsListViewModel()
        {
            getData();
            getNews();

            SelectEvent = new Command<Events>(OnSelectEvent);
            RefreshEvents = new Command(async () => await OnRefreshEvents());
        }


      public async Task OnRefreshEvents()
        {
            IsRefreshingEvent = true;
            UserDataBase userData = new UserDataBase();
            try
            {
                var list = await userData.GetAllEvents();

                EventsList.Clear();

                foreach(var i in list)
                {
                    EventsList.Add(i);
                }

                
            }
            catch
            {
                Shell.Current.ShowPopup(new InternetConnectionPop());
            }

            IsRefreshingEvent = false;

        }

        void getData()
        {
            EventsServices servers = new EventsServices();
            ObservableCollection<Events> events = new ObservableCollection<Events>();


            foreach(var even in servers.GetAllEvents())
            {
                events.Add(even);
            }

            EventsList = events;


        }

        void getNews()
        {
            EventsServices servers = new EventsServices();
            ObservableCollection<News> newslist = new ObservableCollection<News>();

            foreach(var news in servers.GetAllNews())
            {
                newslist.Add(news);
            }


            NewsList = newslist;

        }



        public Events GetSelectedEvent()
        {
            return SelectedEvent;
        }


       async void OnSelectEvent(Events _events){
            SelectedEvent = _events;
            await Shell.Current.GoToAsync("DisplayEventPage");
        }



    }
}
