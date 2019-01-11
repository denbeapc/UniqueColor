using System;
using System.Collections.Generic;
using System.Reactive.Concurrency;
using ReactiveUI;
using UniqueColor.Core.Actions;
using UniqueColor.Core.Interfaces;
using Xamarin.Forms;

namespace UniqueColor.Core.Services
{
    public class NavigationService : INavigationService
    {
        private readonly IScheduler backgroundScheduler;
        private readonly IScheduler mainScheduler;
        private Func<INavigation> GetNavigation { get; set; }

        private static readonly Dictionary<string, KeyValuePair<Func<IViewFor>, Func<BaseViewModel>>> _viewRegistry = new Dictionary<string, KeyValuePair<Func<IViewFor>, Func<BaseViewModel>>>();

        public NavigationService(
            IScheduler backgroundScheduler,
            IScheduler mainScheduler,
            Func<INavigation> getNavigation
        )
        {
            this.backgroundScheduler = backgroundScheduler;
            this.mainScheduler = mainScheduler;
            this.GetNavigation = getNavigation;
        }

        public void RegisterView(string path, Func<IViewFor> viewCreation, Func<BaseViewModel> viewModelCreation)
        {
            _viewRegistry[path] = new KeyValuePair<Func<IViewFor>, Func<BaseViewModel>>(viewCreation, viewModelCreation);
        }

        public IViewFor CreateView(NavigateAction action)
        {
            IViewFor page;
            var registeredView = _viewRegistry[action.Path];

            page = registeredView.Key();

            var viewModel = registeredView.Value();
            viewModel?.Initialize();
            page.ViewModel = viewModel;

            return page;
        }

        public IViewFor CreateView(string path)
        {
            return CreateView(new NavigateAction(path));
        }
    }
}
