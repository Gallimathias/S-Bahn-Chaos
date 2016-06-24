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
    internal static class DatabaseManager
    {
        private static VVSDatabaseDataContext dataContext;

        public static void Connect()
        {
            dataContext = new VVSDatabaseDataContext();
        }

        public static void InsertLine(Line line)
        {
            var entry           = new Lines();
            entry.citycode      = line.CityCode;
            entry.name          = line.Name;
            var array           = new byte[] { (byte)line.VehicleType };
            entry.vehicle_type  = new Binary(array);

            var table = dataContext.GetTable<Lines>();
            table.InsertOnSubmit(entry);
            
        }

        public static void InsertLines(List<Line> lines)
        {
            var list = new List<Lines>();

            foreach (var line in lines)
            {
                var entry = new Lines();
                entry.citycode = line.CityCode;
                entry.name = line.Name;
                var array = new byte[] { (byte)line.VehicleType };
                entry.vehicle_type = new Binary(array);
                list.Add(entry);
            }

            var table = dataContext.GetTable<Lines>();
            table.InsertAllOnSubmit(list);
        }

        public static void InsertVehicle(Vehicle vehicle)
        {
            var entry = new Vehicles();

            entry.db_id = vehicle.ID;
            entry.line = vehicle.Line_id;
            entry.current_stop = vehicle.CurrentStop;
            entry.delay = vehicle.Delay;
            entry.direction = vehicle.DirectionText;
            entry.isAtStop = vehicle.IsAtStop;
            entry.journey = vehicle.JourneyIdentifier;
            entry.latitude = vehicle.Latitude;
            entry.longitude = vehicle.Longitude;
            entry.next_stop = vehicle.NextStop;
            entry.timestamp = vehicle.Timestamp;
            entry.timestamp_before = vehicle.TimestampBefore;

            
            var array = new byte[] { (byte)vehicle.Type};
            entry.vehicle_type = new Binary(array);

            var table = dataContext.GetTable<Vehicles>();
            table.InsertOnSubmit(entry);
        }

        public static void InsertVehicles(List<Vehicle> vehicles)
        {
            var list = new List<Vehicles>();

            foreach (var vehicle in vehicles)
            {
                var entry = new Vehicles();

                entry.db_id = vehicle.ID;
                entry.line = vehicle.Line_id;
                entry.current_stop = vehicle.CurrentStop;
                entry.delay = vehicle.Delay;
                entry.direction = vehicle.DirectionText;
                entry.isAtStop = vehicle.IsAtStop;
                entry.journey = vehicle.JourneyIdentifier;
                entry.latitude = vehicle.Latitude;
                entry.longitude = vehicle.Longitude;
                entry.next_stop = vehicle.NextStop;
                entry.timestamp = vehicle.Timestamp;
                entry.timestamp_before = vehicle.TimestampBefore;


                var array = new byte[] { (byte)vehicle.Type };
                entry.vehicle_type = new Binary(array);
                list.Add(entry);
            }

            var table = dataContext.GetTable<Vehicles>();
            table.InsertAllOnSubmit(list);
        }
        public static void InsertVehicles(Line line)
        {
            InsertVehicles(line.GetVehicles());
        }
        public static void InsertVehicles(List<Line> lines)
        {
            foreach (var line in lines)
                InsertVehicles(line);
        }

        public static List<Line> GetIds(List<Line> lines)
        {
            var table = dataContext.GetTable<Lines>();

            foreach (var line in lines)
            {
                var array = new byte[] { (byte)line.VehicleType };
                var type = new Binary(array);
                var rec = table.First(v => v.name == line.Name 
                                        && v.vehicle_type == type);

                line.Id = rec.Id;
            }

            return lines;
        }

        public static void Submit()
        {
            dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
        }
    }
}
