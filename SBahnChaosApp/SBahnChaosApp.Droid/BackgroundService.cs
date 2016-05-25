using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using System;
using System.Threading;
using Android.OS;

namespace SBahnChaosApp.Droid
{
    [Service]
    [IntentFilter(new string[] { "de.sbahnchaosapp.BackgroundService" })]
    public class BackgroundService : Service
    {
        BackgroundServiceBinder binder;

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
            // startServiceInForeground();

            DoWork();


            return StartCommandResult.Sticky;
        }

        private void startServiceInForeground()
        {
            var ongoing = new Notification(Resource.Drawable.icon, "Service in Foreground");
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), 0);

            ongoing.SetLatestEventInfo(this, "BackgroundService", "BackgroundService is running", pendingIntent);

            StartForeground((int)NotificationFlags.NoClear, ongoing);

            /*
             *  Intent intent = new Intent("com.rj.notitfications.SECACTIVITY");
             *
             *  PendingIntent pendingIntent = PendingIntent.getActivity(MainActivity.this, 1, intent, 0);
             *
             *  Notification.Builder builder = new Notification.Builder(MainActivity.this);
             *
             *  builder.setAutoCancel(false);
             *  builder.setTicker("this is ticker text");
             *  builder.setContentTitle("WhatsApp Notification");               
             *  builder.setContentText("You have a new message");
             *  builder.setSmallIcon(R.drawable.ic_launcher);
             *  builder.setContentIntent(pendingIntent);
             *  builder.setOngoing(true);
             *  builder.setSubText("This is subtext...");   //API level 16
             *  builder.setNumber(100);
             *  builder.build();
             *
             *  myNotication = builder.getNotification();
             *  manager.notify(11, myNotication);
             */

        }

        public void DoWork()
        {
            Toast.MakeText(this, "the Service started", ToastLength.Long).Show();

            var t = new Thread(() =>
            {

                while (true)
                {
                    Thread.Sleep(10000);
                    string a = DateTime.Now.ToString("HH:mm");
                    sendNotification(a);
                }
            });

            t.Start();
        }

        void sendNotification(string message)
        {
            var nMgmt = (NotificationManager)GetSystemService(NotificationService);
            var notification = new Notification(Resource.Drawable.icon, "Hello World");
            var pendingIntent = PendingIntent.GetActivity(this, 0, new Intent(this, typeof(MainActivity)), 0);
            notification.SetLatestEventInfo(this, "Notification", message, pendingIntent);
            
            nMgmt.Notify(0, notification);
        }

        public override IBinder OnBind(Intent intent)
        {
            binder = new BackgroundServiceBinder(this);
            return binder;
        }
    }
}