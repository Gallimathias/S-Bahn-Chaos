using MessengerBot.Core.SQL;
using Types = Telegram.Bot.Types;
using System.Data.Linq;
using System.Text;
using System.Configuration;
using System.Reflection;

namespace MessengerBot.Server
{
    internal static class DatabaseManager
    {
        private static BotDatabaseDataContext database;
        private static StringBuilder stringBuilder;
        private static string connectionString;
        public static void Connect()
        {
            database = new BotDatabaseDataContext();
        }

        public static void Insert(Types.Chat chat)
        {
            Table<User> userTable = database.GetTable<User>();

            var entry = new User();

            entry.chat_id = chat.Id;
            entry.Telegram_Id = chat.Id;
            entry.first_name = chat.FirstName;
            entry.last_name = chat.LastName;
            entry.username = chat.Username;

            userTable.InsertOnSubmit(entry);
            database.SubmitChanges();

        }
    }
}
