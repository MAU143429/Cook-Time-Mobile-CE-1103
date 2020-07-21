using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;
using CookTime.REST_API_CompanyListModel;
using CookTime.REST_API_RecipeListModel;
using CookTime.REST_API_UserListModel;
using System.Security.Cryptography;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        ArrayList UserList;
        ArrayList RecipeList;
        ArrayList CompanyList;
        public Search()
        {
            InitializeComponent();
           // Pull_Better_Companies();
            Pull_Better_Recipes();
           //Pull_Better_Users();


        }
        private async void Pull_Better_Users()
        {
            HttpClient clientUsers = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/user";//TIENE QUE SER LOS 3 USUARIOS CON MAYOR RATING
            var result = await clientUsers.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            UserListModel newusermodel = UserListModel.FromJson(json);
            StartUserList(newusermodel);
        }
        private async void Pull_Better_Recipes()
        {
            HttpClient clientRecipes = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getRatings";//TIENE QUE SER LOS 3 RECETAS CON MAYOR RATING
            var result = await clientRecipes.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            CookTime.REST_API_RecipeListModel.RecipeListModel newrecipemodel = CookTime.REST_API_RecipeListModel.RecipeListModel.FromJson(json);
            StartRecipeList(newrecipemodel);
        }
        private async void Pull_Better_Companies()
        {
            HttpClient clientCompanies = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/test";//TIENE QUE SER LOS 3 RESTAURANTES CON MAYOR RATING
            var result = await clientCompanies.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            CookTime.REST_API_CompanyListModel.CompanyListModel newcompanymodel = CookTime.REST_API_CompanyListModel.CompanyListModel.FromJson(json);
            StartCompanyList(newcompanymodel);
        }
        public void StartUserList(UserListModel model)
        {
            InitUserList();
            if (model.Head.Next != null)
            {
                UserList.Add(model.Head.Data);
                UserListAdd(model.Head.Next);
            }
            else
            {
                UserList.Add(model.Head.Data);
                UserListReturn();
            }
        }
        public void StartRecipeList(RecipeListModel model)
        {
            InitRecipeList();
            if (model.Head.Next != null)
            {
                RecipeList.Add(model.Head.Data);
                RecipeListAdd(model.Head.Next);
            }
            else
            {
                RecipeList.Add(model.Head.Data);
                RecipeListReturn();
            }
        }
        public void StartCompanyList(CookTime.REST_API_CompanyListModel.CompanyListModel model)
        {
            InitCompanyList();
            if (model.Head.Next != null)
            {
                CompanyList.Add(model.Head.Data);
                CompanyListAdd(model.Head.Next);
            }
            else
            {
                CompanyList.Add(model.Head.Data);
                CompanyListReturn();
            }
        }
        private void UserListAdd(CookTime.REST_API_UserListModel.Next next)
        {
            if (next.NextNext != null)
            {
                UserList.Add(next.Data);
                UserListAddRest(next.NextNext);
            }
            else
            {
                UserList.Add(next.Data);
                UserListReturn();
            }
        }
        private void UserListAddRest(CookTime.REST_API_UserListModel.Head head)
        {
            if (head.Next != null)
            {
                UserList.Add(head.Data);
                UserListAdd(head.Next);
            }
            else
            {
                UserList.Add(head.Data);
                UserListReturn();
            }
        }
        private void RecipeListAdd(CookTime.REST_API_RecipeListModel.Next next)
        {
            if (next.NextNext != null)
            {
                RecipeList.Add(next.Data);
                RecipeListAddRest(next.NextNext);
            }
            else
            {
                RecipeList.Add(next.Data);
                RecipeListReturn();
            }
        }
        private void RecipeListAddRest(CookTime.REST_API_RecipeListModel.Head head)
        {
            if (head.Next != null)
            {
                RecipeList.Add(head.Data);
                RecipeListAdd(head.Next);
            }
            else
            {
                RecipeList.Add(head.Data);
                RecipeListReturn();
            }
        }
        private void CompanyListAdd(CookTime.REST_API_CompanyListModel.Next next)
        {
            if (next.NextNext != null)
            {
                CompanyList.Add(next.Data);
                CompanyListAddRest(next.NextNext);
            }
            else
            {
                CompanyList.Add(next.Data);
                CompanyListReturn();
            }
        }
        private void CompanyListAddRest(CookTime.REST_API_CompanyListModel.Head head)
        {
            if (head.Next != null)
            {
                CompanyList.Add(head.Data);
                CompanyListAdd(head.Next);
            }
            else
            {
                CompanyList.Add(head.Data);
                CompanyListReturn();
            }
        }
        public void UserListReturn()
        {
            RecommendedUser.ItemsSource = UserList;
        }
        public void RecipeListReturn()
        {
            RecommendedRecipes.ItemsSource = RecipeList;
        }
        public void CompanyListReturn()
        {
            RecommendedCompanies.ItemsSource = CompanyList;
        }
        public void InitUserList()
        {
            UserList = new ArrayList();
        }
        public void InitCompanyList()
        {
            CompanyList = new ArrayList();
        }
        public void InitRecipeList()
        {
            RecipeList = new ArrayList();
        }


        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());

        }
        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());

        }
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
        private void Profile_View(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ProfileView());

        }
        private void Recipe_View(object sender, EventArgs e)
        {
            //Navigation.PushAsync(new ViewRecipe());
            

        }
        private void Company_View(object sender, EventArgs e)
        { 
            Navigation.PushAsync(new CompanyProfile());

        }


        private void Searching(object sender, EventArgs e)
        {
            var searchValidate = search1.Text;
            if (!string.IsNullOrEmpty(searchValidate))
            {
                if (usersearch.IsChecked || companysearch.IsChecked || recipesearch.IsChecked)
                {

                    if (Checked_Search() != 0)
                    {
                        DisplayAlert("COOKTIME", "SEARCHING...", "ACCEPT");

                        if (Checked_Search() == 1)
                        {
                            Navigation.PushAsync(new ShowSearch());
                            DisplayAlert("COOKTIME", "user", "ACCEPT");
                        }
                        if (Checked_Search() == 2)
                        {
                            var filter1 = DishesS.SelectedIndex;
                            var filter2 = DurationS.SelectedIndex;
                            var filter3 = ServingsS.SelectedIndex;
                            var f1 = DishesS.SelectedItem;
                            Boolean v1, v2, v3;
                            v1 = false;
                            v2 = false;
                            v3 = false;

                            if (filter1 >= 0 && filter1 <= 4) { v1 = true; Console.WriteLine(f1); }
                            if (filter2 >= 0 && filter2 <= 4) { v2 = true; }
                            if (filter3 >= 0 && filter3 <= 4) { v3 = true; }

                            Navigation.PushAsync(new ShowSearch());
                            DisplayAlert("COOKTIME", "recipe", "ACCEPT");



                        }
                        if (Checked_Search() == 3)
                        {
                            Navigation.PushAsync(new ShowSearch());
                            DisplayAlert("COOKTIME", "company", "ACCEPT");

                        }
                 

                    }

                }
                else
                {
                    DisplayAlert("COOKTIME", "Select one type of search to continue", "ACCEPT");

                }
            }
            else
            {
                DisplayAlert("ERROR", "YOU MUST FILL ALL THE BLANKS TO CONTINUE", "ACCEPT");


            }

        }

        private int Checked_Search()
        {
            if (usersearch.IsChecked && !recipesearch.IsChecked && !companysearch.IsChecked) { return 1; }
            if (!usersearch.IsChecked && recipesearch.IsChecked && !companysearch.IsChecked) { return 2; }
            if (!usersearch.IsChecked && !recipesearch.IsChecked && companysearch.IsChecked) { return 3; }

            else { DisplayAlert("COOKTIME", "Selecciona no mas de un tipo de busqueda", "ACCEPT"); return 0; }
        }

    }
}