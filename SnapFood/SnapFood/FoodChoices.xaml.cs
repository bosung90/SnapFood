using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SnapFood
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class FoodChoices : Page
    {
        public ObservableCollection<ingredientDesc> items = new ObservableCollection<ingredientDesc>();

        public List<string> recipeQuery = new List<string>();
        
        public FoodChoices()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var ingredients = e.Parameter as List<string>;
            foreach (string i in ingredients)
            {
                ingredientDesc ingredientDescription = new ingredientDesc();
                var isChecked = ingredientDescription.IngredientChecked;
                ingredientDescription.Description = i;
                items.Add(ingredientDescription);
            }
            ingredientListview.ItemsSource = items;
            loadingRing.IsActive = false;
        }

        public void GetRecipeClick(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(GetRecipe), recipeQuery);
        }

        private void IngredientClick(object sender, RoutedEventArgs e)
        {
            if((sender as CheckBox).IsChecked == true)
            {
                recipeQuery.Add((sender as CheckBox).Content.ToString());
            }
            else
            {
                recipeQuery.Remove((sender as CheckBox).Content.ToString());
            }
        }
    }

    public class ingredientDesc : INotifyPropertyChanged
    {
        private bool _ingredientchecked;
        public bool IngredientChecked
        {
            get
            {
                return _ingredientchecked;
            }
            set
            {
                _ingredientchecked = value;
            }
        }

        private string _description;
        public string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
