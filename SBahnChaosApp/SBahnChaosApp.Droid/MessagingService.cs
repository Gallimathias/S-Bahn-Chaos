using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Firebase.Messaging;
using Android.Support.V7.App;
using Android.Media;

namespace SBahnChaosApp.Droid
{
    [Service(Name = "de.sbahnchaosapp.android.MessagingService") ]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MessagingService : FirebaseMessagingService
    {
        const string TAG = "MessagingService";

        public override void OnMessageReceived(RemoteMessage message)
        {
            Android.Util.Log.Debug(TAG, "From: " + message.From);
            Android.Util.Log.Debug(TAG, "Notification Message Body: " + message.GetNotification().Body);
            

            SendNotification(message.From);
        }

        void SendNotification(string messageBody)
        {
            var intent = new Intent(this, typeof(MainActivity));
            intent.AddFlags(ActivityFlags.ClearTop);
            var pendingIntent = PendingIntent.GetActivity(this, 0, intent, PendingIntentFlags.OneShot);

            var defaultSoundUri = RingtoneManager.GetDefaultUri(RingtoneType.Notification);
            var notificationBuilder = new NotificationCompat.Builder(this)
                //.SetSmallIcon(Resource.Drawable.ic_stat_ic_notification)
                .SetContentTitle("FCM Message")
                .SetContentText(messageBody)
                .SetAutoCancel(true)
                .SetSound(defaultSoundUri)
                .SetContentIntent(pendingIntent);

            var notificationManager = NotificationManager.FromContext(this);

            notificationManager.Notify(0, notificationBuilder.Build());
        }
    }
}