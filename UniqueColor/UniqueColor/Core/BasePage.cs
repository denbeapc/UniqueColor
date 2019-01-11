using System;
using System.Reactive.Disposables;
using ReactiveUI;
using UniqueColor.Core.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;

namespace UniqueColor.Core
{
    public class BasePage<TViewModel> : ReactiveUI.XamForms.ReactiveContentPage<TViewModel>, IDestructible where TViewModel : class
    {
        protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();

        public BasePage()
        {
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();

            SetupBindings(SubscriptionDisposables);
        }

        protected virtual void SetupBindings(CompositeDisposable compositeDisposable) { }

        public void Destroy()
        {
            SubscriptionDisposables?.Clear();
            System.Diagnostics.Debug.WriteLine("DESTROY CALLED");
        }
    }
}
