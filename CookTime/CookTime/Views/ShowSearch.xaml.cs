using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CookTime.REST_API_UserModel;
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
            string url = "http://192.168.0.17:6969/user";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            //Console.WriteLine(json);
            UserListModel model = UserListModel.FromJson(json);
            var list = JsonConvert.DeserializeObject<List<UserListModel>>(json);
            Console.WriteLine(list);
            ListaUsers.ItemsSource = list;





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