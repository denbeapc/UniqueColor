using System;
using ReactiveUI;

namespace UniqueColor.Core.Interfaces
{
    public interface INavigationService
    {
        void RegisterView(string path, Func<IViewFor> viewCreation, Func<BaseViewModel> viewModelCreation);

        IViewFor CreateView(String path);
    }
}
