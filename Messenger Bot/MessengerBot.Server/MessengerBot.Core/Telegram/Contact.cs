namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a phone contact.
    /// </summary>
    public class Contact
    {
        /// <summary>
        /// Contact's phone number
        /// </summary>
        public string phone_number { get; set; }
        /// <summary>
        /// Contact's first name
        /// </summary>
        public string first_name { get; set; }
        /// <summary>
        /// Optional. Contact's last name
        /// </summary>
        public string last_name { get; set; }
        /// <summary>
        /// Optional. Contact's user identifier in Telegram
        /// </summary>
        public int user_id { get; set; }
    }
}