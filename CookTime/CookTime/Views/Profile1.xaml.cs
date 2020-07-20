
using CookTime.REST_API_RecipeListModel;
using CookTime.REST_API_RecipeModel;
using CookTime.User;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// This class show the user profile, and allows create companies and recipes
    /// @author Mauricio C.
    /// </summary>
    public partial class Profile1 : ContentPage
    {

        /// <summary>
        /// This constructor execute Profile1 partial class 
        /// @author Mauricio C.
        /// </summary>
        /// 
        CookTime.REST_API_UserModel.User myprofile = LoginPage.CURRENTUSER;
        ArrayList RecipeList;
        public Profile1()
        {
            InitializeComponent();
            Pull_Search_Request();
            profileimg.Source = myprofile.Image;
            username.Text = myprofile.Name;
            posts.Text = Convert.ToString(myprofile.Recipes.Count);
            followers.Text = Convert.ToString(myprofile.Followers.Count);
            following.Text = Convert.ToString(myprofile.Following.Count);

            if (LoginPage.CURRENTUSER.Hascompany == true)
            {
                CreateCompany.IsEnabled = false;
            }
        }

        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/getRecipe/" + LoginPage.CURRENTUSER.Email + "/user";
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
            ListaR.ItemsSource = RecipeList;
            Console.WriteLine(RecipeList[0]);
        }
        public void InitList()
        {
            RecipeList = new ArrayList();
        }

        /// <summary>
        /// This method is used to change the current page to Home page
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
        /// This method is used to update profile1 page
        /// @author Mauricio C.
        /// </summary>
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
        /// <summary>
        /// This method is used to change the current page to Create Recipe page
        /// @author Mauricio C.
        /// </summary>
        private void Create_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateR());

        }
        /// <summary>
        /// This method is used to change the current page to Create Company page
        /// @author Mauricio C.
        /// </summary>
        private void Create2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateC());
        }
        /// <summary>
        /// This method is used to change the current page to CompanyProfile page
        /// @author Mauricio C.
        /// </summary>
        private void CompanyP(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyProfile());

        }
        /// <summary>
        /// This method is used to change the current page to ChnagePhoto page
        /// @author Mauricio C.
        /// </summary>

        private void Change_Photo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePhoto());

        }
        /// <summary>
        /// This method is used to change the current page to ChangePassword page
        /// @author Mauricio C.
        /// </summary>
        private void Change_Pass(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());

        }


       



        /// <summary>
        /// This method is used to change the current page to ViewRecipe page
        /// @author Mauricio C.
        /// </summary>
        public void View_Recipe(object sender, EventArgs e)
        {
            Recipe item = (Recipe)ListaR.SelectedItem;
            Navigation.PushAsync(new ViewRecipe(item));
        }
  
        public async void updateuser(object sender, EventArgs e)
        {


            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/email/getUser/" + LoginPage.CURRENTUSER.Email;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            myprofile = CookTime.REST_API_UserModel.User.FromJson(json);
            Console.WriteLine(myprofile.Image);

            

           
        }


    }
}