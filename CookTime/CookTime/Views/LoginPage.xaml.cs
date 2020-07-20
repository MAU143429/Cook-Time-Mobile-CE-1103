using CookTime.REST_API_LoginModel;
using CookTime.REST_API_UserModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
    /// This class allows users to login in Cook Time application, if already have an account created.
    /// @author Jose A.
    /// </summary>
    public partial class LoginPage : ContentPage
    {
        public static CookTime.REST_API_UserModel.User CURRENTUSER;
        /// <summary>
        /// This constructor execute Login Page partial class
        /// @author Jose A.
        /// </summary>
        public LoginPage()
        {
            InitializeComponent();

        }


        /// <summary>
        /// This method is used to change the current page to Register page
        /// @author Mauricio C.
        /// </summary>
        private void Go_Register(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Views.RegisterPage());
        }

        /// <summary>
        /// This method is used to change the current page to Home page and send a json file to verify the user credentials
        /// @author Mauricio C.
        /// </summary>
        private async void Button_Clicked(object sender, EventArgs e)
        {

            var userValidate = userEntry.Text;
            if (!string.IsNullOrEmpty(userValidate))
            {

                string email = userEntry.Text;
                string password = "/" + CreateMD5(pasEntry.Text);
                HttpClient cliente = new HttpClient();
                string url = "http://192.168.100.7:6969/login/" + email + password;
                //String jsonNewUser = JsonConvert.SerializeObject(user);
                // Console.WriteLine("JSON NEW USER:" + jsonNewUser);
                //var datasent = new StringContent(jsonNewUser);
                //Console.WriteLine("DATASENT" + datasent);
                //datasent.Headers.ContentType.MediaType = "application/json";
                var result = await cliente.GetAsync(url);
                var json = result.Content.ReadAsStringAsync().Result;
                CookTime.REST_API_UserModel.User InputUser = new REST_API_UserModel.User();
                InputUser = CookTime.REST_API_UserModel.User.FromJson(json);
                if ( InputUser.Name == null)
                {
                    await DisplayAlert("Cook Time", "The data you filled with does not match with any CookTime user. Please verify your info and try again!", "OK");
                }
                else
                {
                    CURRENTUSER = CookTime.REST_API_UserModel.User.FromJson(json);
                    await DisplayAlert("Cook Time", "Welcome back " + CURRENTUSER.Name, "OK");
                    await Navigation.PushAsync(new HomePage());
                }
            }

        }
        /// <summary>
        /// This method is used to change the current page to Info page
        /// @author Mauricio C.
        /// </summary>
        private void Info_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfileView());
        }
        /// <summary>
        /// This method encrypt the password with HASH MD5 algorithm
        /// @author Mauricio C.
        /// </summary>
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