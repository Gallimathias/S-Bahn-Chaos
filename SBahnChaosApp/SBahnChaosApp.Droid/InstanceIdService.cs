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
using Firebase.Iid;
using System.Threading;
using Firebase;
using Firebase.Messaging;
using Android.Util;

namespace SBahnChaosApp.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCEID_EVENT" })]
    public class InstanceIdService : FirebaseInstanceIdService
    {
        const string TAG = "InstanceIdService";

        public InstanceIdService() : base()
        {

        }
        public InstanceIdService(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnTokenRefresh()
        {
            
            var t = new Thread(() =>
            {
                var instance = FirebaseInstanceId.Instance;

                instance.DeleteInstanceId();
                instance.GetToken("181042758685", FirebaseMessaging.InstanceIdScope);
                Log.Debug(TAG, "Token: " + instance.Token);
            });
            t.Start();
            
        }
    }
}