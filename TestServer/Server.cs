using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TestServer
{
    class Server
    {
        private readonly HttpListener server;

        public Server()
        {
            server = new HttpListener();
        }

        public void Start(int port)
        {
            server.Prefixes.Add($"http://+:{port}/lists/");
            server.Prefixes.Add($"http://+:{port}/objects/");
            server.Start();

            server.BeginGetContext(EndGetContext, null);
        }

        private void EndGetContext(IAsyncResult ar)
        {
            HttpListenerContext context;

            try
            {
                context = server.EndGetContext(ar);
            }
            finally
            {
                server.BeginGetContext(EndGetContext, null);
            }

            var request = context.Request;

            var localPath = request.Url.LocalPath.TrimStart('/').TrimEnd('/');
            string a;


            using (StreamReader reader = new StreamReader(request.InputStream))
            {
                a = reader.ReadToEnd();
            }

            var response = context.Response;

            var query = request.Url.Query.TrimStart('?').Split('&');

            var stream = response.OutputStream;
            response.SendChunked = false;
            using (MemoryStream ms = new MemoryStream())
            {
                using (BinaryWriter bw = new BinaryWriter(ms))
                {
                    foreach (var item in DataBaseAPI.Lines)
                    {
                        var buf = ((byte)item.VehicleType << 13) | item.Name;
                        bw.Write(buf);
                    }
                    response.ContentLength64 = ms.Length;
                    stream.Write(ms.ToArray(), 0, ms.ToArray().Length);
                }
            }

        }
    }
}
