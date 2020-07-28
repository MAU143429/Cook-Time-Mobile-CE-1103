using CookTime.REST_API_RecipeListModel;
using CookTime.User;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class show the most recently recipes that was created
    /// @author Jose A.
    /// </summary>
    public partial class HomePage : ContentPage
    {
        CookTime.REST_API_RecipeModel.Recipe currentRecipe;
        ArrayList RecipeList;
        public int index = 0;
        public static int length;
        /// <summary>
        /// This constructor execute Home Page
        /// @author Jose A.
        /// </summary>
        public float rate1;
        public HomePage()
        {

            InitializeComponent();
            Pull_Search_Request();
            Notification_Alert();
        }
        /// <summary>
        /// This method notifies the user whenever a user has commented their recipe, followed them of shared their recipe.
        /// @author Jose A.
        /// </summary>
        public async void  Notification_Alert()
        {
            if (LoginPage.CURRENTUSER.hasNotification == true)
            {
               
                HttpClient client = new HttpClient();
                string url = "http://" + LoginPage.ip + ":6969/setUser/"+LoginPage.CURRENTUSER.Email+"/boolean";
                var result = await client.GetAsync(url);
                var json = result.Content.ReadAsStringAsync().Result;
            }
        }

        /// <summary>
        /// This method brings all the recipes from the recipe tree, the newer first.
        /// @author Jose A.
        /// </summary>
        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/getRecipeList";
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
        /// This method starts the list travel with the first key wich is "head"
        /// @author Jose A.
        /// </summary>
        public void StartList(RecipeListModel model)
        {
            InitList();
            if (model.Head.Next != null)
            {
                RecipeList.Add(model.Head.Data);
                ListAdd(model.Head.Next);
            }
            else
            {
                RecipeList.Add(model.Head.Data);
                ListReturn();
            }
        }
        /// <summary>
        /// This method is for traversing the list and adding every Data object
        /// @author Jose A.
        /// </summary>
        private void ListAdd(CookTime.REST_API_RecipeListModel.Next next)
        {
            if (next.NextNext != null)
            {
                RecipeList.Add(next.Data);
                ListAddRest(next.NextNext);
            }
            else
            {
                RecipeList.Add(next.Data);
                ListReturn();
            }
        }
        /// <summary>
        /// This method is for traversing the list and adding every Data object
        /// @author Jose A.
        /// </summary>
        private void ListAddRest(CookTime.REST_API_RecipeListModel.Head head)
        {
            if (head.Next != null)
            {
                RecipeList.Add(head.Data);
                ListAdd(head.Next);
            }
            else
            {
                RecipeList.Add(head.Data);
                ListReturn();
            }
        }
        /// <summary>
        /// This method changes the value of labels and images everytime a new company is loaded on the newsfeed
        /// @author Jose A.
        /// </summary>
        public void ListReturn()
        {
            length = RecipeList.Count;
            currentRecipe = (CookTime.REST_API_RecipeModel.Recipe)RecipeList[index];
            recipeauthor.Text = currentRecipe.Author;
            recipename.Text = currentRecipe.Title;
            recipeimage.Source = currentRecipe.Image;
            reciperating.Text = Convert.ToString(Math.Round(currentRecipe.Rating,1)) + "☆";


        }
        /// <summary>
        /// This method instances the used array list
        /// @author Jose A.
        /// </summary>
        public void InitList()
        {
            RecipeList = new ArrayList();
        }

        /// <summary>
        /// This method is used to refresh the home page
        /// @author Mauricio C.
        /// </summary>
        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());

        }
        /// <summary>
        /// This method is used to change the current page to Search page
        /// @author Mauricio C.
        /// </summary>
        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());

        }
        /// <summary>
        /// This method is used to change the current page to Profile1 page
        /// @author Mauricio C.
        /// </summary>
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
        /// <summary>
        /// This method is used to change the current recipe to other 
        /// @author Mauricio C.
        /// </summary>

        private async void Next(object sender, EventArgs e)
        {
            Console.WriteLine("LENGHT:"+length);
            Console.WriteLine("INDEX:"+index);
            if (index >= length) {

                
                index = 0;
                ListReturn();
                return;

            }
            
            else
            {
                
                ListReturn();
                index++;
                var rate = rating.SelectedIndex;
                if (rate >= 0 && rate <= 4)
                {
                    await DisplayAlert("COOKTIME", "Rating was sent successfully", "ACCEPT");

                }
                if (rating.SelectedIndex == -1)
                {
                    await DisplayAlert("COOKTIME", "Select one rating to continue", "ACCEPT");
                }
                else
                {
                    rate1 = (rating.SelectedIndex + 1);
                    Console.WriteLine(rate1);
                    HttpClient client = new HttpClient();
                    string url = "http://" + LoginPage.ip + ":6969/newRating/" + currentRecipe.Title + "/" + rate1 + "/";
                    var result = await client.GetAsync(url);
                    var json = result.Content.ReadAsStringAsync().Result;
                    
                    
                }

            }

            
        }
        private void View_Recipe(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ViewRecipe(currentRecipe));

        }
        private async void Share_Recipe(object sender, EventArgs e)
        {
            currentRecipe.Author = LoginPage.CURRENTUSER.Email;
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/newRecipe";
            String jsonNewRecipe = JsonConvert.SerializeObject(currentRecipe);
            var datasent = new StringContent(jsonNewRecipe);
            datasent.Headers.ContentType.MediaType = "application/json";
            var result = await client.PostAsync(url, datasent);
            var json = result.Content.ReadAsStringAsync().Result;
            await DisplayAlert("Cook Time", "Recipe was shared successfully", "Aceptar");
        }
    }
}