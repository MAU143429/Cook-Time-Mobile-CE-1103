using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CookTime.REST_API_CompanyListModel;
using CookTime.REST_API_CompanyModel;
using CookTime.REST_API_RecipeListModel;
using CookTime.REST_API_RecipeModel;
using CookTime.REST_API_RecipeSearchModel;
using CookTime.REST_API_UserListModel;
using CookTime.REST_API_UserModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowSearch : ContentPage
    {
        //List<Object> UserList;
        //List<Object> ShownList;
        public int Reference;
        public string searchKey;
        public string Type_;
        public string Duration;
        public int Servings;
        ArrayList ObjectList;
        public ShowSearch(int reference, string searchkey,string type,string duration, int servings)
        {
            InitializeComponent();
            searchKey = searchkey;
            Type_ = type;
            Duration = duration;
            Servings = servings;
            Reference = reference;
            if(reference == 1)
            {
                Pull_Search_Request_U();
            }
            if (reference == 2)
            {
                Pull_Search_Request_R();
            }
            if (reference == 3)
            {
                Pull_Search_Request_C();
            }
            
        }
        private async void Pull_Search_Request_U()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/searchUser/"+searchKey;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            UserListModel newmodel = UserListModel.FromJson(json);
            StartList_U(newmodel);
        }
        private async void Pull_Search_Request_R()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/searchRecipe";
            REST_API_RecipeSearchModel.RecipeS searchvalues = new REST_API_RecipeSearchModel.RecipeS();
            searchvalues.Search = searchKey;
            searchvalues.TypeOfDish = Type_;
            searchvalues.Servings = Servings;
            searchvalues.Duration = Duration;
            String jsonSearch = JsonConvert.SerializeObject(searchvalues);
            var datasent = new StringContent(jsonSearch);
            datasent.Headers.ContentType.MediaType = "application/json";
            var result = await client.PostAsync(url, datasent);
            var json = result.Content.ReadAsStringAsync().Result;
            RecipeListModel newmodel = RecipeListModel.FromJson(json);
            StartList_R(newmodel);
        }
        private async void Pull_Search_Request_C()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/searchCompany/"+searchKey;
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            CompanyListModel newmodel = CompanyListModel.FromJson(json);
            StartList_C(newmodel);
        }
        public void StartList_U(UserListModel model)
        {
            InitList();
            if (model.Head.Next != null)
            {
                ObjectList.Add(model.Head.Data);
                ListAdd_U(model.Head.Next);
            }
            else
            {
                ObjectList.Add(model.Head.Data);
                ListReturn();
            }
        }
        public void StartList_R(RecipeListModel model)
        {
            InitList();
            if (model.Head.Next != null)
            {
                ObjectList.Add(model.Head.Data);
                ListAdd_R(model.Head.Next);
            }
            else
            {
                ObjectList.Add(model.Head.Data);
                ListReturn();
            }
        }
        public void StartList_C(CompanyListModel model)
        {
            InitList();
            if (model.Head.Next != null)
            {
                ObjectList.Add(model.Head.Data);
                ListAdd_C(model.Head.Next);
            }
            else
            {
                ObjectList.Add(model.Head.Data);
                ListReturn();
            }
        }
        private void ListAdd_U(CookTime.REST_API_UserListModel.Next next)
        {
            if (next.NextNext != null)
            {
                ObjectList.Add(next.Data);
                ListAddRest_U(next.NextNext);
            }
            else
            {
                ObjectList.Add(next.Data);
                ListReturn();
            }
        }
        private void ListAdd_R(CookTime.REST_API_RecipeListModel.Next next)
        {
            if (next.NextNext != null)
            {
                ObjectList.Add(next.Data);
                ListAddRest_R(next.NextNext);
            }
            else
            {
                ObjectList.Add(next.Data);
                ListReturn();
            }
        }
        private void ListAdd_C(CookTime.REST_API_CompanyListModel.Next next)
        {
            if (next.NextNext != null)
            {
                ObjectList.Add(next.Data);
                ListAddRest_C(next.NextNext);
            }
            else
            {
                ObjectList.Add(next.Data);
                ListReturn();
            }
        }
        private void ListAddRest_U(CookTime.REST_API_UserListModel.Head head)
        {
            if (head.Next != null)
            {
                ObjectList.Add(head.Data);
                ListAdd_U(head.Next);
            }
            else
            {
                ObjectList.Add(head.Data);
                ListReturn();
            }
        }
        private void ListAddRest_R(CookTime.REST_API_RecipeListModel.Head head)
        {
            if (head.Next != null)
            {
                ObjectList.Add(head.Data);
                ListAdd_R(head.Next);
            }
            else
            {
                ObjectList.Add(head.Data);
                ListReturn();
            }
        }
        private void ListAddRest_C(CookTime.REST_API_CompanyListModel.Head head)
        {
            if (head.Next != null)
            {
                ObjectList.Add(head.Data);
                ListAdd_C(head.Next);
            }
            else
            {
                ObjectList.Add(head.Data);
                ListReturn();
            }
        }
        public void ListReturn()
        {
            ListaObjects.ItemsSource = ObjectList;
           
        }
        public void InitList()
        {
            ObjectList = new ArrayList();
        }
        private void View_Recipe(object sender, EventArgs e)
        {
            if (Reference == 1)
            {
                REST_API_UserModel.User user = (REST_API_UserModel.User)ListaObjects.SelectedItem;
                //Navigation.PushAsync(new ProfileView(user));
            }
            if (Reference == 2)
            {
                Recipe recipe = (Recipe)ListaObjects.SelectedItem;
                Navigation.PushAsync(new ViewRecipe(recipe));
            }
            if (Reference == 3)
            {
                Company company = (Company)ListaObjects.SelectedItem;
                //Navigation.PushAsync(new CompanyProfileView(company));
            }
            
        }

    }
}
