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
    /// <summary>
    /// This class  show the user profile, and allows users to see and share recipes
    /// @author Jose A.
    /// </summary>
    public partial class ProfileView : ContentPage
    {
        /// <summary>
        /// This constructor execute Profile1 partial class and start a new pull search request to the server 
        /// @author Jose A.
        /// </summary>

       
        ArrayList ProfileListView;
        public ProfileView()
        {
            InitializeComponent();
            Pull_Search_Request();
            

        }
        /// <summary>
        /// This method create a new HTTP client and execute async method with the server to get the user data 
        /// @author Jose A.
        /// </summary>
        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://192.168.100.7:6969/user";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            UserListModel newmodel = UserListModel.FromJson(json);
            StartList(newmodel);
        }
        /// <summary>
        /// This method take the first element and add it
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method take the data and add it to the ProfileListView
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method verify the current data is the last element in json file, if it is the last element the method add it and return the finally list
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method display the list
        /// @author Jose A.
        /// </summary>
        public void ListReturn()
        {
            ListaRPV.ItemsSource = ProfileListView;
            Console.WriteLine(ProfileListView[0]);
        }
        /// <summary>
        /// This method inicializate ArrayList
        /// @author Jose A.
        /// </summary>
        public void InitList()
        {
            ProfileListView = new ArrayList();
        }
        /// <summary>
        /// This method is used to change the current page to View Recipe page
        /// @author Mauricio C.
        /// </summary>
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
            recipe.Description = "Arroz, pollo, chile, etc";
            recipe.Steps = "Primero se hace el arroz, después se sacan trozos de la pechuga de pollo,....";
            recipe.Price = 3500;
            recipe.Image = "https://img-global.cpcdn.com/recipes/bbbdd85a4baaadd8/400x400cq70/photo.jpg";
            Navigation.PushAsync(new ViewRecipe(recipe));

        }
        /// <summary>
        /// Tthis method sends a notification to notify that there are new followers
        /// @author Mauricio C.
        /// </summary>
        private void Send_Notification(object sender, EventArgs e)
        {




            DependencyService.Get<iNotification>().CreateNotification("CookTime", "Un usuario nuevo te ha seguido!");

        }


    }
}