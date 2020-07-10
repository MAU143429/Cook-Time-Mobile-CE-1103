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
    public partial class ViewRecipe : ContentPage
    {
        public ViewRecipe()
        {
            InitializeComponent();
        }

        private void DeleteR(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ShowSearch());
            DisplayAlert("COOKTIME", "RECIPE HAS BEEN DELETED", "ACCEPT");
        }

        private void CommentR(object sender, EventArgs e)
        {


            var commentValidate = Comment.Text;
            if (!string.IsNullOrEmpty(commentValidate))
            {
                DisplayAlert("COOKTIME", "YOUR COMMENT HAS BEEN PUBLISHED", "ACCEPT");
                Navigation.PushAsync(new ShowSearch());

            }else
            {
                DisplayAlert("COOKTIME", "ENTER A COMMENT TO CONTINUE ", "ACCEPT");
            }

        }
            
    }
}