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
        private async void Button_Clicked(object sender, EventArgs e)
        {

            var userValidate = userEntry.Text;
            if (!string.IsNullOrEmpty(userValidate))
            {
                LoginUser user = new LoginUser();
                user.Email = userEntry.Text;
                user.Password = CreateMD5(pasEntry.Text);
                HttpClient cliente = new HttpClient();
                string url = "http://192.168.0.17:6969/test";//LOGIN VERIFICATION
                String jsonNewUser = JsonConvert.SerializeObject(user);
                Console.WriteLine("JSON NEW USER:" + jsonNewUser);
                var datasent = new StringContent(jsonNewUser);
                Console.WriteLine("DATASENT" + datasent);
                datasent.Headers.ContentType.MediaType = "application/json";
                var result = await cliente.PostAsync(url, datasent);
                
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
        private void Info_Clicked(object sender, EventArgs e)
        {
                Navigation.PushAsync(new ChangePhoto());
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