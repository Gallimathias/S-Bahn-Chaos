using MessengerBot.Core;
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
            CommandManager.Inizialize();
            


            

            foreach (var item in update)
            {
                var message = item.message;
                //if (message.text.Contains("/get"))
                //{
                //  
                //}

                var pos = message.text.Split();

                CommandManager.ThrowCommand(pos.First(s => s.StartsWith("/")), new CommandArg(pos, item));
            }

            Console.ReadLine();

        }

    }
}
