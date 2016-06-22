using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a chat.
    /// </summary>
    public class Chat
    {
        /// <summary>
        /// Unique identifier for this chat. This number may be greater than 32 bits and some programming languages 
        /// may have difficulty/silent defects in interpreting it. But it smaller than 52 bits, 
        /// so a signed 64 bit integer or double-precision float type are safe for storing this identifier.
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Type of chat, can be either “private”, “group”, “supergroup” or “channel”
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// [Optional] Title, for channels and group chats
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// [Optional] Username, for private chats, supergroups and channels if available
        /// </summary>
        public string username { get; set; }
        /// <summary>
        /// [Optional] First name of the other party in a private chat
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        /// [Optional] Last name of the other party in a private chat
        /// </summary>
        public string last_name { get; set; }
    }
}
