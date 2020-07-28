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
using System.Data.Common;

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
            Publish_Company();
            DisplayAlert("Company Creation", "Your company was successfully created!", "OK");
            Navigation.PushAsync(new Profile1());

        }
        /// <summary>
        /// This method creates a new company object, serializes it as a json file and sends it to the server for storing in the company tree.
        /// @author Jose A.
        /// </summary>
        private async void Publish_Company() {

            var email = LoginPage.CURRENTUSER.Email;
            Company company = new Company();
            company.Email = companyemail.Text;
            company.Name = companyname.Text;
            company.Schedule = companyschedule.Text;
            company.Number = Int32.Parse(companynumber.Text);
            company.Logo = companyimg.Text;
            company.Location = "location";
            company.Followers = null;
            company.Posts = 0;
            company.Following = null;
            company.Members = null;
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/newCompany/"+ LoginPage.CURRENTUSER.Email;
            String jsonNewUser = JsonConvert.SerializeObject(company);
            var datasent = new StringContent(jsonNewUser);
            datasent.Headers.ContentType.MediaType = "application/json";
            var result = await client.PostAsync(url, datasent);
            Console.WriteLine(url);
            var n = result.Content.ReadAsStringAsync().Result;
            await DisplayAlert("Result", n, "ok");

            changehascompany();

            
        }
        /// <summary>
        /// This method changes the user's boolean of "hasCompany" from false to true when a new company has been created and binded to him/her
        /// @author Jose A.
        /// </summary>
        private async void changehascompany()
        {
            HttpClient client = new HttpClient();
            String url2 = "http://" + LoginPage.ip + ":6969/setUser/" + LoginPage.CURRENTUSER.Email + "/hasCompany";
            var newboolean = "true";
            var sendableboolean = new StringContent(newboolean);
            sendableboolean.Headers.ContentType.MediaType = "application/json";
            await client.PostAsync(url2, sendableboolean);

        }



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
