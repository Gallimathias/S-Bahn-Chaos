using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Lang;

namespace SBahnChaosApp.Droid
{
    [Activity(Label = "SBahnChaosApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        public bool isBound { get; set; }
        public BackgroundServiceBinder Binder { get; set; }


        protected override void OnCreate(Bundle bundle)
        {
            isBound = false;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.ActionBar.SetIcon(Android.Resource.Color.Transparent);
            
            LoadApplication(new App());

            //StartService(new Intent(nameof(BackgroundService)));
            StartService(new Intent("de.sbahnchaosapp.BackgroundService"));

        }
    }
}

