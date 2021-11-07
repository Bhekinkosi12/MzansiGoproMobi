using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using MzansiGopro.Models;
using MzansiGopro.Services.AuthSercurity;
using MzansiGopro.Models.microModel;
using MzansiGopro.Views.PopupV.AlertsV;
using SQLite;
using Xamarin.Forms;
using Xamarin.CommunityToolkit.Extensions;


namespace MzansiGopro.Services
{
   public class UserDataBase
    {
        FirebaseClient client;
        string Cversion = "1";
        static string Token { get; set; } = string.Empty;



        public UserDataBase()
        {
            Init();
        }




        async void Init()
        {
            AuthenticationService authenticationService = new AuthenticationService();
            AuthMemory authMemory = new AuthMemory();
            var auth = authMemory.GetToken();

           

            if(Token == string.Empty)
            {

              
                    try
                    {
                      var token = await authenticationService.AutoLogin();

                        if(token != "")
                        {
                            Token = token;


                        client = new FirebaseClient("https://mzansi-go-pro-default-rtdb.firebaseio.com/", new FirebaseOptions
                        {
                            AuthTokenAsyncFactory = () => Task.FromResult(Token),
                            AsAccessToken = true,
                            SyncPeriod = TimeSpan.FromSeconds(5),

                        });

                    }


                    }
                    catch
                    {
                    
                   
                    }

                
            }
            else
            {

            client = new FirebaseClient("https://mzansi-go-pro-default-rtdb.firebaseio.com/", new FirebaseOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(Token),
                 AsAccessToken = true,
                  SyncPeriod = TimeSpan.FromSeconds(5),
                    
            });
            }








            //  client = new FirebaseClient("https://mzansi-go-pro-default-rtdb.firebaseio.com/");
        }


        public async Task GetNetVersion()
        {

            try
            {
                var item = (await client.Child("Version").OnceAsync<string>()).FirstOrDefault();
               var a = item.Object;



                if(Cversion == a)
                {


                }
                else
                {
                    Shell.Current.ShowPopup(new UpdateAppPop());
                   
                }

            }
            catch
            {

            }

        }





        public async Task<List<User>> GetAllPersons()
        {

            return (await client
              .Child("Users")
              .OnceAsync<User>()).Select(item => new User
              {
                  Name = item.Object.Name,
                  
              }).ToList();
        }

        public async Task AddPerson(string user)
        {

            await client
              .Child("Users")
              .PostAsync(new User() {  Name = "Bheki", Email = "ndumiso@gmail.com" });
        }


        public async Task<bool> AddUserAsync(User user)
        {
            try
            {


               
                    await client
                   .Child("Users")
                   .Child("NotBusiness")
                   .PostAsync(user);
                
                    return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }

        }

        public async Task<User> GetUserById(string id)
        {
            try
            {
                var user = (await client
                    .Child("Users")
                    .Child("NotBusiness")
                    .OnceAsync<User>()).Where(x => x.Object.AutomatedId == id).FirstOrDefault();

               return await Task.FromResult(user.Object);
            }
            catch
            {
                return null;
            }
        }



        public async Task<User> GetUserByEmailAsync(string email)
        {
            try
            {
                var user = (await client
                    .Child("Users")
                    .Child("NotBusiness")
                    .OnceAsync<User>()).Where(x => x.Object.Email == email).FirstOrDefault();

                return await Task.FromResult(user.Object);
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
                return null;
            }
        }


        public async Task<Shop> GetShopById(string id)
        {
            try
            {
                var shop = (await client
                    .Child("Users")
                    .Child("IsBusiness")
                    .OnceAsync<Shop>()).Where(x => x.Object.ID == id).FirstOrDefault();


                return await Task.FromResult(shop.Object);

            }
            catch
            {
                return null;
            }
        }



        public async Task<Shop> GetShopByEmailAsync(string email)
        {
            try
            {
                var shop = (await client
                    .Child("Users")
                    .Child("IsBusiness")
                    .OnceAsync<Shop>()).Where(x => x.Object.Email == email).FirstOrDefault();


                return await Task.FromResult(shop.Object);

            }
            catch
            {
                return null;
            }
        }




        public async Task<bool> AddEventAsync(Events _event)
        {

            try
            {
                await client
                    .Child("Events")
                    .PostAsync(_event);

               return await Task.FromResult(true);

            }
            catch
            {
               return await Task.FromResult(false);
            }


        }




        public async Task DeleteEvent(Events _events)
        {
            try
            {
                var item = (await client
                     .Child("Events")
                     .OnceAsync<Events>()).Where(x => x.Object.Idetifier == _events.Idetifier).FirstOrDefault();

                await client.Child("Events").Child(item.Key).DeleteAsync();
                    
            }
            catch
            {

            }
        }







        public async Task<List<Events>> GetAllEvents()
        {
                List<Events> _events = new List<Events>();
            try
            {

               var list = (await client
                    .Child("Events")
                    .OnceAsync<Events>()).ToList();

                foreach(var i in list)
                {
                    var dateCompare = DateTime.Compare(i.Object.EventDateTime, DateTime.Today);
                    if(dateCompare >= 0)
                    {
                         _events.Add(i.Object);

                    }
                    else
                    {
                        
                    }
                }



                return await Task.FromResult(_events);
            }
            catch
            {

                return await Task.FromResult(_events);
            }
        }


        public async Task AddEmailAndPassword(string email,string password,bool isShop)
        {
            var auth = new Authentication()
            {
                 Email = email,
                 Password = password,
                 IsShop = isShop
            };

            await client
                .Child("Authentications")
                .PostAsync(auth);
        }


        public async Task UpdateEvent(Events events,Events newEvent)
        {
            try
            {

            var item = (await client.Child("Events").OnceAsync<Events>()).Where(x => x.Object == events).FirstOrDefault();

            await client.Child("Events").Child(item.Key).PutAsync(newEvent);
            }
            catch
            {
                
            }


        }

     
        public async Task<Authentication> CheckEmailAndPass(string email, string password)
        {

           var user = (await client
                 .Child("Authentications")
                 .OnceAsync<Authentication>()).Where(x => x.Object.Email == email && x.Object.Password == password).FirstOrDefault();

            return user.Object;
           
        }
        
        public async Task<bool> CheckEmailAndPasswordAsync(string email, string password)
        {
            Authentication userAuth = new Authentication();

            try
            {

           var  user = (await client
                 .Child("Authentications")
                 .OnceAsync<Authentication>()).Where(x => x.Object.Email == email && x.Object.Password == password).FirstOrDefault();

                if(user != null)
                {
                    userAuth = user.Object;
                }
                else
                {
                    userAuth = null;
                }
              
            }
            catch(Exception ex)
            {
                
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");

            }


            if(userAuth.Password != null)
            {
                return await Task.FromResult(true);
            }
            else
            {
                return await Task.FromResult(false);
            }
           
           
        }




        public async Task<User> GetUserAsync(string autoID,bool isShop)
        {
            string shop;
            if (isShop)
            {
                shop = "IsBusiness";
            }
            else
            {
                shop = "NotBusiness";
            }

            var user = (await client
                            .Child("Users")
                            .Child(shop)
                            .OnceAsync<User>()).Where(x => x.Object.AutomatedId == autoID).FirstOrDefault();

            return user.Object;
                    
        }


        public async Task<User> GetUserByEmail(string email)
        {
            User user = new User();
            try
            {

                var users = (await client
                    .Child("Users")
                    .Child("IsBusiness")
                    .OnceAsync<User>()).FirstOrDefault(x => x.Object.Email == email);
                
                var _users = (await client
                    .Child("Users")
                    .Child("NotBusiness")
                    .OnceAsync<User>()).FirstOrDefault(x => x.Object.Email == email);

                if(users.Object == null)
                {
                    await Shell.Current.DisplayAlert("Error", "doesnt exit in business", "OK");
                }
                else
                {
                    user = users.Object;
                }
                if(_users == null)
                {
                    await Shell.Current.DisplayAlert("Error", "doesnt exit in not business", "OK");
                }
                else
                {
                    user = _users.Object;
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }


            return user;


        }


        public async Task<List<Authentication>> GetAllAuths()
        {
            List<Authentication> auths = new List<Authentication>();

            try
            {
                var authList = (await client
                       .Child("Authentications")
                       .OnceAsync<Authentication>()).ToList();

                foreach(var _auth in authList)
                {
                    auths.Add(_auth.Object);
                }
            }
            catch
            {
                auths = null;
            }

            return await Task.FromResult(auths);


        }


       public async Task UpdateUserAsync(User _user)
        {
            
            try
            {
              
                    var SelectedUser = (await client
                    .Child("Users")
                    .Child("NotBusiness")
                    .OnceAsync<User>()).FirstOrDefault(x => x.Object.AutomatedId == _user.AutomatedId);



                await client
                            .Child("Users")
                            .Child("NotBusiness")
                            .Child(SelectedUser.Key)
                            .PutAsync(_user);

                


            }
            catch
            {
                await Shell.Current.DisplayAlert("Error", "Please check your internet connection and retry.", "OK");
            }
        }

       public async Task UpDateBusinessAsync(Shop newShop)
        {

            try
            {

            var business = (await client
               .Child("Users")
                    .Child("IsBusiness")
                    .OnceAsync<Shop>()).FirstOrDefault(x => x.Object.BusinessOfferID == newShop.BusinessOfferID);

            await client
                .Child("Users")
                    .Child("IsBusiness")
                       .Child(business.Key)
                         .PutAsync(newShop);
            }
            catch(Exception ex)
            {
                
                await Shell.Current.DisplayAlert("Error","Please check your internet connection and retry.", "OK");
            }
        }



        public async Task<bool> AddBusinessAsync(Shop shop)
        {
            string shopId = $"{shop.Email};;{shop.Name}";
            

            try
            {


             
                    await client
                        .Child("Users")
                        .Child("IsBusiness")
                        
                        .PostAsync(shop);


              
                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }

        }

        public async Task<List<Shop>> GetAllBusiness()
        {
            try
            {

                List<Shop> shopList = new List<Shop>();
                var items = (await client
                     .Child("Users")
                     .Child("IsBusiness")
                     .OnceAsync<Shop>()).ToList();

                foreach (var shop in items)
                {
                    shopList.Add(shop.Object);
                }


                return shopList;
            }
            catch
            {
                return null;
            }
            

        }






    }
}
