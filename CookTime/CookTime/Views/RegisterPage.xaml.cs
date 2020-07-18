
using CookTime.REST_API_Models;
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
    /// <summary>
    /// This class allows users to register in Cook Time application
    /// @author Jose A.
    /// </summary>
    public partial class RegisterPage : ContentPage
    {
        /// <summary>
        /// This constructor execute Register Page partial class and send data of the new user
        /// @author Jose A.
        /// </summary>
        public RegisterPage()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method create an async request that send data of the new user 
        /// @author Jose A.
        /// </summary>
        private async void RegisterRequest()
        {
            Newuser user = new Newuser();
            user.Age = Int32.Parse(ageEntry.Text);
            user.Email = emailEntry.Text;
            user.Password = CreateMD5(passwordEntry.Text);
            user.Name = nameEntry.Text;
            if (ChefBox.IsChecked)
            {
<<<<<<< Updated upstream
                user.Ischef = true;
            }
            else
            {
                user.Ischef = false;
=======
                string url = "http://192.168.100.7:6969/newUser";//AQUI DEBE IR EL URL PARA CHEFS
                String jsonNewUser = JsonConvert.SerializeObject(user);
                Console.WriteLine("JSON NEW USER:" + jsonNewUser);
                var datasent = new StringContent(jsonNewUser);
                Console.WriteLine("DATASENT" + datasent);
                datasent.Headers.ContentType.MediaType = "application/json";
                var result = await cliente.PostAsync(url, datasent);
                var json = result.Content.ReadAsStringAsync().Result;
                await DisplayAlert("Result", json, "ok");
            }
            else
            {
                string url = "http://192.168.100.7:6969/newUser";//AQUI DEBE IR EL URL PARA USUARIOS SINGULARES
                String jsonNewUser = JsonConvert.SerializeObject(user);
                Console.WriteLine("JSON NEW USER:" + jsonNewUser);
                var datasent = new StringContent(jsonNewUser);
                Console.WriteLine("DATASENT" + datasent);
                datasent.Headers.ContentType.MediaType = "application/json";
                var result = await cliente.PostAsync(url, datasent);
                var json = result.Content.ReadAsStringAsync().Result;
                await DisplayAlert("Result", json, "ok");
>>>>>>> Stashed changes
            }
            HttpClient cliente = new HttpClient();
            string url = "http://192.168.100.7:6969/test";
            String jsonNewUser = JsonConvert.SerializeObject(user);
            Console.WriteLine("JSON NEW USER:" + jsonNewUser);
            var datasent = new StringContent(jsonNewUser);
            Console.WriteLine("DATASENT" + datasent);
            datasent.Headers.ContentType.MediaType = "application/json";
            var result = await cliente.PostAsync(url, datasent);
            var json = result.Content.ReadAsStringAsync().Result;
            await DisplayAlert("Result", json, "ok");
        }
        /// <summary>
        /// This method verify that not exist blank spaces and send the final request
        /// @author Mauricio C.
        /// </summary>
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
        /// <summary>
        /// This method encrypt the password with HASH MD5 algorithm
        /// @author Jose A.
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