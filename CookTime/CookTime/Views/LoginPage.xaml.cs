using CookTime.REST_API_LoginModel;
using Newtonsoft.Json;
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
    /// This class allows users to login in Cook Time application, if already have an account created
    /// @author Jose A.
    /// </summary>
    public partial class LoginPage : ContentPage
    {
        /// <summary>
        /// This constructor execute LoginPage partial class
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
                LoginUser user = new LoginUser();
                user.Email = userEntry.Text;
                user.Password = CreateMD5(pasEntry.Text);
                HttpClient cliente = new HttpClient();
<<<<<<< Updated upstream
<<<<<<< Updated upstream
                string url = "http://192.168.0.17:6969/test";//LOGIN VERIFICATION
=======
                string url = "http://192.168.100.7:6969/test";//LOGIN VERIFICATION
>>>>>>> Stashed changes
                String jsonNewUser = JsonConvert.SerializeObject(user);
                Console.WriteLine("JSON NEW USER:" + jsonNewUser);
                var datasent = new StringContent(jsonNewUser);
                Console.WriteLine("DATASENT" + datasent);
                datasent.Headers.ContentType.MediaType = "application/json";
                var result = await cliente.PostAsync(url, datasent);
=======
                string url = "http://192.168.100.7:6969/login/" + email + password;
                Console.WriteLine(url);
                //String jsonNewUser = JsonConvert.SerializeObject(user);
               // Console.WriteLine("JSON NEW USER:" + jsonNewUser);
                //var datasent = new StringContent(jsonNewUser);
                //Console.WriteLine("DATASENT" + datasent);
                //datasent.Headers.ContentType.MediaType = "application/json";
                var result = await cliente.GetAsync(url);
                var json = result.Content.ReadAsStringAsync().Result;

                var nullUser = new REST_API_UserModel.User();
                nullUser.Password = null;
                nullUser.Followers = null;
                nullUser.Following = null;
                nullUser.Name = null;
                nullUser.Recipes = null;
                nullUser.Hascompany = false;
                nullUser.Email = null;
                nullUser.Age = 0;

                string NullCompareable = JsonConvert.SerializeObject(nullUser);
                


                if (json == NullCompareable)
                {
                   await  DisplayAlert("Cook Time","The data you filled with does not match with any CookTime user. Please verify your info and try again!","OK") ;
                }
                else
                {
                    CURRENTUSER = CookTime.REST_API_UserModel.User.FromJson(json);
                    Console.WriteLine(CURRENTUSER.Name);
                    await Navigation.PushAsync(new HomePage());
                }
                 
                
>>>>>>> Stashed changes
                
                var json = result.Content.ReadAsStringAsync().Result;
                /*
                 * if json != false:
                 * alerta!
                 * else{
                 * user = new USer() con los datos que me manda
                 * }
                 * 
                 */
                await DisplayAlert("Result", json, "ok");
                await Navigation.PushAsync(new HomePage());
            }
            
        }

        /// <summary>
        /// This method is used to change the current page to Info page
        /// @author Mauricio C.
        /// </summary>

        private void Info_Clicked(object sender, EventArgs e)
        {
                Navigation.PushAsync(new CompanyProfileView());
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