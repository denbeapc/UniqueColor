using System.Threading.Tasks;
using UniqueColor.Pages;
using UniqueColor.Themes;
using Xamarin.Forms;

namespace UniqueColor.Core
{
    public class AppStartPage : ContentPage
    {
        public AppStartPage()
        {
            BackgroundColor = Color.FromHex("#4286F4");
            var indicator = new ActivityIndicator
            {
                VerticalOptions = LayoutOptions.End,
                IsRunning = true,
                IsVisible = true,
                Color = Color.White,
                Margin = new Thickness(0, 60)
            };

            var grid = new Grid
            {
                BackgroundColor = Color.FromHex("#4286F4")
            };

            grid.Children.Add(indicator);

            Content = grid;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (Application.Current.Resources == null)
            {
                Application.Current.Resources = new ResourceDictionary();
            }
            Application.Current.Resources.MergedWith = typeof(MainTheme);

            //
            // push new root
            //
            await Task.Run(() => App.Instance.SetMainPage());
        }
    }
}
