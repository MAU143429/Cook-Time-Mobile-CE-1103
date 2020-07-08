using CookTime.REST_API_Models;
using CookTime.User;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private async void RegisterRequest()
        {
            Newuser user = new Newuser();
            user.Age = Int32.Parse(ageEntry.Text);
            user.Email = emailEntry.Text;
            user.Password = CreateMD5(passwordEntry.Text);
            if (ChefBox.IsChecked)
            {
                user.Name = nameEntry.Text + "⍟";
            }
            else
            {
                user.Name = nameEntry.Text;
            } 

            HttpClient cliente = new HttpClient();
            string url = "http://192.168.0.17:6969/newUser";
            String jsonNewUser = JsonConvert.SerializeObject(user);
            Console.WriteLine(jsonNewUser);
            var datasent = new StringContent(jsonNewUser);
            Console.WriteLine(datasent);
            datasent.Headers.ContentType.MediaType = "application/json";
            var result = await cliente.PostAsync(url, datasent);
            var json = result.Content.ReadAsStringAsync().Result;
            await DisplayAlert("Result", json, "ok");
            UserSelf user1 = JsonConvert.DeserializeObject<UserSelf>(json);
            
           
            

        }
        private void Button_Clicked(object sender, EventArgs e)
        {

            var nameValidate = nameEntry.Text;
            var ageValidate = ageEntry.Text;
            var emailValidate = emailEntry.Text;
            var passValidate = passwordEntry.Text;
            if (!string.IsNullOrEmpty(nameValidate) && !string.IsNullOrEmpty(ageValidate) && !string.IsNullOrEmpty(emailValidate) && !string.IsNullOrEmpty(passValidate))
            {
                RegisterRequest();
                DisplayAlert("REGISTRATION COMPLETE", "ENJOY COOKTIME!", "ACCEPT");
                Navigation.PushAsync(new HomePage());
            }
            else
            {
                DisplayAlert("ERROR", "YOU MUST FILL ALL THE BLANKS TO CONTINUE", "ACCEPT");
            }

        }
        public static string CreateMD5(string input)
        {
            // Use input string to calculate MD5 hash
            using (System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create())
            {
                byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }
    }
}