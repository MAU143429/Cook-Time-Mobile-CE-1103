using CookTime.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    /// <summary>
    /// This class show the most recently recipes that was created
    /// @author Jose A.
    /// </summary>
    public partial class HomePage : ContentPage
    {

        /// <summary>
        /// This constructor execute Home Page
        /// @author Jose A.
        /// </summary>
        public HomePage()
        {
            
            InitializeComponent();
            UserSelf CurrentUser;

        }
        /// <summary>
        /// This method is used to refresh the home page
        /// @author Mauricio C.
        /// </summary>
        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());

        }
        /// <summary>
        /// This method is used to change the current page to Search page
        /// @author Mauricio C.
        /// </summary>
        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());

        }
        /// <summary>
        /// This method is used to change the current page to Profile1 page
        /// @author Mauricio C.
        /// </summary>
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
        /// <summary>
        /// This method is used to change the current recipe to other 
        /// @author Mauricio C.
        /// </summary>

        private void Next(object sender, EventArgs e)
        {        
         var Rating = rating.SelectedIndex;
         if (Rating >= 0 && Rating <= 4) {
            DependencyService.Get<iNotification>().CreateNotification("CookTime", "Un usuario ha calificado tu receta!");
            var urlimg = "https://www.recetasconpollo.org/wp-content/uploads/2019/12/filete-pechuga-pollo-plancha--512x341.jpg";
            mainimage.Source = urlimg;
           // rating.ItemsSource CollectionChanged(Reset);
         }
         else
         {
           var urlimg = "https://www.recetasconpollo.org/wp-content/uploads/2019/12/filete-pechuga-pollo-plancha--512x341.jpg";
           mainimage.Source = urlimg;
         }


        }
    }
}