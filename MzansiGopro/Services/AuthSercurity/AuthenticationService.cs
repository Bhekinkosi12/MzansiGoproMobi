using System;
using System.Collections.Generic;
using System.Text;

using Firebase.Auth;

using MzansiGopro.ViewModels;
using System.Threading.Tasks;

namespace MzansiGopro.Services.AuthSercurity
{
   public class AuthenticationService : BaseViewModel
    {


        public AuthenticationService()
        {
         
            
        }

       

        public async Task<bool> Signup(string email, string password)
        {

            try
            {

                //  var authProvider = new FirebaseAuthProvider();

                 

           
                
                
                 



                return await Task.FromResult(true);
            }
            catch
            {
                return await Task.FromResult(false);
            }


        }

    
    }
}
