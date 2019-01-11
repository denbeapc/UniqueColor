using System;
using Splat;
using UniqueColor.Core;
using UniqueColor.Core.Interfaces;

namespace UniqueColor.ViewModels
{
    public class MenuViewModel : BaseViewModel, IEnableLogger
    {
        public MenuViewModel(INavigationService navigationService = null) : base(navigationService ?? Locator.Current.GetService<INavigationService>())
        {
            Title = "Menu";
        }
    }
}
