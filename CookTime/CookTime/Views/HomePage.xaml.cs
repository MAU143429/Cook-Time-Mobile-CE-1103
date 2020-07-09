using CookTime.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {

         
        public HomePage()
        {
            
            InitializeComponent();
            UserSelf CurrentUser;

        }
        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());

        }
        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());

        }
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
 
        private void Next(object sender, EventArgs e)
        {
            var urlimg = "https://www.recetasconpollo.org/wp-content/uploads/2019/12/filete-pechuga-pollo-plancha--512x341.jpg";
            mainimage.Source = urlimg;  

        }
    }
}