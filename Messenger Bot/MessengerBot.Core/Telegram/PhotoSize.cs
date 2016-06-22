namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents one size of a photo or a file / sticker thumbnail.
    /// </summary>
    public class PhotoSize
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// Photo width
        /// </summary>
        public int width { get; set; }
        /// <summary>
        /// Photo height
        /// </summary>
        public int height { get; set; }
        /// <summary>
        /// [Optional] File size
        /// </summary>
        public int file_size { get; set; }
    }
}