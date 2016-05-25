using Android.App;
using Android.Content;
using Android.Runtime;
using System;
using System.Threading;

namespace SBahnChaosApp.Droid
{
    [Service]
    class BackgroundService : IntentService
    {
        protected override void OnHandleIntent(Intent intent)
        {
            throw new NotImplementedException();
        }

        public override void OnCreate()
        {
            base.OnCreate();
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
        }

        [return: GeneratedEnum]
        public override StartCommandResult OnStartCommand(Intent intent, [GeneratedEnum] StartCommandFlags flags, int startId)
        {
            return StartCommandResult.Sticky;
        }

        public void DoWork()
        {
            var t = new Thread(() => {

                Thread.Sleep(30000);

                string a = DateTime.Now.ToString("HH:mm");
                System.Diagnostics.Debug.WriteLine(a);

            });

            t.Start();
        }
    }
}