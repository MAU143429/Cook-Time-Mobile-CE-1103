using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CookTime.REST_API_UserListModel;
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
        //List<Object> UserList;
        //List<Object> ShownList;
        ArrayList UserList;
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
            UserListModel newmodel = UserListModel.FromJson(json);
            StartList(newmodel);
        }
        public void StartList(UserListModel model)
        {
            InitList();
            UserList.Add(model.Head.Data);
            ListAdd(model.Head.Next);
        }
        private void ListAdd(CookTime.REST_API_UserListModel.Next next)
        {
            if (next.NextNext != null)
            {
                UserList.Add(next.Data);
                ListAddRest(next.NextNext);
            }
            else
            {
                UserList.Add(next.Data);
                ListReturn();
            }
        }
        private void ListAddRest(CookTime.REST_API_UserListModel.Head head)
        {
            if (head.Next != null)
            {
                UserList.Add(head.Data);
                ListAdd(head.Next);
            }
            else
            {
                UserList.Add(head.Data);
                ListReturn();
            }
        }
        public void ListReturn()
        {
            ListaUsers.ItemsSource = UserList;
            Console.WriteLine(UserList[0]);
        }
        public void InitList()
        {
            UserList = new ArrayList();
        }
        private void View_Recipe(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewRecipe());
        }
    
    }
}