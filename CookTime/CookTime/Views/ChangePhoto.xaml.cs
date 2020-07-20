using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class allows users to change their profile photo 
    /// @author Mauricio C.
    /// </summary>
    public partial class ChangePhoto : ContentPage
    {
        /// <summary>
        /// This constructor execute ChangePhoto partial class
        /// @author Mauricio C.
        /// </summary>
        public ChangePhoto()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method send a new image url to the server. This photo will be updated when the user go back to their profile
        /// @author Mauricio C.
        /// </summary>
        private async void Changephoto(object sender, EventArgs e)
        {

            
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/setUser/" + LoginPage.CURRENTUSER.Email + "/image";
            String newimg = newphoto.Text;
            var datasent = new StringContent(newimg);
            datasent.Headers.ContentType.MediaType = "application/json";
            await client.PostAsync(url, datasent);
            LoginPage.updateUser();
            await Navigation.PushAsync(new Profile1());
            
        }


    }
}