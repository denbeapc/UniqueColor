using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ReactiveUI;
using Splat;
using UniqueColor.Core;
using UniqueColor.Core.Interfaces;
using UniqueColor.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace UniqueColor.ViewModels
{
    public class ColorsViewModel : BaseViewModel
    {
        public ColorsViewModel(INavigationService navigationService = null) 
            : base(navigationService ?? Locator.Current.GetService<INavigationService>())
        {
            Colors = new ObservableCollection<Color>();

            this.WhenAnyValue(x => x.EntryText)
                .Subscribe(x =>
                {
                    int tryParseNum = 0;
                    if (int.TryParse(EntryText, out tryParseNum))
                    {
                        ButtonEnabled = tryParseNum <= 20;
                    }
                    else
                    {
                        ButtonEnabled = false;
                    }

                    NumberOfColors = (ButtonEnabled) ? tryParseNum : -1;
                })
                .DisposeWith(SubscriptionDisposables);
        }

        public void GenerateRandomColors()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                ButtonEnabled = false;

                ColorHelper colorHelper = new ColorHelper();
                Random random = new Random();
                Color color;
                bool isDifferent = false;

                for (int count = 0; count < NumberOfColors; count++)
                {
                    if (count != 0)
                    {
                        do
                        {
                            color = GenerateColor(random, Color.White);
                            CheckRGBSimilarity(Color.FromRgb(color.R, color.G, color.B), out isDifferent);
                        } while (!isDifferent);

                        Colors.Add(color);
                    }
                    else
                    {
                        Colors.Add(GenerateColor(random, Color.White));
                    }
                }

                ButtonEnabled = true;
                CanFillLayout = true;
            });
        }

        private void CheckRGBSimilarity(Color newColor, out bool isDifferent)
        {
            long rmean = 0, r = 0, g = 0, b = 0;
            double euclidDistance = 100;
            bool trigger = false;

            foreach (var color in Colors)
            {
                rmean = ((long)(color.R * 255) + (long)(newColor.R * 255)) / 2;
                r = (long)(color.R * 255) - (long)(newColor.R * 255);
                g = (long)(color.G * 255) - (long)(newColor.G * 255);
                b = (long)(color.B * 255) - (long)(newColor.B * 255);
                euclidDistance = Math.Sqrt((((512 + rmean) * r * r) >> 8) + 4 * g * g + (((767 - rmean) * b * b) >> 8));

                if (euclidDistance <= 62)
                {
                    trigger = true;
                    break;
                }
            }

            isDifferent = !trigger;
        }

        private Color GenerateColor(Random random, Color? mix = null)
        {
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

            return Color.FromRgb(red, green, blue);
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

        private bool canFillLayout;
        public bool CanFillLayout
        {
            get { return canFillLayout; }
            set { this.RaiseAndSetIfChanged(ref canFillLayout, value); }
        }
    }
}
