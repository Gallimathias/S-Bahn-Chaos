using Android.Content;
using Android.OS;
using Java.Lang;

namespace SBahnChaosApp.Droid
{
    public class BackgroundServiceConnection : Object, IServiceConnection
    {
        MainActivity activity;
        BackgroundServiceBinder binder;

        public BackgroundServiceBinder Binder { get { return binder; } }

        public BackgroundServiceConnection(MainActivity activity)
        {
            this.activity = activity;
        }

        public void OnServiceConnected(ComponentName name, IBinder service)
        {
            var backgroundBinder = service as BackgroundServiceBinder;

            if (backgroundBinder != null)
            {
                activity.Binder = backgroundBinder;
                activity.Service = backgroundBinder.GetService();
                activity.isBound = true;
                binder = backgroundBinder;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            activity.isBound = false;
        }
    }
}