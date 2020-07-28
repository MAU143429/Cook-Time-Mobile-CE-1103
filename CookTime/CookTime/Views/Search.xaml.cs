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
using CookTime.REST_API_RecipeModel;

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
            Pull_Better_Companies();
            Pull_Better_Recipes();
            Pull_Better_Users();


        }
        /// <summary>
        /// This method brings 3 users from the user tree to be shown on the recommened section
        /// @author Jose A.
        /// </summary>
        private async void Pull_Better_Users()
        {
            HttpClient clientUsers = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/getUser/userShuffledList";
            var result = await clientUsers.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            UserListModel newusermodel = UserListModel.FromJson(json);
            if (newusermodel.Length == 0)
            {
                return;
            }
            else
            {
                StartUserList(newusermodel);
            }

        }
        /// <summary>
        /// This method brings the 3 best rated recipes from the recipe tree to be shown on the recommened section
        /// @author Jose A.
        /// </summary>
        private async void Pull_Better_Recipes()
        {
            HttpClient clientRecipes = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/sorting/getRatings";
            var result = await clientRecipes.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            CookTime.REST_API_RecipeListModel.RecipeListModel newrecipemodel = CookTime.REST_API_RecipeListModel.RecipeListModel.FromJson(json);
            if (newrecipemodel.Length == 0)
            {
                return;
            }
            else
            {
                StartRecipeList(newrecipemodel);
            }
            
        }
        /// <summary>
        /// This method brings 3 companies from the company tree to be shown on the recommened section
        /// @author Jose A.
        /// </summary>
        private async void Pull_Better_Companies()
        {
            HttpClient clientCompanies = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/getCompany/companyShuffledList";//TIENE QUE SER LOS 3 RESTAURANTES CON MAYOR RATING
            var result = await clientCompanies.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            CookTime.REST_API_CompanyListModel.CompanyListModel newcompanymodel = CookTime.REST_API_CompanyListModel.CompanyListModel.FromJson(json);
            if (newcompanymodel.Length == 0)
            {
                return;
            }
            else
            {
                StartCompanyList(newcompanymodel);
            }

            
        }
        /// <summary>
        /// This method starts the list travel by adding the head data for user lists
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method starts the list travel by adding the head data for recipe lists
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method starts the list travel by adding the head data for company lists
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method adds the Data object from the singly list to the array list of users that is being worked
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method adds the Data object from the singly list to the array list of users that is being worked
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method adds the Data object from the singly list to the array list of recipes that is being worked
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method adds the Data object from the singly list to the array list of recipes that is being worked
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method adds the Data object from the singly list to the array list of companies that is being worked
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method adds the Data object from the singly list to the array list of companies that is being worked
        /// @author Jose A.
        /// </summary>
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
        /// <summary>
        /// This method binds the array list data into the list view item source for visual aid
        /// @author Jose A.
        /// </summary>
        public void UserListReturn()
        {
            RecommendedUser.ItemsSource = UserList;
        }
        /// <summary>
        /// This method binds the array list data into the list view item source for visual aid
        /// @author Jose A.
        /// </summary>
        public void RecipeListReturn()
        {
            RecommendedRecipes.ItemsSource = RecipeList;
        }
        /// <summary>
        /// This method binds the array list data into the list view item source for visual aid
        /// @author Jose A.
        /// </summary>
        public void CompanyListReturn()
        {
            RecommendedCompanies.ItemsSource = CompanyList;
        }
        /// <summary>
        /// This method instances the array list we are goint to work on
        /// @author Jose A.
        /// </summary>
        public void InitUserList()
        {
            UserList = new ArrayList();
        }
        /// <summary>
        /// This method instances the array list we are goint to work on
        /// @author Jose A.
        /// </summary>
        public void InitCompanyList()
        {
            CompanyList = new ArrayList();
        }
        /// <summary>
        /// This method instances the array list we are goint to work on
        /// @author Jose A.
        /// </summary>
        public void InitRecipeList()
        {
            RecipeList = new ArrayList();
        }

        /// <summary>
        /// This method pushes a new home page into the pages stack
        /// @author Jose A.
        /// </summary>
        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());

        }
        /// <summary>
        /// This method pushes a new search page into the pages stack
        /// @author Jose A.
        /// </summary>
        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());

        }
        /// <summary>
        /// This method pushes a new profile page into the pages stack
        /// @author Jose A.
        /// </summary>
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
        /// <summary>
        /// This method pushes a new profile page into the pages stack using a user from the recommended section
        /// @author Jose A.
        /// </summary>
        private void Profile_View(object sender, EventArgs e)
        {
            CookTime.REST_API_UserModel.User item = (CookTime.REST_API_UserModel.User)RecommendedUser.SelectedItem;
            Navigation.PushAsync(new ProfileView(item));

        }
        /// <summary>
        /// This method pushes a new recipe view page into the pages stack using a recipe from the recommended section
        /// @author Jose A.
        /// </summary>
        private void Recipe_View(object sender, EventArgs e)
        {
            Recipe item = (Recipe)RecommendedRecipes.SelectedItem;
            Navigation.PushAsync(new ViewRecipe(item));
            

        }
        /// <summary>
        /// This method pushes a new company view page into the pages stack using a company from the recommended section
        /// @author Jose A.
        /// </summary>
        private void Company_View(object sender, EventArgs e)
        {
            CookTime.REST_API_CompanyModel.Company item = (CookTime.REST_API_CompanyModel.Company)RecommendedCompanies.SelectedItem;
            Navigation.PushAsync(new CompanyProfileView(item));

        }

        /// <summary>
        /// This method manages all the logic and server interaction needed for the 4 possible ways of searching users, companies, recipes by name and recipes by filters
        /// @author Jose A.
        /// </summary>
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
                            Navigation.PushAsync(new ShowSearch(1,search1.Text,"","",0));
                            DisplayAlert("COOKTIME", "user", "ACCEPT");

                        }
                        if (Checked_Search() == 2)
                        {
                            var filter1 = DishesS.SelectedIndex;
                            var filter2 = DurationS.SelectedIndex;
                            var filter3 = ServingsS.SelectedIndex;

                            string sel1, sel2;
                            int sel3;




                            if (filter1 == -1)
                            {
                                 sel1 = null;
                            }
                            else
                            {
                                 sel1 = DishesS.SelectedItem.ToString();
                            }

                            if (filter2 == -1)
                            {
                                 sel2 = null;
                            }
                            else
                            {
                                 sel2 = DurationS.SelectedItem.ToString();
                            }

                            if (filter3 == -1)
                            {
                                sel3 = 0; 
                            }
                            else
                            {
                                string self3temp = ServingsS.SelectedItem.ToString();
                                sel3 = Int32.Parse(self3temp);
                            }

                            


                            Navigation.PushAsync(new ShowSearch(2, search1.Text, sel1, sel2, sel3));
                            DisplayAlert("COOKTIME", "recipe", "ACCEPT");



                        }
                        if (Checked_Search() == 3)
                        {
                            Navigation.PushAsync(new ShowSearch(3, search1.Text,"","",0));
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
        /// <summary>
        /// This method returns a number to manage cases of selection of the filters
        /// @author Jose A.
        /// </summary>
        private int Checked_Search()
        {
            if (usersearch.IsChecked && !recipesearch.IsChecked && !companysearch.IsChecked) { return 1; }
            if (!usersearch.IsChecked && recipesearch.IsChecked && !companysearch.IsChecked) { return 2; }
            if (!usersearch.IsChecked && !recipesearch.IsChecked && companysearch.IsChecked) { return 3; }

            else { DisplayAlert("COOKTIME", "Select no more than one type of search", "ACCEPT"); return 0; }
        }

    }
}