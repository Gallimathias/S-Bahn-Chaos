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
            var entry = TypeConverter.LineToLines(line);

            var table = dataContext.GetTable<Lines>();
            table.InsertOnSubmit(entry);

        }

        public static void InsertLines(List<Line> lines)
        {
            var list = new List<Lines>();

            foreach (var line in lines)
            {
                var entry = TypeConverter.LineToLines(line);
                list.Add(entry);
            }

            var table = dataContext.GetTable<Lines>();
            table.InsertAllOnSubmit(list);
        }

        public static void InsertVehicle(Vehicle vehicle)
        {
            var entry = TypeConverter.VehicleToVehicles(vehicle);

            var table = dataContext.GetTable<Vehicles>();
            table.InsertOnSubmit(entry);
        }

        public static void InsertVehicles(List<Vehicle> vehicles)
        {
            var list = new List<Vehicles>();

            foreach (var vehicle in vehicles)
            {
                var entry = TypeConverter.VehicleToVehicles(vehicle);
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
            foreach (var line in lines)
                line.Id = GetId(line);

            return lines;
        }

        public static int GetId(Line line)
        {
            var table = dataContext.GetTable<Lines>();

            var array = new byte[] { (byte)line.VehicleType };
            var type = new Binary(array);
            var rec = table.First(v => v.name == line.Name
                                    && v.vehicle_type == type
                                    && v.citycode == line.CityCode);

            return rec.Id;
        }

        public static List<Line> GetLines()
        {
            var table = dataContext.GetTable<Lines>();
            var list = new List<Line>();

            foreach (var entry in table.ToList())
                list.Add(TypeConverter.LinesToLine(entry));

            return list;
        }
        public static List<Line> GetLines(string citycode)
        {
            var table = dataContext.GetTable<Lines>();
            var list = new List<Line>();

            var rec = table.Where(l => l.citycode == citycode);

            foreach (var entry in rec)
                list.Add(TypeConverter.LinesToLine(entry));

            return list;
        }
        public static List<Line> GetLines(VehicleType type)
        {
            var table = dataContext.GetTable<Lines>();
            var list = new List<Line>();

            var array = new byte[] { (byte)type };
            var vehicle_type = new Binary(array);

            var rec = table.Where(l => l.vehicle_type == vehicle_type);

            foreach (var entry in rec)
                list.Add(TypeConverter.LinesToLine(entry));

            return list;
        }
        public static List<Line> GetLines(VehicleType type, string citycode)
        {
            var table = dataContext.GetTable<Lines>();
            var list = new List<Line>();

            var array = new byte[] { (byte)type };
            var vehicle_type = new Binary(array);

            var rec = table.Where(l => l.vehicle_type == vehicle_type && l.citycode == citycode);

            foreach (var entry in rec)
                list.Add(TypeConverter.LinesToLine(entry));

            return list;
        }

        public static Line GetLine(int id)
        {
            var table = dataContext.GetTable<Lines>();

            var rec = table.First(l => l.Id == id);
                        
            return TypeConverter.LinesToLine(rec);
        }
        public static Line GetLine(string name, string citycode, VehicleType type)
        {
            var table = dataContext.GetTable<Lines>();
            var vehicle_type = TypeConverter.VehicleTypeToBinary(type);

            var rec = table.First(l => l.name == name && l.citycode == citycode && l.vehicle_type == vehicle_type);

            return TypeConverter.LinesToLine(rec);
        }

        public static List<Vehicle> GetVehicles()
        {
            var table = dataContext.GetTable<Vehicles>();
            var list = new List<Vehicle>();

            foreach (var entry in table.ToList())
                list.Add(TypeConverter.VehiclesToVehicle(entry));

            return list;
        }
        public static List<Vehicle> GetVehicles(int line_id)
        {
            var table = dataContext.GetTable<Vehicles>();
            var list = new List<Vehicle>();
            var rec = table.Where(v => v.line == line_id);

            foreach (var entry in rec)
                list.Add(TypeConverter.VehiclesToVehicle(entry));

            return list;
        }

        public static Vehicle GetVehicle(string db_id)
        {
            var table = dataContext.GetTable<Vehicles>();

            var rec = table.First(v => v.db_id == db_id);

            return TypeConverter.VehiclesToVehicle(rec);
        }

        public static void Submit()
        {
            dataContext.SubmitChanges(ConflictMode.ContinueOnConflict);
        }
    }
}
