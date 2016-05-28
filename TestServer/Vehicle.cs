using System;

namespace TestServer
{
    public class Vehicle
    {
        public string CurrentStop { get; set; }
        public int Delay { get; set; }
        public string DirectionText { get; set; }
        public string ID { get; set; }
        public bool IsAtStop { get; set; }
        public string JourneyIdentifier { get; set; }
        public string Latitude { get; set; }
        public string LatitudeBefore { get; set; }
        public string Longitude { get; set; }
        public string LongitudeBefore { get; set; }
        public string NextStop { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public DateTimeOffset TimestampBefore { get; set; }
        
    }
}