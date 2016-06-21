using MessengerBot.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Server
{
    internal class TelegramServer : APIServer<CommandArg, dynamic>
    {
        public TelegramServer(string path) : base(path)
        {
        }

        public override void Initialize()
        {
            ComandHandler["/getUpdates"] = (args) => getUpdates();
            ComandHandler["/sendMessage"] = (args) => sendMessage();
        }

        private dynamic sendMessage()
        {
            Write(GetRequestStream($"{MasterKey}/sendMessage"),"blub");
            return true;
        }

        private dynamic getUpdates()
        {
            return Read(GetResponseStream($"{MasterKey}/getUpdates"));
        }

        
    }
}
