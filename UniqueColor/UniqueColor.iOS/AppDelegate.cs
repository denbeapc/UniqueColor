using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Splat;
using UIKit;

namespace UniqueColor.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        UniqueColor.App app;

        public override bool FinishedLaunching(UIApplication application, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            var compositionRoot = new iOSCompositionRoot();
            app = compositionRoot.ResolveApp();
            app.ScreenSize = new Xamarin.Forms.Size(UIScreen.MainScreen.Bounds.Width, UIScreen.MainScreen.Bounds.Height);

            var splatRegistrar = new iOSSplatRegistrar();
            splatRegistrar.Register(Locator.CurrentMutable, compositionRoot);

            app.Initialize(compositionRoot);

            LoadApplication(app);

            return base.FinishedLaunching(application, options);
        }
    }
}
