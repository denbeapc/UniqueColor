using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace UniqueColor
{
    public partial class MainPage : ContentPage
    {
        public List<string> Colors { get; set; }
        public int NumberOfColors { get; set; }

        public MainPage()
        {
            InitializeComponent();

            NumberOfColors = 7;
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        void Handle_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender == null || e == null)
            {
                generateButton.IsEnabled = false;
                return;
            }

            if (string.IsNullOrEmpty(e.NewTextValue))
            {
                generateButton.IsEnabled = false;
                return;
            }

            int num;
            if (int.TryParse(e.NewTextValue, out num))
            {
                NumberOfColors = num;
                if (NumberOfColors > 145 || NumberOfColors < 0)
                {
                    generateButton.IsEnabled = false;
                    entry.TextColor = Color.Red;
                }
                else
                {
                    generateButton.IsEnabled = true;
                    entry.TextColor = Color.Default;
                }
            }
            else
            {
                generateButton.IsEnabled = false;
                return;
            }
        }

        void Handle_GeneratePressed(object sender, EventArgs e)
        {
            if (sender == null || e == null)
                return;

            colorsLayout.Children.Clear();
            if (NumberOfColors != 0)
            {
                UniqueColorGenerator uniqueColor = new UniqueColorGenerator(NumberOfColors);
                Colors = uniqueColor.GetColors();
                FillColorLayout();
            }
        }

        void Handle_ClearPressed(object sender, EventArgs e)
        {
            if (sender == null || e == null)
                return;

            colorsLayout.Children.Clear();
        }

        private void FillColorLayout()
        {
            foreach (var color in Colors)
            {
                StackLayout parentContainer = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 64,
                    BackgroundColor = Color.FromHex(color)
                };

                Label label = new Label
                {
                    Text = color,
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.FromHex("#2d2d2d"),
                    Margin= new Thickness(7, 7, 0, 0),
                    FontSize = 16
                };

                parentContainer.Children.Add(label);
                colorsLayout.Children.Add(parentContainer);
            }
        }
    }
}
