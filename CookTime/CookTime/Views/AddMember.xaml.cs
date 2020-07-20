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
    public partial class AddMember : ContentPage
    {
        public AddMember()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method is used to change the current page to AddMember page
        /// @author Mauricio C.
        /// </summary>
        private void Add_Member(object sender, EventArgs e)
        {
            if(newmember != null)
            {
                DisplayAlert("ERROR", "Member added successfully", "ACCEPT");
                Navigation.PushAsync(new CompanyProfile());
            }
            else
            {
                DisplayAlert("ERROR", "The user email doesn't exist", "ACCEPT");
            }

            DisplayAlert("ERROR", "YOU MUST FILL ALL THE BLANKS TO CONTINUE", "ACCEPT");


        }
    }
}