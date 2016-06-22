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
        public static CommandHandler<CommandArg, dynamic> CommandHandler { get; set; }

        public static dynamic ThrowCommand(string command, CommandArg args) => CommandHandler.Throw(command, args);

        public static void Inizialize()
        {
            CommandHandler = new CommandHandler<CommandArg, dynamic>();
            CommandHandler["/help"] = (args) => getCommands(args);
            CommandHandler["/sendMe"] = (args) => sendMe(args);
        }

        private static dynamic sendMe(CommandArg args)
        {
            var id = args.Chat.Id;
            var api = TelegramServer.TelegramApi;
            var user = api.GetMe().Result;

            api.SendTextMessage(id, $"Das ist eine Return Message {user.Username}");

            return true;
        }

        private static dynamic getCommands(CommandArg args)
        {
            var commands = CommandHandler.GetListOfCommands();


            return true;
        }
    }
}
