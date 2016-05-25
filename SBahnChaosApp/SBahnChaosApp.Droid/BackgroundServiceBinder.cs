using Android.OS;

namespace SBahnChaosApp.Droid
{
    public class BackgroundServiceBinder : Binder
    {
        BackgroundService service;

        public BackgroundServiceBinder (BackgroundService service)
        {
            this.service = service;
        }

        public BackgroundService GetService()
        {
            return service;
        }
    }
}