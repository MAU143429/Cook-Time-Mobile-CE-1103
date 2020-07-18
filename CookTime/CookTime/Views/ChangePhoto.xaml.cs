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
    /// This class allows users to change their profile photo 
    /// @author Mauricio C.
    /// </summary>
    public partial class ChangePhoto : ContentPage
    {
        /// <summary>
        /// This constructor execute ChangePhoto partial class
        /// @author Mauricio C.
        /// </summary>
        public ChangePhoto()
        {
            InitializeComponent();
        }
        /// <summary>
        /// This method send a new image url to the server. This photo will be updated when the user go back to their profile
        /// @author Mauricio C.
        /// </summary>
        private void Changephoto(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }

    }
}