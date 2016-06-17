namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents one special entity in a text message. 
    /// For example, hashtags, usernames, URLs, etc. 
    /// </summary>
    public class MessageEntity
    {
        /// <summary>
        /// Type of the entity. 
        /// be mention (@username), hashtag, bot_command, url, email, 
        /// bold (bold text), italic (italic text), 
        /// code (monowidth string), pre (monowidth block),
        /// text_link (for clickable text URLs), text_mention (for users without usernames)
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// Offset in UTF-16 code units to the start of the entity
        /// </summary>
        public int offset { get; set; }
        /// <summary>
        /// Length of the entity in UTF-16 code units
        /// </summary>
        public int length { get; set; }
        /// <summary>
        /// [Optional] For “text_link” only, url 
        /// that will be opened after user taps on the text
        /// </summary>
        public string url { get; set; }
        /// <summary>
        /// [Optional] For “text_mention” only, the mentioned user
        /// </summary>
        public User user { get; set; }
    }
}