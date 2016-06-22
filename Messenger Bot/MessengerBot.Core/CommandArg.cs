using Telegram.Bot.Types;

namespace MessengerBot.Core
{
    public class CommandArg
    {
        public MessageEventArgs MessageEventArgs { get; private set; }
        public string[] Elements { get; private set; }

        public Chat Chat { get { return Message.Chat; } }
        public Message Message { get { return MessageEventArgs.Message; } }

        public CommandArg(string[] elements, MessageEventArgs args)
        {
            Elements = elements;
            MessageEventArgs = args;
        }
    }
}