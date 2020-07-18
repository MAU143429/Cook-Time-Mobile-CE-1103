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

    /// <summary>
    /// This class allows  users to change their password
    /// @author Mauricio C.
    /// </summary>
    public partial class ChangePassword : ContentPage
    {
        /// <summary>
        /// This constructor execute ChangePassword partial class
        /// @author Mauricio C.
        /// </summary>
        public ChangePassword()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method send a new password to the server
        /// @author Mauricio C.
        /// </summary>
        private void ChangePass(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Profile1());

        }
    }
}