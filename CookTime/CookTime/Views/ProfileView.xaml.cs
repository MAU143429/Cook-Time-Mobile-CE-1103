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
using Xamarin.Forms.Internals;
using CookTime.REST_API_RecipeListModel;

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

        CookTime.REST_API_UserModel.User User;
        ArrayList ProfileListView;
        public ProfileView(CookTime.REST_API_UserModel.User user)
        {
            InitializeComponent();
            User = user;
            if (User.Image == null)
            {
                profileimg.Source = "defaultimg.jpg";
            }
            else
            {
                profileimg.Source = User.Image;
            }
            username.Text = User.Name;
            posts.Text = Convert.ToString(User.Recipes.Count);
            followers.Text = Convert.ToString(User.Followers.Count);
            following.Text = Convert.ToString(User.Following.Count);
            Pull_Search_Request();
            

        }
        /// <summary>
        /// This method create a new HTTP client and execute async method with the server to get the user data 
        /// @author Jose A.
        /// </summary>
        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/getRecipe/" + User.Email + "/user";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel listofrecipes = RecipeListModel.FromJson(json);
            Console.WriteLine("RESULT" + json);
            if (listofrecipes.Length == 0)
            {
                return;
            }
            else
            {
                StartList(listofrecipes);
            }
        }
        /// <summary>
        /// This method take the first element and add it
        /// @author Jose A.
        /// </summary>
        public void StartList(RecipeListModel model)
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
        private void ListAdd(CookTime.REST_API_RecipeListModel.Next next)
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
        private void ListAddRest(CookTime.REST_API_RecipeListModel.Head head)
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

            Recipe item = (Recipe)ListaRPV.SelectedItem;
            Navigation.PushAsync(new ViewRecipe(item));

        }
        /// <summary>
        /// This method brings every recipe from an especific user sorted by difficulty
        /// @author Jose A.
        /// </summary>
        private async void Sort_Difficulty(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getDifficulties/" + User.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel recipeList = RecipeListModel.FromJson(json);
            StartList(recipeList);

        }
        /// <summary>
        /// This method brings every recipe from an especific user sorted by date
        /// @author Jose A.
        /// </summary>
        private async void Sort_Date(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getDates/" + User.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel recipeList = RecipeListModel.FromJson(json);
            StartList(recipeList);
        }
        /// <summary>
        /// This method brings every recipe from an especific user sorted by rating
        /// @author Jose A.
        /// </summary>
        private async void Sort_Rating(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getRatings/" + User.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel recipeList = RecipeListModel.FromJson(json);
            StartList(recipeList);
        }

        /// <summary>
        /// Tthis method sends a notification to notify that there are new followers
        /// @author Mauricio C.
        /// </summary>
        private async void Send_Notification(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/user/" + User.Email + "/addFollower/" + LoginPage.CURRENTUSER.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(json);
        }


    }
}