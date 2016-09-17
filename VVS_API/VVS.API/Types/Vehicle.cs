using System;

namespace VVS.API.Types
{
    public class Vehicle
    {
        public int? Database_Id { get; set; }
        public int? Line_id { get; set; }
        public string CurrentStop { get; set; }
        public int Delay
        {
            get { return delay; }
            set
            {
                if (delay != value)
                    DelayHasChanged = true;
                else
                    DelayHasChanged = false;

                delay = value;
            }
        }
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
        public VehicleType Type { get; set; }

        public bool DelayHasChanged { get; private set; }

        private int delay;
    }
}