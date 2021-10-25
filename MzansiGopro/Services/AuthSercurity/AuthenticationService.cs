using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.CommunityToolkit.Extensions;
using Firebase.Auth;
using Xamarin.Essentials;
using Xamarin.Forms;
using MzansiGopro.ViewModels;
using MzansiGopro.Views.PopupV.ErrorHandlingV;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace MzansiGopro.Services.AuthSercurity
{
   public class AuthenticationService : BaseViewModel
    {


        public AuthenticationService()
        {
         
        }

       

        public async Task<string> Signup(string email, string password)
        {

            try
            {

                //  var authProvider = new FirebaseAuthProvider();

                 

           var authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKEY));

                var auth = await authProvider.CreateUserWithEmailAndPasswordAsync(email, password);

                string getToken = auth.FirebaseToken;



                var serial = JsonConvert.SerializeObject(auth);

                Preferences.Set("RefreshToken", serial);






                return await Task.FromResult(getToken);
            }
            catch
            {

               
                return await Task.FromResult("");
            }


        }


        public async Task<bool> Login(string email, string password)
        {
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKEY));
            UserDataBase userDataBase = new UserDataBase();

            try
            {


                var auth = await authProvider.SignInWithEmailAndPasswordAsync(email, password);

                var content = await auth.GetFreshAuthAsync();

                var serial = JsonConvert.SerializeObject(content);

                Preferences.Set("RefreshToken", serial);

                




                try
                {

                    var _user = await userDataBase.GetUserByEmailAsync(auth.User.Email);



                    if (_user != null)
                    {

                        RunTimeUser = _user;




                        try
                        {
                            var shop = await userDataBase.GetShopById(RunTimeUser.AutomatedId);

                            RunTimeBusiness = shop;

                        }
                        catch
                        {

                        }





                    }
                    else
                    {
                        Shell.Current.ShowPopup(new InternetConnectionPop());
                    }






                }
                catch
                {

                }







                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }

        }




        public async Task<bool> CheckEmailExist(string email)
        {
            bool IsAvailable = false;
                var authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKEY));
            try
            {

              var a =  await authProvider.GetLinkedAccountsAsync(email);


                if (a.IsForExistingProvider)
                {
                    IsAvailable = true;
                }
                else
                {

                }


            }
            catch
            {

            }

            return await Task.FromResult(IsAvailable);
        }




        public async Task<string> AutoLogin()
        {
            UserDataBase userDataBase = new UserDataBase();
            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKEY));
            try
            {

                var savedAuth = JsonConvert.DeserializeObject<Firebase.Auth.FirebaseAuth>(Preferences.Get("RefreshToken", ""));

              // var user = await authProvider.SignInWithOAuthAsync(FirebaseAuthType.EmailAndPassword, savedAuth.FirebaseToken);

               var user = await authProvider.RefreshAuthAsync(savedAuth);
            
                
                var serial = JsonConvert.SerializeObject(user);


                Preferences.Set("RefreshToken", serial);

                try
                {

             var _user =  await userDataBase.GetUserByEmailAsync(user.User.Email);

                    

                    if(_user != null)
                    {

                         RunTimeUser = _user;




                        try
                        {
                            var shop = await userDataBase.GetShopById(RunTimeUser.AutomatedId);

                            RunTimeBusiness = shop;

                        }
                        catch
                        {

                        }





                    }
                    else
                    {
                       Shell.Current.ShowPopup(new InternetConnectionPop());
                      
                    }






                }
                catch
                {

                }





                return await Task.FromResult(user.FirebaseToken);

            }
            catch(Exception ex)
            {
                Shell.Current.ShowPopup(new InternetConnectionPop());

               
                return "";
            }


        }




        public async Task<bool> ForgotPassword(string email)
        {

            var authProvider = new FirebaseAuthProvider(new FirebaseConfig(APIKEY));

            try
            {

               await authProvider.SendPasswordResetEmailAsync(email);

                return await Task.FromResult(true);

            }
            catch
            {
                //Shell.Current.ShowPopup(new InternetConnectionPop());

                return false;
            }


        }


    
    }
}
