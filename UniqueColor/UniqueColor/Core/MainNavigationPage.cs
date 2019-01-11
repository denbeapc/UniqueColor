using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using UniqueColor.Core.Interfaces;
using Xamarin.Forms;

namespace UniqueColor.Core
{
    public class MainNavigationPage : NavigationPage
    {
        public MainNavigationPage(Page page) : base(page)
        {
            this.BarBackgroundColor = (Color)Application.Current.Resources["PrimaryColor"];
            this.BarTextColor = Color.White;

            Observable.FromEventPattern<EventHandler<NavigationEventArgs>, NavigationEventArgs>(
                    ev => this.Popped += ev,
                    ev => this.Popped -= ev).
                Subscribe(
                    (args) =>
                    {
                        if (args.EventArgs.Page is IDestructible)
                        {
                            ((IDestructible)args.EventArgs.Page).Destroy();
                        }
                    });
        }

        public async Task ResetRootAsync(Page page)
        {
            this.Navigation.InsertPageBefore(page, this.Navigation.NavigationStack.First());
            await this.Navigation.PopToRootAsync(false);
        }

    }
}
