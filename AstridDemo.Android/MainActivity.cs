using Android.App;
using Android.OS;
using Android.Content.PM;
using Android.Views;
using Astrid.Android;

namespace AstridDemo.Android
{
    [Activity(Label = "AstridDemo.Android", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = ScreenOrientation.Landscape,
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden | ConfigChanges.ScreenSize)]
    public class MainActivity : Activity
    {
        private AndroidApplication _application;
        private DemoGame _game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            RequestWindowFeature(WindowFeatures.NoTitle);
            Window.SetFlags(WindowManagerFlags.Fullscreen, WindowManagerFlags.Fullscreen);

            var config = new AndroidApplicationConfig(this);
            _application = new AndroidApplication(config);
            _game = new DemoGame(_application);
            _application.Run(_game);
            SetContentView(_application.View);
        }

        protected override void OnDestroy()
        {
            _application.Dispose();
            base.OnDestroy();
        }

        protected override void OnPause()
        {
            _application.Pause();
            base.OnPause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _application.Resume();
        }
    }
}

