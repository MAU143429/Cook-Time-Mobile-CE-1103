using CookTime.REST_API_CompanyModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Internals;
using CookTime.Views;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class allows users to create a company and add member on it
    /// @author Mauricio C.
    /// </summary>
    public partial class CreateC : ContentPage
    /// <summary>
    /// This constructor execute Create Company partial class 
    /// @author Mauricio C.
    /// </summary>
    {
        public CreateC()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method is used to change the current page to Profile 1 page and create a new company
        /// @author Jose A.
        /// </summary>
        private void Share_Company(object sender, EventArgs e)
        {
            
            DisplayAlert("Company Creation", "Your company was successfully created!", "OK");
            Navigation.PushAsync(new Profile1());

        }
        /**
        private async void Publish_Company() {
                     
            HttpClient client = new HttpClient();
            string url = "http://192.168.0.17:6969/test";
            String jsonNewUser = JsonConvert.SerializeObject(company);
            var datasent = new StringContent(jsonNewUser);
            datasent.Headers.ContentType.MediaType = "application/json";
            await client.PostAsync(url, datasent);
        }*/

        /// <summary>
        /// This method allows the owner to set a location for company
        /// @author Mauricio C.
        /// </summary>
        private void setLocation(object sender, EventArgs e)
        {
            DisplayAlert("Upload Location ", "Your location has been uploaded successfully!", "OK");
           
        }
        /// <summary>
        /// This method allows the owner to set a logo for company
        /// @author Mauricio C.
        /// </summary>
        private void setLogo(object sender, EventArgs e)
        {
            DisplayAlert("Upload Logo", "Your logo has been uploaded successfully!", "OK");

        }
    }
}