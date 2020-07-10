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
        List<Object> ShownList;
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
            UserModel newmodel = UserModel.FromJson(json);
            StartList(newmodel);
        }
        public void StartList(UserModel model)
        {
            InitList(model.Length);
            UserList.Add(model.Head.Data);
            ListAdd(model.Head.Next);
        }
        private void ListAdd(CookTime.REST_API_UserModel.Next next)
        {
            if (next.NextNext!=null)
            {
                UserList.Add(next.Data);
                ListAddRest(next.NextNext);
            }
            else
            {
                ListReturn();
            }
        }
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
            ListaUsers.ItemsSource = UserList;
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