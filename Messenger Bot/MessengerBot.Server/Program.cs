namespace MessengerBot.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            CommandManager.Inizialize();
            TelegramServer.Initialize(@".\privateConfig.keys");
            TelegramServer.Start();

            while (true) { }

        }

    }
}
