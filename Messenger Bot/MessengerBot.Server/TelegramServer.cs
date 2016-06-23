using MessengerBot.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace MessengerBot.Server
{
    public static class TelegramServer
    {
        public static Api TelegramApi { get; private set; }

        public static void Initialize(string path)
        {
            TelegramApi = new Api(readKey(path));
            TelegramApi.UpdateReceived += TelegramApi_UpdateReceived;
            TelegramApi.MessageReceived += TelegramApi_MessageReceived;
            TelegramApi.ReceiveError += TelegramApi_ReceiveError;
        }

        public static void Start()
        {
            TelegramApi.StartReceiving();
            DatabaseManager.Connect();
        }

        public static void Stop()
        {
            TelegramApi.StopReceiving();
        }

        private static void TelegramApi_ReceiveError(object sender, ReceiveErrorEventArgs e)
        {
            Console.WriteLine("Error");
        }

        private static void TelegramApi_MessageReceived(object sender, MessageEventArgs e)
        {
            var Message = e.Message;
            
            var elements = Message.Text.Split();

            CommandManager.ThrowCommand(elements.First(t => t.StartsWith("/")), new CommandArg(elements, e));

        }

        private static void TelegramApi_UpdateReceived(object sender, UpdateEventArgs e)
        {
            switch (e.Update.Type)
            {
                case UpdateType.UnkownUpdate:
                    break;
                case UpdateType.MessageUpdate:
                    break;
                case UpdateType.InlineQueryUpdate:
                    break;
                case UpdateType.ChosenInlineResultUpdate:
                    break;
                case UpdateType.CallbackQueryUpdate:
                    break;
                default:
                    break;
            }
        }

        private static string readKey(string path)
        {
            using (StreamReader reader = new StreamReader(System.IO.File.OpenRead(path)))
                return reader.ReadLine();
        }

    }
}
