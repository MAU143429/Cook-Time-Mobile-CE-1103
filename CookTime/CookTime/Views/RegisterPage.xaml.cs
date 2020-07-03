using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
        }
        private void Button_Clicked(object sender, EventArgs e)
        {

            var nameValidate = nameEntry.Text;
            var ageValidate = ageEntry.Text;
            var emailValidate = emailEntry.Text;
            var passValidate = passwordEntry.Text;
            if (!string.IsNullOrEmpty(nameValidate)&& !string.IsNullOrEmpty(ageValidate) && !string.IsNullOrEmpty(emailValidate) && !string.IsNullOrEmpty(passValidate))
            {
                DisplayAlert("REGISTRATION COMPLETE", "ENJOY COOKTIME!", "ACCEPT");
                Navigation.PushAsync(new HomePage());
            }
            else
            {
                DisplayAlert("ERROR", "YOU MUST FILL ALL THE BLANKS TO CONTINUE", "ACCEPT");
            }

        }
    }
}