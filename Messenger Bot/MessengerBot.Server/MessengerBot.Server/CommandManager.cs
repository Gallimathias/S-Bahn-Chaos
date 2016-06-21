using MessengerBot.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Server
{
    public static class CommandManager
    {
        public static ComandHandler<CommandArg, dynamic> ComandHandler { get; set; }

        public static dynamic ThrowCommand(string command, CommandArg args) => ComandHandler.Throw(command, args);

        public static void Inizialize()
        {
            ComandHandler["/help"] = (args) => getCommands(args);
        }

        private static dynamic getCommands(CommandArg args)
        {
            var commands = ComandHandler.GetListOfCommands();


            return false;
        }
    }
}
