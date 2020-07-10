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
    public partial class CompanyProfile : ContentPage
    {
        public CompanyProfile()
        {
            InitializeComponent();
        }

        private void Create_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CreateR());

        }
        private void View_List(object sender, EventArgs e)
        {
            Navigation.PushAsync(new CompanyMemberList());

        }
    }
}