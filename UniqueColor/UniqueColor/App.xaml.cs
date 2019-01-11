using System;
using UniqueColor.Core;
using UniqueColor.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace UniqueColor
{
    public partial class App : Application
    {
        public static App Instance => Application.Current as App;
        public Size ScreenSize { get; set; }

        public CompositionRoot _compositionRoot;
        private Action _started;

        public App()
        {
            InitializeComponent();
        }

        public void Initialize(CompositionRoot compositionRoot, Action started = null)
        {
            _compositionRoot = compositionRoot;
            _started = started;

            MainPage = new AppStartPage();
        }

        public void SetMainPage()
        {
            SetMainPage(_compositionRoot.CreatePage(NavConstants.colors));
        }

        private void SetMainPage(Page page)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                this.MainPage = page;
                this._started?.Invoke();
            });
        }

        public INavigation GetNavigation()
        {
            if (MainPage is MasterDetailPage)
            {
                return ((MasterDetailPage)MainPage).Detail.Navigation;
            }
            else
            {
                return MainPage.Navigation;
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
