using CookTime.REST_API_RecipeModel;
using CookTime.User;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class allows users to see a summary of recipes details
    /// @author Mauricio C.
    /// </summary>
    public partial class ViewRecipe : ContentPage
    {
        ArrayList RecipeList;
        public static Recipe showrecipe;
        /// <summary>
        /// This constructor execute ViewRecipe partial class 
        /// @author Mauricio C.
        /// </summary>
        public ViewRecipe(CookTime.REST_API_RecipeModel.Recipe recipe)
        {

            showrecipe = recipe;
            InitializeComponent();
            update();
            InitList();
            StartPage(recipe);

        }
        
        /// <summary>
        /// This method take the data of recipe and add it to the RecipeList
        /// @author Mauricio C.
        /// </summary>
        private void StartPage(CookTime.REST_API_RecipeModel.Recipe Recipe)
        {
            RecipeList.Add(Recipe);
            ListReturn();
        }
        /// <summary>
        /// This method take one recipe and delete it of MyMenu
        /// @author Mauricio C.
        /// </summary>
        private async void DeleteR(object sender, EventArgs e)
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/deleteRecipe/" + showrecipe.Title + "/";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            Console.WriteLine(json);
            await Navigation.PushAsync(new Search());
            await DisplayAlert("COOKTIME", "RECIPE HAS BEEN DELETED", "ACCEPT");
           
            
            
        }

        private void update()
        {
            if (showrecipe.Author == LoginPage.CURRENTUSER.Email)
            {
                rating.IsEnabled = false;
                sharecipe.IsEnabled = false;
                
            }
            if (showrecipe.Author != LoginPage.CURRENTUSER.Email)
            {
                Delete.IsEnabled = false;
            }
            

        }
        /// <summary>
        /// This method take one recipe and share in newsfeed page
        /// @author Mauricio C.
        /// </summary>
        private void ShareRecipe (object sender, EventArgs e)
        {
            
            
            Navigation.PushAsync(new Search());
            DisplayAlert("COOKTIME", "RECIPE HAS BEEN SHARED", "ACCEPT");
              
        }

        /// <summary>
        /// This method allow users to comment in one recipe
        /// @author Mauricio C.
        /// </summary>
        private async void CommentR(object sender, EventArgs e)
        {

            var commentValidate = Comment.Text;
            if (!string.IsNullOrEmpty(commentValidate))
            {
                HttpClient client = new HttpClient();
                string url = "http://" + LoginPage.ip + ":6969/addComment/" + showrecipe.Title+ "/" + LoginPage.CURRENTUSER.Email + "/" ;
                String newcomment = Comment.Text;
                var datasent = new StringContent(newcomment);
                datasent.Headers.ContentType.MediaType = "application/json";
                var result = await client.PostAsync(url, datasent);
                var json = result.Content.ReadAsStringAsync().Result;
                Console.WriteLine(json);
                await DisplayAlert("COOKTIME", "YOUR COMMENT HAS BEEN PUBLISHED", "ACCEPT");
                DependencyService.Get<iNotification>().CreateNotification("CookTime", "Un usuario ha comentado en tu receta!");
                await Navigation.PushAsync(new  Search());

            }
            else
            {
                await DisplayAlert("COOKTIME", "ENTER A COMMENT TO CONTINUE ", "ACCEPT");
            }

        }
        public void ListReturn()
        {
            ListaRecipe.ItemsSource = RecipeList;
            Console.WriteLine(RecipeList[0]);
        }
        public void InitList()
        {
            RecipeList = new ArrayList();
        }

    }
}