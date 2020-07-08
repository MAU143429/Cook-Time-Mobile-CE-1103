using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        public Search()
        {
            InitializeComponent();
        }

        private void Home_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());

        }
        private void Search_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Search());

        }
        private void Profile_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
        private void Searching(object sender, EventArgs e)
        {
            var filter1 = DishesS.SelectedIndex;
            var filter2 = DurationS.SelectedIndex;
            var filter3 = ServingsS.SelectedIndex;
            var f1 = DishesS.SelectedItem;
            
            var searchValidate = search1.Text;
            Boolean v1, v2, v3;
            v1 = false;
            v2 = false;
            v3 = false;
            if (!string.IsNullOrEmpty(searchValidate))
            {
                DisplayAlert("COOKTIME", "SEARCHING...", "ACCEPT");
                
                if (filter1 >= 0 && filter1 <= 4) { v1 = true; Console.WriteLine(f1); }
                if (filter2 >= 0 && filter2 <= 4) { v2 = true; }
                if (filter3 >= 0 && filter3 <= 4) { v3 = true; }

                if (v1 == true && v2 == true && v3 == true)
                {
                    DisplayAlert("COOKTIME", "TODOS LOS FILTROS HAN SIDO UTILIZADOS", "ACCEPT");
                }
                Navigation.PushAsync(new ShowSearch());

            }
            else
            {
                DisplayAlert("ERROR", "YOU MUST FILL ALL THE BLANKS TO CONTINUE", "ACCEPT");
            

        }

        }
        private void Search_Filter(object sender, EventArgs e)
        {
            var filter1 = DishesS.SelectedIndex;
            if (filter1 == 1)
            {
                DisplayAlert("COOKTIME", "Seleccinaste Lunch", "ACCEPT");
            }
        }

    }
}