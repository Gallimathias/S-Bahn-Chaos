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
        bool isConfiChange = false;
        public BackgroundServiceBinder Binder { get; set; }
        BackgroundServiceConnection serviceConnection;


        protected override void OnCreate(Bundle bundle)
        {
            isBound = false;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.ActionBar.SetIcon(Android.Resource.Color.Transparent);
            
            LoadApplication(new App());

            //StartService(new Intent(nameof(BackgroundService)));
            StartService(new Intent("de.sbahnchaosapp.BackgroundService"));

            //serviceConnection = LastNonConfigurationInstance as BackgroundServiceConnection;

            //if (serviceConnection != null)
            //    Binder = serviceConnection.Binder;
        }

        //protected override void OnStart()
        //{
        //    var backgroundServiceIntent = new Intent(nameof(BackgroundService));
        //    serviceConnection = new BackgroundServiceConnection(this);
        //    BindService(backgroundServiceIntent, serviceConnection, Bind.AutoCreate);
        //}

        //protected override void OnDestroy()
        //{
        //    base.OnDestroy();

        //    if(!isConfiChange)
        //        if (isBound)
        //        {
        //            UnbindService(serviceConnection);
        //            isBound = false;
        //        }
        //}

        //public override Object OnRetainNonConfigurationInstance()
        //{
        //    base.OnRetainNonConfigurationInstance();
        //    isConfiChange = true;
        //    return serviceConnection;
        //}
    }
}

