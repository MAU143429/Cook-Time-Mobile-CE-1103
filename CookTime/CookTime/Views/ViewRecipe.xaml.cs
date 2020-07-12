using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewRecipe : ContentPage
    {
        ArrayList RecipeList;
        public ViewRecipe(CookTime.REST_API_RecipeModel.Recipe recipe)
        {
            InitializeComponent();
            InitList();
            StartPage(recipe);
        }
        private void StartPage(CookTime.REST_API_RecipeModel.Recipe Recipe)
        {
            RecipeList.Add(Recipe);
            ListReturn();
        }
        private void DeleteR(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowSearch());
            DisplayAlert("COOKTIME", "RECIPE HAS BEEN DELETED", "ACCEPT");
        }

        private void CommentR(object sender, EventArgs e)
        {


            var commentValidate = Comment.Text;
            if (!string.IsNullOrEmpty(commentValidate))
            {
                DisplayAlert("COOKTIME", "YOUR COMMENT HAS BEEN PUBLISHED", "ACCEPT");
                Navigation.PushAsync(new ShowSearch());

            }
            else
            {
                DisplayAlert("COOKTIME", "ENTER A COMMENT TO CONTINUE ", "ACCEPT");
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