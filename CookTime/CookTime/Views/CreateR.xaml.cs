using CookTime.REST_API_Recipe;
using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CookTime.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateR : ContentPage
    {
        public CreateR()
        {
            InitializeComponent();
            
        }
        private void Share_Clicked(object sender, EventArgs e)
        {
            Share_Recipe();
            DisplayAlert("Upload Successfull!", "Your recipe was uploaded successfully! ^^ ", "OK");
            Navigation.PushAsync(new Profile1());

        }
        private async void Share_Recipe()
        {

            var dish = Dishes.SelectedItem;
            var duration = Duration.SelectedItem;
            var time = Time.SelectedItem;
            var diets = Diet.SelectedItem;
            var date = DateTime.Now;

            Recipe recipe = new Recipe();
            recipe.Author = "Automatic";
            recipe.TypeOfDish = dish.ToString();
            recipe.Servings = Int32.Parse(servings.Text);
            recipe.Duration = duration.ToString();
            recipe.Time = time.ToString();
            recipe.Difficulty = Int32.Parse(Difficulty.Text);
            recipe.Diet = diets.ToString();
            recipe.Ingredients = Ingredients.Text;
            recipe.Steps = Steps.Text;
            recipe.Price = Int32.Parse(Price.Text);
            recipe.ImageURL = imageurl.Text;
            recipe.Date = DateTime.Now.ToString("MM/dd/yyyy HH:mm:ss");
            recipe.Rating = 0;

            HttpClient client = new HttpClient();
            string url = "http://192.168.0.17:6969/test";
            String jsonNewRecipe = JsonConvert.SerializeObject(recipe);
            var datasent = new StringContent(jsonNewRecipe);
            datasent.Headers.ContentType.MediaType = "application/json";
            /*var result = */
            await client.PostAsync(url, datasent);
            //var json = result.Content.ReadAsStringAsync().Result;
            //await DisplayAlert("Result", json, "ok");
        }

        /*
        public async void loadIMG()
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("no upload", "picking a photo is not supported", "ok");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if (file == null)
                return;

            Image1.Source = ImageSource.FromStream(() => file.GetStream());

            return Image1;
        }
        private static string ImageToBase64(Image image)
        {
            var imageStream = new MemoryStream();
            try
            {
                image.Save(imageStream, System.Drawing.Imaging.ImageFormat.Bmp);
                imageStream.Position = 0;
                var imageBytes = imageStream.ToArray();
                var ImageBase64 = Convert.ToBase64String(imageBytes);
                return ImageBase64;
            }
            catch (Exception ex)
            {
                return "Error converting image to base64!";
            }
            finally
            {
                imageStream.Dispose;
            }
        }*/
    }
}