using ReactiveUI;
using Xamarin.Forms;

namespace UniqueColor.Core
{
    public class MainPage : ContentPage, IViewFor<MainViewModel>
    {
        public MainPage()
        {
        }

        public bool IsPresentedByBackButton { get; set; }

        public MainViewModel ViewModel
        {
            get
            {
                return this.BindingContext as MainViewModel;
            }

            set
            {
                this.BindingContext = value;
            }
        }

        object IViewFor.ViewModel
        {
            get
            {
                return this.BindingContext;
            }

            set
            {
                this.BindingContext = value;
            }
        }
    }
}

