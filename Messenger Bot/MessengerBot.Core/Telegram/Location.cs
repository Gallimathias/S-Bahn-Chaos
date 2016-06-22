namespace MessengerBot.Core.Telegram
{
    /// <summary>
    /// This object represents a point on the map.
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Longitude as defined by sender
        /// </summary>
        public float longitude { get; set; }
        /// <summary>
        /// Latitude as defined by sender
        /// </summary>
        public float latitude { get; set; }
    }
}