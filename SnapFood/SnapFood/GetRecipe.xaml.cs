using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SnapFood
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GetRecipe : Page
    {
        string apiKey = "1b764d8532c16ec829c038b8e718fe25";
        public ObservableCollection<buttonDesc> items = new ObservableCollection<buttonDesc>();

        public GetRecipe()
        {
            this.InitializeComponent();
        }


        public async void GetRecipeClick(object sender, RoutedEventArgs e)
        {
            string ingredients = textBox.Text;
            var url = "http://food2fork.com/api/search?key=" + apiKey + "&q=" + ingredients;

            HttpClient httpClient = new HttpClient();
            string responseBodyAsText;
            
            
            HttpResponseMessage response = await httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            responseBodyAsText = await response.Content.ReadAsStringAsync();
            var result = responseBodyAsText;

            // parse json
            RecipeCollection rc = JsonConvert.DeserializeObject<RecipeCollection>(result);


            // for each recipe, create a button that contains photo & recipe name
            foreach(Recipe r in rc.recipes)
            {
                var pic_url = r.image_url;
                var name = r.title;
                var source_url = r.source_url;

                buttonDesc item = new buttonDesc();
                item.Image = new BitmapImage(new Uri(pic_url));
                item.Image.DecodePixelHeight = 100;
                item.Image.DecodePixelWidth = 100;
                item.Description = name;
                item.Uri = source_url;
                items.Add(item);

            }
            myListview.ItemsSource = items;
        }

        private async void GetRecipeClickTwo(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://hotmail.com"));
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri("http://hotmail.com"));
           
        }

        private async void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            string uriLink = (e.ClickedItem as buttonDesc).Uri;
            await Windows.System.Launcher.LaunchUriAsync(new Uri(uriLink));
        }
    }

    public class buttonDesc : INotifyPropertyChanged
    {
        private BitmapImage _image;
        public BitmapImage Image
        {
            get { return _image; }


            set
            {
                _image = value;
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }


            set
            {
                _description = value;
                
            }
        }

        public string Uri;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
