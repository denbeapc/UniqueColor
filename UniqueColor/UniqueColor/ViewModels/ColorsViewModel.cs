using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using ReactiveUI;
using Splat;
using UniqueColor.Core;
using UniqueColor.Core.Interfaces;
using Xamarin.Forms;

namespace UniqueColor.ViewModels
{
    public class ColorsViewModel : BaseViewModel
    {
        public ColorsViewModel(INavigationService navigationService = null) 
            : base(navigationService ?? Locator.Current.GetService<INavigationService>())
        {
            LoadCommand = ReactiveCommand.Create(() =>
            {
                NumberOfColors = 7;
                EntryText = NumberOfColors.ToString();
            });
            LoadCommand.ObserveOn(RxApp.MainThreadScheduler);
            LoadCommand.IsExecuting.StartWith(false).ToProperty(this, x => x.IsLoading, out _isLoading);
            LoadCommand.DisposeWith(SubscriptionDisposables);
        }

        public void GenerateRandomColor(Color? mix = null)
        {
            Random random = new Random();
            int red = random.Next(80, 256);
            int green = random.Next(80, 256);
            int blue = random.Next(80, 256);

            // mix the color
            if (mix != null)
            {
                red = (int)((red + ((Color)mix).R) / 2);
                green = (int)((green + ((Color)mix).G) / 2);
                blue = (int)((blue + ((Color)mix).B) / 2);
            }

            Colors.Add(Color.FromRgb(red, green, blue));
        }

        public ReactiveCommand<Unit, Unit> LoadCommand { get; private set; }

        public ObservableCollection<Color> Colors { get; set; }

        private string entryText;
        public string EntryText
        {
            get { return entryText; }
            set { this.RaiseAndSetIfChanged(ref entryText, value); }
        }

        private int numberOfColors;
        public int NumberOfColors
        {
            get { return numberOfColors; }
            set { this.RaiseAndSetIfChanged(ref numberOfColors, value); }
        }

        private bool buttonEnabled;
        public bool ButtonEnabled
        {
            get { return buttonEnabled; }
            set { this.RaiseAndSetIfChanged(ref buttonEnabled, value); }
        }

        public bool generateIsLoading;
        public bool GenerateIsLoading
        {
            get { return generateIsLoading; }
            set { this.RaiseAndSetIfChanged(ref generateIsLoading, value); }
        }
    }
}
