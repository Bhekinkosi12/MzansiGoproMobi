using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using MzansiGopro.Models.microModel;
using NativeMedia;
using MzansiGopro.Services;
using Xamarin.Essentials;
using System.IO;
using MzansiGopro.Models;
using MzansiGopro.Models.CollectiveModel;
using MzansiGopro.Services.AuthSercurity;
using MzansiGopro.Services.LocalData;
using Xamarin.CommunityToolkit.Extensions;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using MzansiGopro.Views.PopupV.SuccessNotifyV;

namespace MzansiGopro.ViewModels.AuthenticationVM
{
   public class SignInVM : BaseViewModel
    {

        private string name;
        private string email;
        private string number;
        private string password;
        private string confirmPass;
        private bool isShop = false;
        private string shopName;
        private string offerInput;
        private int imageUploadProgress;
        string coverImage;
        bool createProgress;
        private string location;

        private ObservableCollection<offer> offer = new ObservableCollection<offer>();
        private ObservableCollection<offer> extraOffer;
        private ObservableCollection<image> images = new ObservableCollection<image>();

       


        private string defaultColor = "#191919";
        string errorColor = "#800000";
        string successColor = "#009000";
        private string numshadowColor = "#191919";
        






        public Command SignIn { get; }
        public Command AddItem { get; }
        public Command AddExtraItem { get; }
        public Command AddMedia { get; }
        public Command AddMediaCover { get; }

        public Command<offer> DeleteOffer { get; set; }








        public SignInVM()
        {
            SignIn = new Command(Onsignin);
           // defaultItem();
            AddItem = new Command(onAddItem);
            AddExtraItem = new Command(onAddExtraItem);
            AddMedia = new Command(OnAddMedia);
            AddMediaCover = new Command(OnAddMediaCover);
            DeleteOffer = new Command<offer>(OnDeleteOffer);
        }


        public string CoverImage
        {
            get => coverImage;
            set
            {
                SetProperty(ref coverImage, value);
                OnPropertyChanged(nameof(CoverImage));
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

        public bool CreateProgress
        {
            get => createProgress;
            set
            {
                SetProperty(ref createProgress, value);
                OnPropertyChanged(nameof(CreateProgress));
            }
        }

        public int ImageUploadProgress
        {
            get => imageUploadProgress;
            set
            {
                SetProperty(ref imageUploadProgress, value);
                OnPropertyChanged(nameof(ImageUploadProgress));
            }
        }





        public ObservableCollection<image> Images
        {
            get => images;
            set
            {
                SetProperty(ref images, value);
                OnPropertyChanged(nameof(Images));
            }
        }


        public string OfferInput
        {
            get => offerInput;
            set
            {
                SetProperty(ref offerInput, value);
                OnPropertyChanged(nameof(OfferInput));
            }
        }
        public string ExtraOfferInput
        {
            get => offerInput;
            set
            {
                SetProperty(ref offerInput, value);
                OnPropertyChanged(nameof(ExtraOfferInput));
            }
        }

        public ObservableCollection<offer> Offer
        {
            get => offer;
            set
            {
                SetProperty(ref offer, value);
                OnPropertyChanged(nameof(Offer));
            }
        }
        public ObservableCollection<offer> ExtraOffer
        {
            get => extraOffer;
            set
            {
                SetProperty(ref extraOffer, value);
                OnPropertyChanged(nameof(ExtraOffer));
            }
        }


        public string NumShadowColor
        {
            get => numshadowColor;
            set
            {
                SetProperty(ref numshadowColor, value);
                OnPropertyChanged(nameof(NumShadowColor));
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
        public string Email
        {
            get => email;
            set
            {
                SetProperty(ref email, value);
                OnPropertyChanged(nameof(Email));
            }
        }
        public string Number
        {
            get => number;
            set
            {
                SetProperty(ref number, value);
                colorGenerate(value);
                OnPropertyChanged(nameof(Number));
            }
        }

        public string Password
        {
            get => password;
            set
            {
                SetProperty(ref password, value);
                OnPropertyChanged(nameof(Password));
            }
        }

        public string ConfirmPassword
        {
            get => confirmPass;
            set
            {
                SetProperty(ref confirmPass, value);
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public bool IsShop
        {
            get => isShop;
            set
            {
                SetProperty(ref isShop, value);
                OnPropertyChanged(nameof(IsShop));
            }
        }

        public string ShopName
        {
            get => shopName;
            set
            {
                SetProperty(ref shopName, value);
                OnPropertyChanged(nameof(ShopName));
            }
        }






        void colorGenerate(string number)
        {
            if(number.Length < 6)
            {
                NumShadowColor = defaultColor;
            }
            else if(number.Length == 10)
            {
                NumShadowColor = successColor;
            }
            else
            {
                NumShadowColor = errorColor;
            }

        }






      public async void Onsignin()
        {
            
            PasswordAbcHash abcHash = new PasswordAbcHash();
            UserDataBase userDB = new UserDataBase();
            LocalUserService localDB = new LocalUserService();
            if (IsShop == true)
            {





                var user = new User()
                {
                   
                    Email = Email,
                    Location = Location,
                    IsShop = IsShop,
                    Name = Name,
                    Number = Number,
                    PasswordConfigID = "std",
                    Password = abcHash.StandardPasswordHash(Password),
                    AutomatedId = $"{Email};;{Name}"

                };

                CurrentUser = user;
                RunTimeUser = user;

                localDB.AddUser(RunTimeUser);




                await Shell.Current.GoToAsync("StoreSetupPage");
            }
            else
            {

                var user = new User()
                {
                    
                    Email = Email,

                    IsShop = IsShop,
                    Name = Name,
                    Number = Number,
                    PasswordConfigID = "std",
                    Password = abcHash.StandardPasswordHash(Password),
                    AutomatedId = $"{Email};;{Name}",
                     EventsHoted = new List<Events>(),
                      EventsGoing = new List<Events>()

                };

                RunTimeUser = user;
                localDB.AddUser(RunTimeUser);

                var complete = await userDB.AddUserAsync(user);
                if (complete)
                {
                    await userDB.AddEmailAndPassword(Email,abcHash.StandardPasswordHash(Password), IsShop);
                    Shell.Current.ShowPopup(new SuccessSigninPop());
                    await Shell.Current.GoToAsync("//TabbedPage");
                }
                else
                {
                    Shell.Current.ShowPopup(new InternetConnectionPop());

                }


            }

            IsBusy = false;

        }


        void defaultItem()
        {

            if(Offer == null)
            {
                Offer = new ObservableCollection<offer>()
                {
                    new offer
                    {
                        Name = "Add"
                    }
                };
            }

            if (ExtraOffer == null)
            {
                ExtraOffer = new ObservableCollection<offer>()
                {
                    new offer
                    {
                        Name = "Add"
                    }
                };
            }

        }


        void onAddItem()
        {

            var item = new offer()
            {
                Name = "Add"
            };
          
            ObservableCollection<offer> _offer = new ObservableCollection<offer>();


            if (OfferInput != null || OfferInput.Length > 0)
            {
                var Currentitem = new offer()
                {
                    Name = OfferInput
                };

                var firstItem = Offer[0];

                if (firstItem.Name == "Add")
                {
                    Offer.Clear();
                    Offer = _offer;
                    Offer.Add(Currentitem);

                    
                    
                }
                else
                {
                    Offer.Add(Currentitem);

                   
                }



                OfferInput = "";


            }
            else
            {

            }
           
        }

        void onAddExtraItem()
        {

            

            ObservableCollection<offer> _offer = new ObservableCollection<offer>();


            if (ExtraOfferInput != null || ExtraOfferInput.Length > 0)
            {
                var Currentitem = new offer()
                {
                    Name = ExtraOfferInput
                };

                var firstItem = Offer[0];

                if (firstItem.Name == "Add")
                {
                    ExtraOffer.Clear();
                    ExtraOffer = _offer;
                    ExtraOffer.Add(Currentitem);



                }
                else
                {
                    ExtraOffer.Add(Currentitem);


                }

                ExtraOfferInput = "";


            }
            else
            {

            }

        }






        void OnDeleteOffer(offer _offer)
        {
            Offer.Remove(_offer);
        }







        async void OnAddMedia()
        {

            UserDataStorage storage = new UserDataStorage();

            PickOptions pickOption = new PickOptions()
            {
                FileTypes = FilePickerFileType.Images,
                PickerTitle = "Please select images or video"
            };
           var picked = await storage.PickMedia(pickOption, "nameAndemail");

            var file = await picked.OpenReadAsync();

            

         

            try
            {

          var link =  storage.AddStoreStream(RunTimeUser.Email, picked.FileName, file as FileStream);

                var _image = new image()
                {
                     Url = await link
                };

                Images.Add(_image);

            }
            catch(Exception ex)
            {
                Shell.Current.ShowPopup(new InternetConnectionPop());
            }



        }



        async void OnAddMediaCover()
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



                CoverImage = link;

            }
            catch (Exception ex)
            {
                Shell.Current.ShowPopup(new InternetConnectionPop());
            }

            IsBusy = false;


        }






        public async void AddUserToDB()
        {
            CreateProgress = true;
            PasswordAbcHash abcHash = new PasswordAbcHash();
            UserDataBase userDB = new UserDataBase();
            LocalUserService localDB = new LocalUserService();


            RunTimeUser.Location = Location;


            List<ProductListModel> listModels = new List<ProductListModel>();

            foreach(var item in Offer)
            {

                var productlist = new ProductListModel()
                {
                    Layout = "Card",
                    ListName = item.Name,
                    Products = new List<Products>() { new Products { Name = "Default" } },
                     ListID = ""
            };

                listModels.Add(productlist);
            }






            var businessOffer = new BusinessOffers()
            {
                  ProductListID = Guid.NewGuid().ToString(),
                   OfferList = listModels,
                     
                  
            };


            List<offer> _offers = new List<offer>();
            List<offer> _Extraoffers = new List<offer>();
            List<image> _images = new List<image>();
            foreach(var item in Offer)
            {
                _offers.Add(item);
            }
            foreach(var item in Images)
            {
                _images.Add(item);
            }
            foreach(var item in ExtraOffer)
            {
                _Extraoffers.Add(item);
            }



            var shop = new Shop()
            {
                Email = RunTimeUser.Email,
                Name = ShopName,
                ID = $"{RunTimeUser.Email}and{RunTimeUser.Name}",
                ExtraList = _Extraoffers,
                 StoreImage = _images,
                  Offers = _offers,
                   Location = Location,
                   Cover_Img = CoverImage,
                    BusinessOffers = businessOffer,
                     Points = 0,
                      Visits = 0,
                       
                        
                      

            };
            RunTimeBusiness = shop;
            localDB.AddBusiness(shop);

            try
            {

            var isCompleted = await userDB.AddBusinessAsync(shop);
            

         var IsComplete = await userDB.AddUserAsync(RunTimeUser);
             await userDB.AddEmailAndPassword(RunTimeUser.Email, abcHash.StandardPasswordHash(RunTimeUser.Password), true);




                if (IsComplete && isCompleted)
                {
                    Shell.Current.ShowPopup(new SuccessSigninPop());
                    await Shell.Current.GoToAsync("//TabbedPage");
                }
                else
                {
                    Shell.Current.ShowPopup(new InternetConnectionPop());
                }


            }
            catch(Exception ex)
            {
                Shell.Current.ShowPopup(new UnexpectedErrorPop());
            }



            IsBusy = false;


        }



        public  void AddUserRunTimeUser()
        {
            PasswordAbcHash abcHash = new PasswordAbcHash();
            var user = new User()
            {
                CompanyName = ShopName,
                Email = Email,
                Location = Location,
                IsShop = IsShop,
                Name = Name,
                Number = Number,
                PasswordConfigID = "std",
                Password = abcHash.StandardPasswordHash(Password),
                AutomatedId = $"{Email};;{Name}",
                 EventsHoted = new List<Events>(),
                  EventsGoing = new List<Events>()

            };
            RunTimeUser = user;
        }

    }
}
