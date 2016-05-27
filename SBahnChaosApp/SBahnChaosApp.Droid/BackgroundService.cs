using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Widget;
using System;
using System.Threading;
using Android.OS;
using System.Net;

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
            //startServiceInForeground();

            DoWork();


            return StartCommandResult.Sticky;
        }

        private void startServiceInForeground()
        {
            Intent intent = new Intent(this, typeof(MainActivity));

            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 1, intent, 0);
            
            Notification.Builder builder = new Notification.Builder(this);

            builder.SetAutoCancel(false);
            builder.SetTicker("this is ticker text");
            builder.SetContentTitle("WhatsApp Notification");
            builder.SetContentText("You have a new message");
            builder.SetSmallIcon(Resource.Drawable.icon);
            builder.SetContentIntent(pendingIntent);
            builder.SetOngoing(true);
            builder.SetSubText("This is subtext...");   //API level 16
            builder.SetNumber(100);
            
            StartForeground((int)NotificationFlags.ForegroundService, builder.Build());
        }

        public void DoWork()
        {
            Toast.MakeText(this, "the Service started", ToastLength.Long).Show();

            var t = new Thread(() =>
            {
                
                while (true)
                {
                    Thread.Sleep(10000);
                    //string a = DateTime.Now.ToString("HH:mm");
                    //sendNotification(a);
                }
            });

            t.Start();
        }

        void sendNotification(string message)
        {
            var manager = (NotificationManager)GetSystemService(NotificationService);
            Intent intent = new Intent(this, typeof(MainActivity));

            PendingIntent pendingIntent = PendingIntent.GetActivity(this, 0, intent, 0);

            Notification.Builder builder = new Notification.Builder(this);

            builder.SetAutoCancel(true);
            builder.SetTicker("this is ticker text");
            builder.SetContentTitle("Message from Service");
            builder.SetContentText(message);
            builder.SetSmallIcon(Resource.Drawable.icon);
            builder.SetContentIntent(pendingIntent);
            builder.SetOngoing(false);
            //builder.SetSubText("This is subtext...");   //API level 16
            //builder.SetNumber(100);

            manager.Notify((int)NotificationFlags.OnlyAlertOnce, builder.Build());
        }

        public override IBinder OnBind(Intent intent)
        {
            binder = new BackgroundServiceBinder(this);
            return binder;
        }
    }
}