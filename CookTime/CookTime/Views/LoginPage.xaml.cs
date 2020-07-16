using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

        }
        private void Go_Register(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.RegisterPage());
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

            var userValidate = userEntry.Text;
            if (!string.IsNullOrEmpty(userValidate))
            {
                DisplayAlert("COOKTIME", "BIENVENIDO A COOK TIME ", "ACEPTAR");
                Navigation.PushAsync(new HomePage());
            }
            
        }
        private void Info_Clicked(object sender, EventArgs e)
        {
                Navigation.PushAsync(new ChangePhoto());
        }
    }
}