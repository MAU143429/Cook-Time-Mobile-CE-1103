using CookTime.REST_API_CompanyModel;
using CookTime.REST_API_UserListModel;
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
    /// This class allows users to see a company member list. In this page the user can see all the members in this company and the profile view for each one
    /// @author Mauricio C.
    /// </summary>
    public partial class CompanyMemberList : ContentPage
    {

        /// <summary>
        /// This constructor execute CompanyMemberList partial class 
        /// @author Mauricio C.
        /// </summary>
        ArrayList ObjectList;
        public static Company company2;
        public CompanyMemberList(Company company)
        {
            company2 = company;
            InitializeComponent();
            Pull_Search_Request_U();
        }

        private async void Pull_Search_Request_U()
        {
            HttpClient client = new HttpClient();
            string url = "http://" + LoginPage.ip + ":6969/"+company2.Email+"/memberList";
            var result = await client.GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;
            UserListModel newmodel = UserListModel.FromJson(json);
            StartList_U(newmodel);
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
        public void ListReturn()
        {
            ListaM.ItemsSource = ObjectList;

        }
        public void InitList()
        {
            ObjectList = new ArrayList();
        }
    }
}