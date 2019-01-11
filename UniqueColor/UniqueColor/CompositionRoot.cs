using System;
using ReactiveUI;
using Splat;
using UniqueColor.Core;
using UniqueColor.Core.Interfaces;
using UniqueColor.Core.Services;
using UniqueColor.Pages;
using UniqueColor.ViewModels;
using Xamarin.Forms;

namespace UniqueColor
{
    public abstract class CompositionRoot
    {
        protected readonly Lazy<App> app;
        protected readonly Lazy<INavigationService> navigationService;

        protected CompositionRoot()
        {
            // Create Lazy Instances
            this.app = new Lazy<App>(this.CreateApp);
            this.navigationService = new Lazy<INavigationService>(this.CreateNavigationService);

            // Locators
            Locator.CurrentMutable.RegisterLazySingleton(() => this.navigationService.Value, typeof(INavigationService));
        }

        // Creates
        private App CreateApp() => new App();


        private INavigationService CreateNavigationService()
        {
            var navService = new NavigationService(RxApp.TaskpoolScheduler,
                RxApp.MainThreadScheduler,
                GetNavigation);

            navService.RegisterView(NavConstants.main,
                                    () => CreateMainPage(),
                                    () => new MainViewModel(this.navigationService.Value));

            navService.RegisterView(NavConstants.colors,
                                    () => new ColorsPage(),
                                    () => new ColorsViewModel());

            return navService;
        }


        // Resolvers
        public App ResolveApp() => this.app.Value;

        public INavigationService ResolveNavigationService() => this.navigationService.Value;

        public INavigation GetNavigation()
        {
            return App.Instance.GetNavigation();
        }

        public Page CreatePage(string path)
        {
            return this.navigationService.Value.CreateView(path) as Page;
        }

        public IViewFor CreateMainPage()
        {
            var mainPage = new MainPage();

            return mainPage;
        }
    }
}
