namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a voice note.
    /// </summary>
    public class Voice
    {
        /// <summary>
        /// Unique identifier for this file
        /// </summary>
        public string file_id { get; set; }
        /// <summary>
        /// Duration of the audio in seconds as defined by sender
        /// </summary>
        public int duration { get; set; }
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