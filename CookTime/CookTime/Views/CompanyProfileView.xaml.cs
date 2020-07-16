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
        public partial class CompanyProfileView : ContentPage
        {
          
            ArrayList CompanyListView;
            public CompanyProfileView()
            {
                InitializeComponent();
                Pull_Search_Request();
            }
            private async void Pull_Search_Request()
            {
                HttpClient client = new HttpClient();
                string url = "http://192.168.100.7:6969/user";
                var result = await client.GetAsync(url);
                var json = result.Content.ReadAsStringAsync().Result;
                CompanyListModel newmodel = CompanyListModel.FromJson(json);
                StartList(newmodel);
            }
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
            public void ListReturn()
            {
                ListaRCPV.ItemsSource = CompanyListView;
                Console.WriteLine(CompanyListView[0]);
            }
            public void InitList()
            {
                CompanyListView = new ArrayList();
            }
            private void View_List(object sender, EventArgs e)
             {
            Navigation.PushAsync(new CompanyMemberList());

            }
            private void Send_Notification(object sender, EventArgs e)
            {
                 DependencyService.Get<iNotification>().CreateNotification("CookTime", "Un usuario nuevo te ha  seguido!");

            }
            private void Rate_Company(object sender, EventArgs e)
            {
                DisplayAlert("COOKTIME", "Rated successfully", "ACCEPT");

            }
            public void View_Recipe(object sender, EventArgs e)
            {
                //Navigation.PushAsync(new ViewRecipe());
            }
            public void Location_Map(object sender, EventArgs e)
            {
                Navigation.PushAsync(new ShowMap());
            }




    }
}