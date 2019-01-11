using ReactiveUI;
using Xamarin.Forms;

namespace UniqueColor.Core
{
    public class MainPage : MasterDetailPage, IViewFor<MainViewModel>
    {
        public MainPage()
        {
            this.Icon = "menu.png";
        }

        public bool IsPresentedByBackButton { get; set; }

        protected override bool OnBackButtonPressed()
        {
            var isRoot = this.Detail.Navigation.NavigationStack.Count == 1;

            if (!isRoot)
                return base.OnBackButtonPressed();

            if (this.IsPresented && this.IsPresentedByBackButton)
            {
                return false;
            }
            else if (this.IsPresented && !this.IsPresentedByBackButton)
            {
                return base.OnBackButtonPressed();
            }
            else
            {
                this.IsPresentedByBackButton = true;
                this.IsPresented = true;
                return true;
            }
        }

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

