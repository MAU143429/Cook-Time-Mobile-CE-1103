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
    /// This class allow the owner of company to set a location for the company
    /// @author Mauricio C.
    /// </summary>
    public partial class SetLocation : ContentPage
    {
        /// <summary>
        /// This constructor execute SetLocation partial class 
        /// @author Mauricio C.
        /// </summary>
        public SetLocation()
        {
            InitializeComponent();
        }
    }
}