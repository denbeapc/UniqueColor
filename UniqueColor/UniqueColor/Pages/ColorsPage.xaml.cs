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

            //this.WhenAnyValue(x => x.ViewModel)
                //.Where(x => x == null || !x.Initialized)
                //.SelectMany(x => x.LoadCommand.Execute())
                //.Subscribe()
                //.DisposeWith(compositeDisposable);

            this.Bind(this.ViewModel, x => x.GenerateIsLoading, x => x.generateColorsIndicator.IsRunning)
                .DisposeWith(compositeDisposable);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ViewModel.LoadCommand.Execute();
        }

        void Handle_GeneratePressed(object sender, EventArgs e)
        {
            if (sender == null || e == null)
                return;

            ViewModel.ButtonEnabled = false;
            ViewModel.GenerateIsLoading = true;

            ViewModel.Colors.Clear();
            colorsLayout.Children.Clear();
            if (ViewModel.NumberOfColors != 0)
            {
                for (int count = 0; count < ViewModel.NumberOfColors; count++)
                {
                    ViewModel.GenerateRandomColor(Color.White);
                    Thread.Sleep(50);
                }

                ColorHelper colorHelper = new ColorHelper();
                FillColorLayout(colorHelper);
            }

            ViewModel.GenerateIsLoading = false;
            ViewModel.ButtonEnabled = true;
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
