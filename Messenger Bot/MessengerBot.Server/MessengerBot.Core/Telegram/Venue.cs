namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a venue.
    /// </summary>
    public class Venue
    {
        /// <summary>
        /// Venue location
        /// </summary>
        public Location location { get; set; }
        /// <summary>
        /// Name of the venue
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// Address of the venue
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// [Optional] Foursquare identifier of the venue
        /// </summary>
        public string foursquare_id { get; set; }
    }
}