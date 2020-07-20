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
    /// This class  show the company account, and allows users to see and share recipes of the company 
    /// @author Mauricio C.
    /// </summary>
    public partial class CompanyProfileView : ContentPage
        {
        /// <summary>
        /// This constructor execute CompanyProfileView partial class and start a new pull search request to the server 
        /// @author Mauricio C.
        /// </summary>
        ArrayList CompanyListView;

            public CompanyProfileView()
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
                if (model.Head.Next != null)
                {
                    CompanyListView.Add(model.Head.Data);
                    ListAdd(model.Head.Next);
                }
                else
                {
                    CompanyListView.Add(model.Head.Data);
                    ListReturn();
                }
            }
        /// <summary>
        /// This method take the data and add it to the CompanyListView
        /// @author Jose A.
        /// </summary>
        private void ListAdd(CookTime.REST_API_CompanyListModel.Next next)
            {
                if (next.NextNext != null)
                {
                    CompanyListView.Add(next.Data);
                    ListAddRest(next.NextNext);
                }
                else
                {
                    CompanyListView.Add(next.Data);
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
                    CompanyListView.Add(head.Data);
                    ListAdd(head.Next);
                }
                else
                {
                    CompanyListView.Add(head.Data);
                    ListReturn();
                }
            }
        /// <summary>
        /// This method display the list
        /// @author Jose A.
        /// </summary>
            public void ListReturn()
                {
                ListaRCPV.ItemsSource = CompanyListView;
                Console.WriteLine(CompanyListView[0]);
                 }
        /// <summary>
        /// This method inicializate ArrayList
        /// @author Jose A.
        /// </summary>
            public void InitList()
            {
                CompanyListView = new ArrayList();
            }
        /// <summary>
        /// This method is used to change the current page to CompanyMemberList page
        /// @author Mauricio C.
        /// </summary>
           private void View_List(object sender, EventArgs e)
             {
            Navigation.PushAsync(new CompanyMemberList());

            }
        /// <summary>
        /// Tthis method sends a notification to notify that there are new followers
        /// @author Mauricio C.
        /// </summary>
        private void Send_Notification(object sender, EventArgs e)
            {
                 DependencyService.Get<iNotification>().CreateNotification("CookTime", "Un usuario nuevo te ha  seguido!");

            }
        /// <summary>
        /// This method allows users to rate a company
        /// @author Mauricio C.
        /// </summary>
        private void Rate_Company(object sender, EventArgs e)
            {
                DisplayAlert("COOKTIME", "Rated successfully", "ACCEPT");

            }
        /// <summary>
        /// This method is used to change the current page to ViewRecipe page
        /// @author Mauricio C.
        /// </summary>
        public void View_Recipe(object sender, EventArgs e)
            {
                //Navigation.PushAsync(new ViewRecipe());
            }
        /// <summary>
        /// This method is used to change the current page to ShowMap page
        /// @author Mauricio C.
        /// </summary>
        public void Location_Map(object sender, EventArgs e)
            {
                Navigation.PushAsync(new ShowMap());
            }




    }
}