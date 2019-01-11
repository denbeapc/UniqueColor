using System.Collections.Generic;
using System.Reactive.Disposables;
using PropertyChanged;
using ReactiveUI;
using Splat;
using UniqueColor.Core.Interfaces;

namespace UniqueColor.Core
{
    [DoNotNotify]
    public class BaseViewModel : ReactiveObject
    {
        internal INavigationService _navigationService;
        protected readonly CompositeDisposable SubscriptionDisposables = new CompositeDisposable();

        public BaseViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public virtual void RegisterObservables() { }

        public virtual void Initialize() { }

        public ObservableAsPropertyHelper<bool> _isLoading;
        public bool IsLoading
        {
            get
            {
                return _isLoading?.Value ?? false;
            }
        }

        private string title;
        public string Title
        {
            get { return title; }
            set { this.RaiseAndSetIfChanged(ref title, value); }
        }

        private bool initialized;
        public bool Initialized
        {
            get { return initialized; }
            set { this.RaiseAndSetIfChanged(ref initialized, value); }
        }

        public T Resolve<T>(T entry) where T : class
        {
            return entry ?? Locator.Current.GetService<T>();
        }
    }
}
