using MessengerBot.Core.Telegram;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Server
{
    class Program
    {

        static void Main(string[] args)
        {
            ComandManager.Inizialize();

            string uri;
            using (StreamReader reader = new StreamReader(File.OpenRead(@".\privateConfig.keys")))
            {
                uri = reader.ReadLine();
            }


            var request = WebRequest.Create(new Uri($"{uri}/getUpdates"));
            request.Timeout = 60000;
            var response = request.GetResponse();

            Update[] update;

            var a = read(response.GetResponseStream());
            update = JsonConvert.DeserializeObject<Rootobject>(a).result;

            foreach (var item in update)
            {
                var message = item.message;
                //if (message.text.Contains("/get"))
                //{
                //    var u = new Uri($"{uri}/sendMessage");
                //    transfer m = new transfer();
                //    m.chat_id = message.chat.id;
                //    m.text = "Hallo du da";
                //    var text = JsonConvert.SerializeObject(m);
                //    request = WebRequest.Create(u);


                //    request.ContentType = "application/json";
                //    request.Method = "POST";
                //    write(request.GetRequestStream(), text);
                //    text = read(request.GetResponse().GetResponseStream());
                //}

                var pos = message.text.Split();

                ComandManager.ThrowCommand(pos.First(s => s.StartsWith("/")), new CommandArg(pos, item));
            }

            Console.ReadLine();

        }

        static string read(Stream stream)
        {
            using (StreamReader reader = new StreamReader(stream))
                return reader.ReadToEndAsync().Result;
        }

        static void write(Stream stream, string text)
        {
            using (StreamWriter writer = new StreamWriter(stream))
            {
                writer.Write(text);
                writer.Flush();
            }
        }

        class transfer
        {
            public int chat_id { get; set; }
            public string text { get; set; }
        }

    }
}
