using System;
using UniqueColor.Core.Interfaces;

namespace UniqueColor.Core
{
    public class MainViewModel : BaseViewModel
    {
        public MainViewModel(INavigationService navigationService) : base(navigationService)
        {
            Console.WriteLine("in base");
        }
    }
}
