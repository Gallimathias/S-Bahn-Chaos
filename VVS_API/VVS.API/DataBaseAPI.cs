using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace VVS.API
{
    static class DataBaseAPI
    {
        private static readonly Random Random = new Random();
        public static List<RawData> Data { get; set; }
        public static List<Line> Lines { get; set; }

        public static async Task<List<RawData>> ReadData()
        {
            var nvc = new NameValueCollection {
                { "CoordSystem", "NBWT" },
                { "ts", "1464121860493" },
                { "ModCode", "0,1,3,5" },
                //{ "_", Random.Next().ToString() }
                {"_", "1464124645590" }
            };

            using (StreamReader reader = new StreamReader(await new WebClient().OpenReadTaskAsync($"http://mobil.vvs.de/VELOC?{nvc.ToString()}")))
                return JArray.Parse(await reader.ReadToEndAsync()).Select(t => t.ToObject<RawData>()).ToList();
        }

        public static void RawToLine()
        {
            foreach (var item in Data)
            {
                try
                {
                    var array = item.LineText.Split(' ');
                    VehicleType type = stringToType(array[0]);
                    var line = new Line(array[1], type);
                    if (!Lines.Exists(l => l.Name == line.Name && l.VehicleType == type))
                    {
                        Lines.Add(line);
                    }
                    else
                    {
                        var lines = Lines.First(l => l.Name == line.Name && l.VehicleType == type);
                        var vehicle = new Vehicle();

                        vehicle.CurrentStop = item.CurrentStop;
                        vehicle.Delay = item.Delay;
                        vehicle.DirectionText = item.DirectionText;
                        vehicle.ID = item.ID;
                        vehicle.IsAtStop = item.IsAtStop;
                        vehicle.JourneyIdentifier = item.JourneyIdentifier;
                        vehicle.Latitude = item.Latitude;
                        vehicle.LatitudeBefore = item.LatitudeBefore;
                        vehicle.Longitude = item.Longitude;
                        vehicle.LongitudeBefore = item.LongitudeBefore;
                        vehicle.NextStop = item.NextStop;
                        vehicle.Timestamp = item.Timestamp;
                        vehicle.TimestampBefore = item.TimestampBefore;

                        if (lines.Vehicles.ContainsKey(vehicle.ID))
                        {
                            Lines.First(l => l.Name == line.Name && l.VehicleType == type).Vehicles.Remove(vehicle.ID);
                            Lines.First(l => l.Name == line.Name && l.VehicleType == type).Vehicles.Add(vehicle.ID, vehicle);
                        }
                        else
                        {
                            Lines.First(l => l.Name == line.Name && l.VehicleType == type).Vehicles.Add(vehicle.ID, vehicle);
                        }
                        

                    }
                }
                catch (Exception)
                {
                    continue;
                }
            }

        }


        public static string lineTextToName(string label) => label.Where(x => char.IsNumber(x)).ToString();


        private static VehicleType charToType(char t)
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

        private static VehicleType stringToType(string line)
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


    }
}
