using System;
using Android.Content;
using Android.OS;

namespace SBahnChaosApp.Droid
{
    internal class BackgroundServiceConnection : Java.Lang.Object, IServiceConnection
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
                activity.isBound = true;
                this.binder = backgroundBinder;
            }
        }

        public void OnServiceDisconnected(ComponentName name)
        {
            activity.isBound = false;
        }
    }
}