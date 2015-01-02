using Android.App;

namespace Astrid.Android
{
    public class AndroidApplicationConfig
    {
        public AndroidApplicationConfig(Activity activity)
        {
            Activity = activity;
        }

        public Activity Activity { get; set; }
    }
}