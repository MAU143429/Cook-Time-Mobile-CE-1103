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
    public partial class CompanyProfile : ContentPage
    {
        //List<Object> UserList;
        //List<Object> ShownList;
        ArrayList CompanyList;
        public CompanyProfile()
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
        public void ListReturn()
        {
            ListaRCP.ItemsSource = CompanyList;
            Console.WriteLine(CompanyList[0]);
        }
        public void InitList()
        {
            CompanyList = new ArrayList();
        }

        private void Create_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateR());

        }
        private void View_List(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyMemberList());

        }
        private void Change_Photo(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePhoto());

        }
        private void Change_Pass(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ChangePassword());

        }
    }
}