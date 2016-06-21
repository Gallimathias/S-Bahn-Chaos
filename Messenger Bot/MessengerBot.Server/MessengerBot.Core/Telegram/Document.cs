namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a general file (as opposed to photos, voice messages and audio files).
    /// </summary>
    public class Document
    {
        /// <summary>
        /// Unique file identifier
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// [Optional] Document thumbnail as defined by sender
        /// </summary>
        public PhotoSize thumb { get; set; }
        /// <summary>
        /// [Optional] Original filename as defined by sender
        /// </summary>
        public string file_name { get; set; }
        /// <summary>
        /// [Optional] MIME type of the file as defined by sender
        /// </summary>
        public string mime_type { get; set; }
        /// <summary>
        /// [Optional] File size
        /// </summary>
        public int file_size { get; set; }
    }
}