using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VVS.API.Types
{
    class RawData
    {
        public string CurrentStop { get; set; }
        public DateTimeOffset DayOfOperation { get; set; }
        public int Delay { get; set; }
        public string DirectionText { get; set; }
        public string ID { get; set; }
        public bool IsAtStop { get; set; }
        public string JourneyIdentifier { get; set; }
        public string Latitude { get; set; }
        public string LatitudeBefore { get; set; }
        public string LineText { get; set; }
        public string Longitude { get; set; }
        public string LongitudeBefore { get; set; }
        public int ModCode { get; set; }
        public string NextStop { get; set; }
        public string Operator { get; set; }
        public string ProductIdentifier { get; set; }
        public int RealtimeAvailable { get; set; }
        public DateTimeOffset Timestamp { get; set; }
        public DateTimeOffset TimestampBefore { get; set; }
        
        public Vehicle ToVehicle()
        {
            var vehicle = new Vehicle();

            vehicle.CurrentStop         = CurrentStop;
            vehicle.Delay               = Delay;
            vehicle.DirectionText       = DirectionText;
            vehicle.ID                  = ID;
            vehicle.IsAtStop            = IsAtStop;
            vehicle.JourneyIdentifier   = JourneyIdentifier;
            vehicle.Latitude            = Latitude;
            vehicle.LatitudeBefore      = LatitudeBefore;
            vehicle.Longitude           = Longitude;
            vehicle.LongitudeBefore     = LongitudeBefore;
            vehicle.NextStop            = NextStop;
            vehicle.Timestamp           = Timestamp;
            vehicle.TimestampBefore     = TimestampBefore;

            return vehicle;
        }

        public override string ToString() => LineText;
        
    }
}
