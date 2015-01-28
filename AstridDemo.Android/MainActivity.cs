using Android.App;
using Android.OS;
using Android.Content.PM;
using Astrid.Android;

namespace AstridDemo.Android
{
    [Activity(Label = "AstridDemo.Android", MainLauncher = true, Icon = "@drawable/icon",
        ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.KeyboardHidden 
#if __ANDROID_11__
		,HardwareAccelerated=false
#endif
)]
    public class MainActivity : Activity, IPlatformService
    {
        private AndroidApplication _application;
        private DemoGame _game;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var config = new AndroidApplicationConfig(this);
            _application = new AndroidApplication(config);
            _game = new DemoGame(_application, this);
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
            base.OnPause();
            _application.Pause();
        }

        protected override void OnResume()
        {
            base.OnResume();
            _application.Resume();
        }

        public void OpenUrl(string url)
        {
            throw new System.NotImplementedException();
        }
    }
}

