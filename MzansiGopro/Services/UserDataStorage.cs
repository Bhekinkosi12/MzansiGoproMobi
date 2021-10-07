using Firebase.Storage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MzansiGopro.Services
{
   public class UserDataStorage
    {
        Stream file;
        string usernameID;

       

        public UserDataStorage()
        {
          

        }

     


      

        public FirebaseStorageTask AddStoreStream(string userConfigID,string name, Stream stream)
        {
            return new FirebaseStorage("mzansi-go-pro.appspot.com")
                .Child("Compony")
                .Child(userConfigID)
                .Child(name)
                .PutAsync(stream);


        }
        public FirebaseStorageTask AddStoreStream(string userConfigID, string name, FileStream stream)
        {
            return new FirebaseStorage("mzansi-go-pro.appspot.com")
                .Child("Compony")
                .Child(userConfigID)
                .Child(name)
                .PutAsync(stream);


        }

        public Task<FirebaseStorageTask> AddStoreStreamAsync(string userConfigID, string name, Stream stream)
        {
            return Task.FromResult(new FirebaseStorage("mzansi-go-pro.appspot.com")
                .Child("Compony")
                  .Child(userConfigID)
                .Child(name)
                .PutAsync(stream));
            


        }

        public async Task<FileResult> PickMedia(PickOptions option,string userConfigId)
        {
          

            try
            {
                var result = await FilePicker.PickAsync(option);
                if(result != null)
                {
                    
                    if(result.FileName.EndsWith("jpg", StringComparison.OrdinalIgnoreCase) || result.FileName.EndsWith("png", StringComparison.OrdinalIgnoreCase))
                    {
                        var stream = await result.OpenReadAsync();
                        var image = ImageSource.FromStream(() => stream);

                        




                      //await AddStoreStream(userConfigId, result.FileName, stream as FileStream);


                    }
                    else
                    {
                        var stream = await result.OpenReadAsync();
                        //await AddStoreStream(userConfigId, result.FileName, stream);
                        //await AddStoreStream(userConfigId, result.FileName, stream as FileStream);
                    }

                }
                return result;
                
            }
            catch(Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
            }

            return null;



        }


    }
}
