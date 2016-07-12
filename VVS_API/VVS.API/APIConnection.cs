using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using VVS.API.Types;

namespace VVS.API
{
    static class APIConnection
    {
        public static Dictionary<string, Line> Lines { get; set; }
        
        public static void Initzialize()
        {
            Lines = new Dictionary<string, Line>();
        }

        public static async Task<List<RawData>> ReadLocData()
        {
            var unix = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;
            var nvc = new StringBuilder();
            nvc.Append("CoordSystem=NBWT&");
            nvc.Append($"ts={unix}&");
            nvc.Append("ModCode=0,1,3,5");

            unix = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalMilliseconds;

            using (StreamReader reader = new StreamReader(await new WebClient().OpenReadTaskAsync($"http://mobil.vvs.de/VELOC?{nvc.ToString()}&_{unix}")))
                return JArray.Parse(await reader.ReadToEndAsync()).Select(t => t.ToObject<RawData>()).ToList();
        }

        public static async Task<List<Stop>> ReadStopData(string station)
        {
            var sb = new StringBuilder();

            sb.Append($"name_sf={station}&");
            sb.Append("language=de&");
            sb.Append("stateless=1&");
            sb.Append("locationServerActive=1&");
            sb.Append("anyObjFilter_sf=2&");
            sb.Append("anyMaxSizeHitList=500&");
            sb.Append("outputFormat=JSON&");
            sb.Append("SpEncId=0&");
            sb.Append("type_sf=any&");
            sb.Append("anySigWhenPerfectNoOtherMatches=1&");
            sb.Append("convertAddressesITKernel2LocationServer=1&");
            sb.Append("convertCoord2LocationServer=1&");
            sb.Append("convertCrossingsITKernel2LocationServer=1&");
            sb.Append("convertStopsPTKernel2LocationServer=1&");
            sb.Append("convertPOIsITKernel2LocationServer=1&");
            sb.Append("tryToFindLocalityStops=1&");
            sb.Append("w_objPrefAl=2&");
            sb.Append("w_objPrefAl=12&");
            sb.Append("w_regPrefAm=1");

            using (StreamReader reader = new StreamReader(await new WebClient().OpenReadTaskAsync($"http://mobil.vvs.de/jqm/controller/XSLT_STOPFINDER_REQUEST?{sb.ToString()}")))
            {


                var st = sb.ToString();
                var res = reader.ReadToEndAsync().Result;
                res += "";
            }
            return null;
        }

        public static async Task ReciveData()
        {
            Console.Write("Get Data....");
            RawToLine(await ReadLocData());
            Console.WriteLine("Finish");
            Console.WriteLine($"Count: {Lines.Count}");
            
        }

        public static void RawToLine(List<RawData> Data)
        {
            foreach (var item in Data)
            {
                if (item.DirectionText == null)
                    continue;

                var line = item.ToLine();
                line.CityCode = "stgt";
                var key = line.GetKey();

                if (Lines.ContainsKey(key))
                {
                    Lines.TryGetValue(key, out line);

                    if (line.Id == null)
                        line.Id = DatabaseManager.GetId(line);

                    var vehicle = item.ToVehicle();

                    if (line.Vehicles.ContainsKey(vehicle.ID))
                    {
                        Vehicle temp;
                        line.Vehicles.TryGetValue(vehicle.ID, out temp);
                        temp = vehicle;
                        line.Vehicles[vehicle.ID] = temp;
                    }
                    else
                    {
                        line.Vehicles.Add(vehicle.ID, vehicle);

                        if (line.Id != null)
                        {
                            vehicle.Line_id = line.Id.Value;
                            DatabaseManager.InsertVehicle(vehicle);
                        }
                    }

                    lock (Lines)
                        Lines[key] = line;

                }
                else
                {
                    var vehicle = item.ToVehicle();
                    line.Vehicles.Add(vehicle.ID, vehicle);

                    lock (Lines)
                        Lines.Add(key, line);

                    DatabaseManager.InsertLine(line);
                }
            }

        }
        
        public static string lineTextToName(string label) => label.Where(x => char.IsNumber(x)).ToString();
    }
}
