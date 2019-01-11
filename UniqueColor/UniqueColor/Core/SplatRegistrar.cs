using System;
using Splat;

namespace UniqueColor.Core
{
    public abstract class SplatRegistrar
    {
        public SplatRegistrar()
        {
                
        }

        public void Register(IMutableDependencyResolver splatLocator, CompositionRoot compositionRoot)
        {
            this.RegisterViews(splatLocator);
            this.RegisterPlatformComponents(splatLocator, compositionRoot);
        }

        private void RegisterViews(IMutableDependencyResolver splatLocator)
        {

        }

        protected abstract void RegisterPlatformComponents(IMutableDependencyResolver splatLocator, CompositionRoot compositionRoot);

    }
}
