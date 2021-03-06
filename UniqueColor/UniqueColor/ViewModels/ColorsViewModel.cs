﻿using System;
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
                        ButtonEnabled = tryParseNum <= 50;
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
                CancelGenerate = false;
                ButtonEnabled = false;

                ColorHelper colorHelper = new ColorHelper();
                Random random = new Random();
                Color color = Color.White;
                bool isDifferent = false;

                for (int count = 0; count < NumberOfColors; count++)
                {
                    if (count != 0)
                    {
                        do
                        {
                            if (CancelGenerate)
                                break;

                            color = GenerateColor(random);
                            CheckRGBSimilarity(Color.FromRgb(color.R, color.G, color.B), out isDifferent);

                        } while (!isDifferent);

                        if (CancelGenerate)
                            break;

                        Colors.Add(color);
                    }
                    else
                    {
                        Colors.Add(GenerateColor(random));
                    }
                }

                ButtonEnabled = true;
                CanFillLayout = !CancelGenerate;
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

                if (euclidDistance <= 95)
                {
                    trigger = true;
                    break;
                }
            }

            isDifferent = !trigger;
        }

        private Color GenerateColor(Random random)
        {
            int red = random.Next(30, 256);
            int green = random.Next(30, 256);
            int blue = random.Next(30, 256);

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

        private bool generateFailed;
        public bool CancelGenerate
        {
            get { return generateFailed; }
            set { this.RaiseAndSetIfChanged(ref generateFailed, value); }
        }
    }
}
