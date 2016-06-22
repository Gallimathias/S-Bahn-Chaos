namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents an audio file to be treated as music by the Telegram clients.
    /// </summary>
    public class Audio
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
        /// [Optional] Performer of the audio as defined by sender or by audio tags
        /// </summary>
        public string performer { get; set; }
        /// <summary>
        /// [Optional] Title of the audio as defined by sender or by audio tags
        /// </summary>
        public string title { get; set; }
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