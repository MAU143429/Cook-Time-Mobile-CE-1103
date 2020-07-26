using CookTime.REST_API_CompanyModel;
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
    public partial class AddMember : ContentPage
    {
        public AddMember()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method is used to change the current page to AddMember page
        /// @author Mauricio C.
        /// </summary>
        private async void Add_Member(object sender, EventArgs e)
        {
            var membervalidate = newmember.Text;
            if (!string.IsNullOrEmpty(membervalidate))
            {
                HttpClient client = new HttpClient();
                string url = "http://" + LoginPage.ip + ":6969/user/" + newmember.Text + "/addMember/" + CompanyProfile.mycompany.Email;
                Console.WriteLine(url);
                var result  = await client.GetAsync(url);
                var json = result.Content.ReadAsStringAsync().Result;

                if (json == "Please check your info") {

                    await DisplayAlert("ERROR", "The user email doesn't exist", "ACCEPT");
                }
                else
                {
                    await DisplayAlert("Cook Time", "Member added successfully", "ACCEPT");
                    await Navigation.PushAsync(new CompanyProfile());
                }

                

            }
            else
            {
                await DisplayAlert("ERROR", "YOU MUST FILL ALL THE BLANKS TO CONTINUE", "ACCEPT");
                
            }

            


        }
    }
}