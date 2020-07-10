using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CompanyProfileView : ContentPage
    {
        public CompanyProfileView()
        {
            InitializeComponent();
        }
        private void View_List(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyMemberList());

        }
    }
}