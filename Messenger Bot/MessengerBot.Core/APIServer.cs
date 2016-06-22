using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Core
{
    public abstract class APIServer<TArguments, TResults>
    {
        public string MasterKey { get; private set; }
        public ComandHandler<TArguments, TResults> ComandHandler { get; set; }

        public APIServer(string path)
        {
            MasterKey = readKey(path);
            ComandHandler = new ComandHandler<TArguments, TResults>();
            Initialize();
        }

        public virtual void Initialize()
        {

        }

        public string Read(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEndAsync().Result;
        }

        public void Write(Stream stream, string text)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(text);
                writer.Flush();
            }
        }

        public Stream GetRequestStream(Uri uri, string Method)
        {
            var request = WebRequest.Create(uri);


            request.ContentType = "application/json";
            request.Method = Method;
            return request.GetRequestStream();
        }
        public Stream GetRequestStream(string uri)
        {
            return GetRequestStream(new Uri(uri), "POST");
        }
        public Stream GetRequestStream(Uri uri)
        {
            return GetRequestStream(uri, "POST");
        }

        public Stream GetResponseStream(Uri uri, string Method)
        {
            var request = WebRequest.Create(uri);
            request.Timeout = 60000;
            request.Method = "GET";
            var response = request.GetResponse();

            return response.GetResponseStream();
        }
        public Stream GetResponseStream(string uri)
        {
            return GetResponseStream(new Uri(uri), "GET");
        }
        public Stream GetResponseStream(Uri uri)
        {
            return GetResponseStream(uri, "GET");
        }

        private string readKey(string path)
        {
            using (StreamReader reader = new StreamReader(File.OpenRead(path)))
                return reader.ReadLine();
        }

    }
}
