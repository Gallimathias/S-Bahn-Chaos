using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Lang;
using System.Linq;
using SBahnChaosApp.FileManager;
using Android.Widget;

namespace SBahnChaosApp.Droid
{
    [Activity(Label = "SBahnChaosApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        public bool isBound { get; set; }
        public BackgroundServiceBinder Binder { get; set; }
        private BackgroundServiceConnection connection;
        private Intent intent;

        protected override void OnCreate(Bundle bundle)
        {
            isBound = false;
            base.OnCreate(bundle);
            global::Xamarin.Forms.Forms.Init(this, bundle);
            this.ActionBar.SetIcon(Android.Resource.Color.Transparent);
            MainFileManager.Initialize();
            LoadApplication(new App());
            intent = new Intent("de.sbahnchaosapp.BackgroundService");

        }

        protected override void OnStart()
        {
            base.OnStart();

            var manager = (ActivityManager)GetSystemService(ActivityService);
            var list = manager.GetRunningServices(int.MaxValue);

            if (list.Any(s => s.Service.PackageName == PackageName))
            {
                connection = new BackgroundServiceConnection(this);
                BindService(intent, connection, Bind.AutoCreate);
                isBound = true;
                Toast.MakeText(this, "the Service bound", ToastLength.Long).Show();
            }
            else
            {
                StartService(intent);
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (isBound)
            {
                UnbindService(connection);
                isBound = false;
            }
        }
    }
}

