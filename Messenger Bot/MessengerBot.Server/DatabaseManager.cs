using MessengerBot.Core.SQL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Server
{
    internal static class DatabaseManager
    {
        private static DataContext database;
        public static void Connect()
        {
            database= new DataContext(@"
                Data Source = KRUEGERM - NB\NAV90SQL;
                Initial Catalog = TestBase;
                Integrated Security = True;
                Persist Security Info = False;
                Pooling = False;
                MultipleActiveResultSets = False;
                Connect Timeout = 60;
                Encrypt = False;
                TrustServerCertificate = True");
        }

        public static void Insert(Telegram.Bot.Types.Chat chat)
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
