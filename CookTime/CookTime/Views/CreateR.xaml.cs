using CookTime.REST_API_RecipeModel;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class allows users to create a new recipe
    /// @author Mauricio C.
    /// </summary>
    public partial class CreateR : ContentPage
    {
        /// <summary>
        /// This constructor execute Create Recipe Page
        /// @author Mauricio C.
        /// </summary>
        public int value;
        public CreateR(int num)
        {
            value = num;
            InitializeComponent();
            
        }
        /// <summary>
        /// This method is used to change the current page to Profile1 page and share the recipe in NewsFeed Page
        /// @author Jose A.
        /// </summary>
        private void Share_Clicked(object sender, EventArgs e)
        {
            if (value == 1)
            {
                Share_Recipe();
                LoginPage.updateUser();
                Navigation.PushAsync(new Profile1());
            }
            else
            {
                Share_RecipeC();
                LoginPage.updateUser();
                Navigation.PushAsync(new CompanyProfile());
            }
            
            DisplayAlert("Upload Successfull!", "Your recipe was uploaded successfully!", "OK");
            
            

        }
        /// <summary>
        /// This method create a json with all recipe details and update My Menu Board
        /// @author Jose A.
        /// </summary>
        private async void Share_Recipe()
        {

            var dish = Dishes.SelectedItem;
            var duration = Duration.SelectedItem;
            var time = Time.SelectedItem;
            var diets = Diet.SelectedItem;
            var date = DateTime.Now;

            Recipe recipe = new Recipe();
            recipe.Title = namerecipe.Text;
            recipe.Author = LoginPage.CURRENTUSER.Email;
            recipe.TypeOfDish = dish.ToString();
            recipe.Servings = Int32.Parse(servings.Text);
            recipe.Duration = duration.ToString();
            recipe.Time = time.ToString();
            recipe.Difficulty = Int32.Parse(Difficulty.Text);
            recipe.Diet = diets.ToString();
            recipe.Description = Ingredients.Text;
            recipe.Steps = Steps.Text;
            recipe.Price = Int32.Parse(Price.Text);
            recipe.Image = imageurl.Text;
            recipe.Date = DateTime.Now.ToString("dd/MM/yyyy");
            recipe.Rating = 0;
            

            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/newRecipe";
            String jsonNewRecipe = JsonConvert.SerializeObject(recipe);
            var datasent = new StringContent(jsonNewRecipe);
            datasent.Headers.ContentType.MediaType = "application/json";
            var result = await client.PostAsync(url, datasent);
            var json = result.Content.ReadAsStringAsync().Result;
            await DisplayAlert("Result", json, "ok");
            Console.WriteLine("RECIPE" + jsonNewRecipe);
            Console.WriteLine("RECIPE RESPONSE" + json );


        }

        private async void Share_RecipeC()
        {

            var dish = Dishes.SelectedItem;
            var duration = Duration.SelectedItem;
            var time = Time.SelectedItem;
            var diets = Diet.SelectedItem;
            var date = DateTime.Now;

            Recipe recipe = new Recipe();
            recipe.Title = namerecipe.Text;
            recipe.Author = CompanyProfile.mycompany.Email;
            recipe.TypeOfDish = dish.ToString();
            recipe.Servings = Int32.Parse(servings.Text);
            recipe.Duration = duration.ToString();
            recipe.Time = time.ToString();
            recipe.Difficulty = Int32.Parse(Difficulty.Text);
            recipe.Diet = diets.ToString();
            recipe.Description = Ingredients.Text;
            recipe.Steps = Steps.Text;
            recipe.Price = Int32.Parse(Price.Text);
            recipe.Image = imageurl.Text;
            recipe.Date = DateTime.Now.ToString("dd/MM/yyyy");
            recipe.Rating = 0;


            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/company/newRecipe";
            String jsonNewRecipe = JsonConvert.SerializeObject(recipe);
            var datasent = new StringContent(jsonNewRecipe);
            datasent.Headers.ContentType.MediaType = "application/json";
            var result = await client.PostAsync(url, datasent);
            var json = result.Content.ReadAsStringAsync().Result;
            await DisplayAlert("Result", json, "ok");
            Console.WriteLine("RECIPE" + jsonNewRecipe);
            Console.WriteLine("RECIPE RESPONSE" + json);


        }


    }
}