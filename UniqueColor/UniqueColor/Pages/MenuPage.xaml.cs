using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using UniqueColor.Core;
using UniqueColor.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace UniqueColor.Pages
{
    public partial class MenuPage : BasePage<MenuViewModel>
    {
        public MenuPage()
        {
            InitializeComponent();

            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        protected override void SetupBindings(CompositeDisposable compositeDisposable)
        {
            base.SetupBindings(compositeDisposable);

            this.Title = ViewModel.Title;
        }
    }
}
