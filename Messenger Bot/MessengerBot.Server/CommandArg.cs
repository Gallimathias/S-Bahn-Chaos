using MessengerBot.Core.Telegram;

namespace MessengerBot.Server
{
    public class CommandArg
    {
        private Update item;
        private Message message;
        private string[] pos;

        public CommandArg(string[] pos, Update item)
        {
            this.pos = pos;
            this.item = item;
            message = item.message;
        }

        public CommandArg(string[] pos, Message message)
        {
            this.pos = pos;
            this.message = message;
        }
    }
}