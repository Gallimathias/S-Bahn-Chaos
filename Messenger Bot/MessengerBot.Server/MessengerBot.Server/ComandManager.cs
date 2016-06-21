using MessengerBot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Server
{
    public static class ComandManager
    {
        public static ComandHandler<CommandArg, bool> ComandHandler { get; set; }

        public static void ThrowCommand(string command, CommandArg args) => ComandHandler.Throw(command, args);

        public static void Inizialize()
        {

        }
    }
}
