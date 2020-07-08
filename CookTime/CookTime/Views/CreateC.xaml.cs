using CookTime.REST_API_Company;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateC : ContentPage
    {
        public CreateC()
        {
            InitializeComponent();
        }
        private void Share_Company(object sender, EventArgs e)
        {
            Publish_Company();
            DisplayAlert("Company Creation", "Your company was successfully created!", "OK");
            Navigation.PushAsync(new Profile1());

        }
        private async void Publish_Company() {
         

            Company company = new Company();
            company.Name = companyname.Text;
            company.Email = companyemail.Text;
            company.Number = Int32.Parse(companynumber.Text);
            company.Schedule = companyschedule.Text;
            company.Logo = "";
            company.Location = "";
            company.Posts = 0;
            company.Followers = 0;
            company.Following = 0;
            company.Members = 1;
            
            HttpClient client = new HttpClient();
            string url = "http://192.168.0.17:6969/test";
            String jsonNewUser = JsonConvert.SerializeObject(company);
            var datasent = new StringContent(jsonNewUser);
            datasent.Headers.ContentType.MediaType = "application/json";
            await client.PostAsync(url, datasent);
        }
    }
}