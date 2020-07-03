using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace CookTime
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        int cont = 0;
        protected override void OnAppearing()
        {
            Sumarbtn.Clicked += Sumarbtn_Clicked;
        }

        private void Sumarbtn_Clicked (object sender ,  EventArgs e)
        {
            cont++;
            Sumarbtn.Text = cont.ToString();
        }
    }
}
