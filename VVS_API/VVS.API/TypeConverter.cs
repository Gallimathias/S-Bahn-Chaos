using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVS.API.Types;

namespace VVS.API
{
    public static class TypeConverter
    {

        public static VehicleType CharToType(char t)
        {
            switch (t)
            {
                case ('S'):
                    return VehicleType.S;
                case ('B'):
                    return VehicleType.B;
                case ('U'):
                    return VehicleType.U;
                default:
                    return VehicleType.None;
            }
        }

        public static VehicleType StringToType(string line)
        {
            switch (line)
            {
                case "Bus":
                    return VehicleType.B;
                case "S-Bahn":
                    return VehicleType.S;
                case "Stadtbahn":
                    return VehicleType.U;
                case "R-Bahn":
                    return VehicleType.R;
                case "SEV-Bus":
                    return VehicleType.SEV;
                case "Zahnradbahn":
                    return VehicleType.Z;
                default:
                    return VehicleType.None;
            }
        }

        public static Line ToLine(RawData data)
        {
            var array = data.LineText.Split(' ');
            VehicleType type = StringToType(array[0]);
            return new Line(array[1], type);
        }

        public static Vehicle ToVehicle(RawData data)
        {
            var vehicle = new Vehicle();
            
            vehicle.CurrentStop         = data.CurrentStop;
            vehicle.Delay               = data.Delay;
            vehicle.DirectionText       = data.DirectionText;
            vehicle.ID                  = data.ID;
            vehicle.IsAtStop            = data.IsAtStop;
            vehicle.JourneyIdentifier   = data.JourneyIdentifier;
            vehicle.Latitude            = data.Latitude;
            vehicle.LatitudeBefore      = data.LatitudeBefore;
            vehicle.Longitude           = data.Longitude;
            vehicle.LongitudeBefore     = data.LongitudeBefore;
            vehicle.NextStop            = data.NextStop;
            vehicle.Timestamp           = data.Timestamp;
            vehicle.TimestampBefore     = data.TimestampBefore;

            return vehicle;
        }
    }
}
