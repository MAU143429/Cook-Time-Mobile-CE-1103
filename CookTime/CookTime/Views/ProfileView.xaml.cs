using CookTime.REST_API_UserListModel;
using CookTime.REST_API_RecipeModel;
using CookTime.User;
using System;
using System.Collections;
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
    public partial class ProfileView : ContentPage
    {
        //List<Object> UserList;
        //List<Object> ShownList;
        ArrayList ProfileListView;
        public ProfileView()
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
            if (model.Head.Next != null)
            {
                ProfileListView.Add(model.Head.Data);
                ListAdd(model.Head.Next);
            }
            else
            {
                ProfileListView.Add(model.Head.Data);
                ListReturn();
            }
        }
        private void ListAdd(CookTime.REST_API_UserListModel.Next next)
        {
            if (next.NextNext != null)
            {
                ProfileListView.Add(next.Data);
                ListAddRest(next.NextNext);
            }
            else
            {
                ProfileListView.Add(next.Data);
                ListReturn();
            }
        }
        private void ListAddRest(CookTime.REST_API_UserListModel.Head head)
        {
            if (head.Next != null)
            {
                ProfileListView.Add(head.Data);
                ListAdd(head.Next);
            }
            else
            {
                ProfileListView.Add(head.Data);
                ListReturn();
            }
        }
        public void ListReturn()
        {
            ListaRPV.ItemsSource = ProfileListView;
            Console.WriteLine(ProfileListView[0]);
        }
        public void InitList()
        {
            ProfileListView = new ArrayList();
        }
        private void View_Recipe(object sender, EventArgs e)
        {
            Recipe recipe = new Recipe();
            recipe.Title = "Arroz con Pollo";
            recipe.Author = "yomeroelmismo@yahoo.com";
            recipe.TypeOfDish = "Breakfast";
            recipe.Servings = 2;
            recipe.Duration = "1 - 3 hours";
            recipe.Time = "Appetizer";
            recipe.Difficulty = 67;
            recipe.Diet = "Carnivourous";
            recipe.Ingredients = "Arroz, pollo, chile, etc";
            recipe.Steps = "Primero se hace el arroz, después se sacan trozos de la pechuga de pollo,....";
            recipe.Price = 3500;
            recipe.ImageURL = "https://img-global.cpcdn.com/recipes/bbbdd85a4baaadd8/400x400cq70/photo.jpg";
            Navigation.PushAsync(new ViewRecipe(recipe));

        }
        private void Send_Notification(object sender, EventArgs e)
        {
            DependencyService.Get<iNotification>().CreateNotification("CookTime", "Un usuario nuevo te ha seguido!");

        }


    }
}