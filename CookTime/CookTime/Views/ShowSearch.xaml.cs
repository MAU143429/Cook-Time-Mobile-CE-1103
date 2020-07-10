using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CookTime.REST_API_Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowSearch : ContentPage
    {
        List<Object> UserList;
        public ShowSearch()
        {
            InitializeComponent();
            Pull_Search_Request();
            
        }
        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://192.168.100.7:6969/user";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            
            RegistrationModel model = RegistrationModel.FromJson(json);
            ListaUsers.ItemsSource = model.Newusers;
            



        }
        public void createList()
        {
            InitList(5);
            //ListAdd(head.data, head.next)

        }
        private void ListAdd(JObject data, JObject next)
        {
            UserList.Add(data);//castear a newuser
            if (next == null)
            {
                return;
            }
            else
            {
               // JObject newdata = next.Parse("data");

               // ListAdd(, next.Parse("next"));
            }
        }
<<<<<<< Updated upstream
=======
        private void ListAddRest(CookTime.REST_API_UserModel.Head head)
        {
            if (head.Next!=null)
            {
                UserList.Add(head.Data);
                ListAdd(head.Next);
            }
            else
            {
                ListReturn();
            } 
        }
        public void ListReturn()
        {
            //ListaUsers.ItemsSource = UserList;
            Console.WriteLine(UserList);
        }
>>>>>>> Stashed changes
        public void InitList(int size)
        {
            UserList = new List<Object>();
        }

        private void View_Recipe(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewRecipe());

        }
    }
}