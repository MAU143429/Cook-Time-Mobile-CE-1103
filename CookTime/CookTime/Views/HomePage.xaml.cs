using CookTime.REST_API_RecipeListModel;
using CookTime.User;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
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
        public HomePage() 
        {
            
            InitializeComponent();
            Pull_Search_Request();
            

        }


        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getDates";
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
        public void ListReturn()
        {
            length = RecipeList.Count;
            currentRecipe = (CookTime.REST_API_RecipeModel.Recipe)RecipeList[index];
            recipeauthor.Text = currentRecipe.Author;
            recipename.Text = currentRecipe.Title;
            recipeimage.Source = currentRecipe.Image;
            reciperating.Text = Convert.ToString(currentRecipe.Rating) + "☆";


        }
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

                await DisplayAlert("Cook Time", "Ya no hay recetas en el servidor", "Aceptar");
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
                    DependencyService.Get<iNotification>().CreateNotification("CookTime", "Un usuario ha calificado tu receta!");
                    
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

        }
    }
}