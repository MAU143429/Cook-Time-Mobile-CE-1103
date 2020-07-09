using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CookTime.REST_API_Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowSearch : ContentPage
    {
        public ShowSearch()
        {
            InitializeComponent();
            Pull_Search_Request();
        }
        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://192.168.0.17:6969/user";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RegistrationModel model = RegistrationModel.FromJson(json);
            Console.WriteLine(json);
            ListaUsers.ItemsSource = model.Newusers;
            
        }
        private void View_Recipe(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewRecipe());

        }
    }
}