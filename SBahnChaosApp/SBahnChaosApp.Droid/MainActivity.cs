using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Lang;
using System.Linq;
using SBahnChaosApp.FileManager;
using Android.Widget;
using Firebase;
using Firebase.Messaging;
using Firebase.Iid;

namespace SBahnChaosApp.Droid
{
    [Activity(Label = "SBahnChaosApp", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
    {
        public bool isBound { get; set; }

        public BackgroundServiceBinder Binder { get; set; }
        public BackgroundService Service { get; set; }
        private BackgroundServiceConnection connection;
        private Intent intent;

        protected override void OnCreate(Bundle bundle)
        {
            isBound = false;
            base.OnCreate(bundle);
            Xamarin.Forms.Forms.Init(this, bundle);
            ActionBar.SetIcon(Android.Resource.Color.Transparent);
            MainFileManager.Initialize();
            LoadApplication(new App());
            intent = new Intent("de.sbahnchaosapp.BackgroundService");
            intent.SetPackage("de.sbahnchaosapp.android");
            //intent = new Intent(Application.Context, typeof(BackgroundService));

            var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:181042758685:android:a92cd76aad2e6bc")
                .SetApiKey("AIzaSyAuLZp-VBJdgl4mwxhyiY8UkKBPZhdFn9Q")
                .SetGcmSenderId("181042758685")
                .SetDatabaseUrl("https://sbahnchaosapp.firebaseio.com")
                .SetStorageBucket("sbahnchaosapp.appspot.com")
                .Build();

            try
            {
                FirebaseApp.InitializeApp(Application.Context, options);
            }
            catch { }

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
                Toast.MakeText(this, "The Service is bound", ToastLength.Long).Show();
            }
            else
            {
                StartService(intent);
            }
        }

        protected override void OnDestroy()
        {
            if (isBound)
            {
                UnbindService(connection);
                Toast.MakeText(this, "The Service is unbound", ToastLength.Long).Show();
            }

            base.OnDestroy();

        }
    }
}

