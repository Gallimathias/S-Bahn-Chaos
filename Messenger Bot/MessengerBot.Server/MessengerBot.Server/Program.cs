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
            string uri;
            using (StreamReader reader = new StreamReader(File.OpenRead(@".\privateConfig.keys")))
            {
                uri = reader.ReadLine();
            }

            
            var request = WebRequest.Create(new Uri($"{uri}/getUpdates"));
            request.Timeout = 60000;
            var response = request.GetResponse();
            

            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                var message = reader.ReadToEndAsync().Result;
            }

            Console.ReadLine();
        }
        
    }
}
