using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading;
using ReactiveUI;
using UniqueColor.Core;
using UniqueColor.Helpers;
using UniqueColor.ViewModels;
using Xamarin.Forms;

namespace UniqueColor.Pages
{
    public partial class ColorsPage : BasePage<ColorsViewModel>
    {
        public ColorsPage()
        {
            InitializeComponent();
        }

        protected override void SetupBindings(CompositeDisposable compositeDisposable)
        {
            base.SetupBindings(compositeDisposable);

            this.WhenAnyValue(x => x.ViewModel.CanFillLayout)
                .Where(x => x)
                .Subscribe(x =>
                {
                    ColorHelper colorHelper = new ColorHelper();
                    FillColorLayout(colorHelper);
                })
                .DisposeWith(compositeDisposable);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.NumberOfColors = 7;
            ViewModel.EntryText = ViewModel.NumberOfColors.ToString();
        }

        void Handle_GeneratePressed(object sender, EventArgs e)
        {
            if (sender == null || e == null)
                return;

            ViewModel.Colors.Clear();
            colorsLayout.Children.Clear();
            if (ViewModel.NumberOfColors != 0)
            {
                ViewModel.GenerateRandomColors();
            }
        }

        void Handle_ClearPressed(object sender, EventArgs e)
        {
            if (sender == null || e == null)
                return;

            colorsLayout.Children.Clear();
        }

        private void FillColorLayout(ColorHelper colorHelper)
        {
            foreach (var color in ViewModel.Colors)
            {
                StackLayout parentContainer = new StackLayout
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    HeightRequest = 64,
                    BackgroundColor = color
                };

                Label label = new Label
                {
                    Text = colorHelper.GetHexString(color),
                    VerticalOptions = LayoutOptions.Center,
                    TextColor = Color.White,
                    Margin= new Thickness(10, 10, 0, 0),
                    FontSize = 16
                };

                parentContainer.Children.Add(label);
                colorsLayout.Children.Add(parentContainer);
            }

            ViewModel.CanFillLayout = false;
        }
    }
}
