using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    /// <summary>
    /// This class allows  users to change their password
    /// @author Mauricio C.
    /// </summary>
    public partial class ChangePassword : ContentPage
    {
        /// <summary>
        /// This constructor execute ChangePassword partial class
        /// @author Mauricio C.
        /// </summary>
        public ChangePassword()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method send a new password to the server
        /// @author Mauricio C.
        /// </summary>
        private async void ChangePass(object sender, EventArgs e)
        {

            if (newpassword.Text == newpassword1.Text && newpassword != null && newpassword1 != null){

                HttpClient client = new HttpClient();
                string url = "http://192.168.100.7:6969/setUser/" + LoginPage.CURRENTUSER.Email + "/password";
                String newpass = CreateMD5(newpassword.Text);
                var datasent = new StringContent(newpass);
                datasent.Headers.ContentType.MediaType = "application/json";
                await client.PostAsync(url, datasent);

                await Navigation.PushAsync(new LoginPage());
            }
            else {
                await DisplayAlert("ERROR", "YOU MUST FILL ALL THE BLANKS TO CONTINUE", "ACCEPT");
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