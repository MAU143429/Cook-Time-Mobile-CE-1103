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
    /// <summary>
    /// This class allows to create a page that contains a developers info.
    /// </summary>

    public partial class AboutPage : ContentPage
    {

        /// <summary>
        /// This constructor execute About Page partial class
        /// </summary>
        public AboutPage()
        {
            InitializeComponent();
        }
    }
}