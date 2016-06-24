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
        private static DataContext database;
        private static StringBuilder stringBuilder;
        private static string connectionString;
        public static void Connect()
        {
            connectionString = Properties.Settings.Default.TestBaseConnectionString;

            stringBuilder = new StringBuilder();
            stringBuilder.Append(@"Data Source = KRUEGERM - NB\NAV90SQL;");
            stringBuilder.Append("Initial Catalog = TestBase;");
            stringBuilder.Append("Integrated Security = True;");
            stringBuilder.Append("Persist Security Info = False;");
            stringBuilder.Append("Pooling = False;");
            stringBuilder.Append("MultipleActiveResultSets = False;");
            stringBuilder.Append("Connect Timeout = 60;");
            stringBuilder.Append("Encrypt = False;");
            stringBuilder.Append("TrustServerCertificate = True");

            database = new DataContext(connectionString);

            


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
