using CookTime.REST_API_CompanyListModel;
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

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class allows users to manage the company account, create recipes and share recipes with other company members
    /// @author Jose A.
    /// </summary>
    public partial class CompanyProfile : ContentPage
    {
        /// <summary>
        /// This constructor execute CompanyProfile partial class and start a new pull search request to the server 
        /// @author Jose A.
        /// </summary>
        ArrayList CompanyList;
        public CompanyProfile()
        {
            InitializeComponent();
            Pull_Search_Request();
        }

        /// <summary>
        /// This method create a new HTTP client and execute async method with the server to get the company data 
        /// @author Jose A.
        /// </summary>
        private async void Pull_Search_Request()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/user";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            CompanyListModel newmodel = CompanyListModel.FromJson(json);
            StartList(newmodel);
        }
        /// <summary>
        /// This method take the first element and add it
        /// @author Jose A.
        /// </summary>
        public void StartList(CompanyListModel model)
        {
            
            InitList();
            if(model.Head.Next != null) {
                CompanyList.Add(model.Head.Data);
                ListAdd(model.Head.Next);
            }
            else
            {
                CompanyList.Add(model.Head.Data);
                ListReturn();   
            }
        }
        /// <summary>
        /// This method take the data and add it to the CompanyList 
        /// @author Jose A.
        /// </summary>
        private void ListAdd(CookTime.REST_API_CompanyListModel.Next next)
        {
            if (next.NextNext != null)
            {
                CompanyList.Add(next.Data);
                ListAddRest(next.NextNext);
            }
            else
            {
                CompanyList.Add(next.Data);
                ListReturn();
            }
        }
        /// <summary>
        /// This method verify the current data is the last element in json file, if it is the last element the method add it and return the finally list
        /// @author Jose A.
        /// </summary>
        private void ListAddRest(CookTime.REST_API_CompanyListModel.Head head)
        {
            if (head.Next != null)
            {
                CompanyList.Add(head.Data);
                ListAdd(head.Next);
            }
            else
            {
                CompanyList.Add(head.Data);
                ListReturn();
            }
        }
        /// <summary>
        /// This method display the list
        /// @author Jose A.
        /// </summary>
        public void ListReturn()
        {
            ListaRCP.ItemsSource = CompanyList;
            Console.WriteLine(CompanyList[0]);
        }
        /// <summary>
        /// This method inicializate ArrayList
        /// @author Jose A.
        /// </summary>
        public void InitList()
        {
            CompanyList = new ArrayList();
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
        /// This method is used to change the current page to Company Member List page
        /// @author Mauricio C.
        /// </summary>
        private void View_List(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyMemberList());

        }
        /// <summary>
        /// This method is used to change the current page to Change Photo page
        /// @author Mauricio C.
        /// </summary>
        private void Change_Photo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePhoto());

        }
        /// <summary>
        /// This method is used to change the current page to Change Password page
        /// @author Mauricio C.
        /// </summary>
        private void Change_Pass(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());

        }
        /// <summary>
        /// This method is used to change the current page to AddMember page
        /// @author Mauricio C.
        /// </summary>
        private void Add_Member(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());

        }
    }
}