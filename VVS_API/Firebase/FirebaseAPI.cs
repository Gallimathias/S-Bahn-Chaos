using Firebase.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json;
using System.IO;

namespace Firebase
{
    public static class FirebaseAPI
    {
        public static Response SendRequest(Message message)
        {
            var req = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
            req.ContentType = "application/json";
            req.Method = "post";

            req.Headers.Add("Authorization","key=AIzaSyDCamtAQG_A6EGGUqWDvqL48RJGmCJYizM");
            req.Headers.Add("Sender", "id=181042758685");


            using (var streamWriter = new StreamWriter(req.GetRequestStream()))
                streamWriter.Write(JsonConvert.SerializeObject(message));

            
            var res = req.GetResponse();

            using (var streamReader = new StreamReader(res.GetResponseStream()))
                return JsonConvert.DeserializeObject<Response>(streamReader.ReadToEnd());



        }

    }
}
