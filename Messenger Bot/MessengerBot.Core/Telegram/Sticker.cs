namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a sticker.
    /// </summary>
    public class Sticker
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// Sticker width
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// Sticker height
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// [Optional] Sticker thumbnail in .webp or .jpg format
        /// </summary>
        public PhotoSize thumb { get; set; }
        /// <summary>
        /// [Optional] Emoji associated with the sticker
        /// </summary>
        public string emoji { get; set; }
        /// <summary>
        /// [Optional] File size
        /// </summary>
        public int file_size { get; set; }
    }
}