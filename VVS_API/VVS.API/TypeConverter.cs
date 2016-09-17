using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VVS.API.SQL;
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

        public static Line RawToLine(RawData data)
        {
            var array = data.LineText.Split(' ');
            VehicleType type = StringToType(array[0]);
            return new Line(array[1], type);
        }

        public static Vehicle RawToVehicle(RawData data)
        {
            var vehicle = new Vehicle();

            vehicle.CurrentStop = data.CurrentStop;
            vehicle.Delay = data.Delay;
            vehicle.DirectionText = data.DirectionText;
            vehicle.ID = data.ID;
            vehicle.IsAtStop = data.IsAtStop;
            vehicle.JourneyIdentifier = data.JourneyIdentifier;
            vehicle.Latitude = data.Latitude;
            vehicle.LatitudeBefore = data.LatitudeBefore;
            vehicle.Longitude = data.Longitude;
            vehicle.LongitudeBefore = data.LongitudeBefore;
            vehicle.NextStop = data.NextStop;
            vehicle.Timestamp = data.Timestamp;
            vehicle.TimestampBefore = data.TimestampBefore;

            var array = data.LineText.Split(' ');
            vehicle.Type = StringToType(array[0]);

            return vehicle;
        }

        public static Lines LineToLines(Line line)
        {
            var entry = new Lines();
            entry.citycode = line.CityCode;
            entry.name = line.Name;
            entry.vehicle_type = VehicleTypeToBinary(line.VehicleType);

            return entry;
        }

        public static Line LinesToLine(Lines entry)
        {
            VehicleType type = BinaryToVehicleType(entry.vehicle_type);

            var line = new Line(entry.name, type);
            line.CityCode = entry.citycode;
            line.Id = entry.Id;

            return line;
        }

        public static Vehicles VehicleToVehicles(Vehicle vehicle)
        {
            var entry = new Vehicles();

            entry.db_id = vehicle.ID;
            entry.line = vehicle.Line_id.Value;
            
            entry.vehicle_type = VehicleTypeToBinary(vehicle.Type);

            return entry;
        }
        
        public static Vehicle VehiclesToVehicle(Vehicles entry)
        {
            var vehicle = new Vehicle();

            vehicle.ID = entry.db_id;
            vehicle.Line_id = entry.line;
            vehicle.Database_Id = entry.Id;
            
            vehicle.Type = BinaryToVehicleType(entry.vehicle_type);

            return vehicle;
        }

        public static Binary VehicleTypeToBinary(VehicleType vehicleType)
        {
            var array = new byte[] { (byte)vehicleType };
            return new Binary(array);
        }

        public static VehicleType BinaryToVehicleType(Binary binary)
        {
            var array = binary.ToArray();
            return (VehicleType)array[0];
        }

        public static History VehicleToHistory(Vehicle vehicle)
        {
            var history = new History();

            history.current_stop = vehicle.CurrentStop;
            history.delay = vehicle.Delay;
            history.direction = vehicle.DirectionText;
            history.isAtStop = vehicle.IsAtStop;
            history.journey = vehicle.JourneyIdentifier;
            history.latitude = vehicle.Latitude;
            history.longitude = vehicle.Longitude;
            history.next_stop = vehicle.NextStop;
            history.timestamp = vehicle.Timestamp;
            history.timestamp_before = vehicle.TimestampBefore;
            history.vehicle = vehicle.Database_Id.Value;
            
            return history;
        }

        public static Dictionary<string, Line> lineListToDictionary(List<Line> lines)
        {
            var tmp = new Dictionary<string, Line>();

            foreach (var line in lines)
                tmp.Add(line.GetKey(), line);

            return tmp;
        }

        public static Dictionary<string, Vehicle> vehicleListToDictionary(List<Vehicle> vehicles)
        {
            var tmp = new Dictionary<string, Vehicle>();

            foreach (var vehicle in vehicles)
                tmp.Add(vehicle.ID, vehicle);

            return tmp;
        }
    }
}
