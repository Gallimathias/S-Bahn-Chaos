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
        public static List<Line> Lines { get; set; }

        public static async Task<List<RawData>> ReadLocData()
        {
            var nvc = new StringBuilder();
            nvc.Append("CoordSystem=NBWT&");
            nvc.Append("ts=1464121860493&");
            nvc.Append("ModCode=0,1,3,5&");
            nvc.Append("_1464124645590");

            using (StreamReader reader = new StreamReader(await new WebClient().OpenReadTaskAsync($"http://mobil.vvs.de/VELOC?{nvc.ToString()}")))
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

        public static async Task BeginReciveData()
        {
            var are = new AutoResetEvent(true);
            Lines = new List<Line>();

            while (true)
            {
                are.WaitOne(30000);
                RawToLine(await ReadLocData());
                Console.WriteLine("Finish");
            }          
        }

        public static void RawToLine(List<RawData> Data)
        {
            foreach (var item in Data)
            {
                var line = item.ToLine();
                line.CityCode = "stgt"; //for Debugging

                if (!Lines.Exists(l => l.Name == line.Name && l.VehicleType == line.VehicleType && l.CityCode == line.CityCode))
                    Lines.Add(line);

                line = Lines.First(l => l.Name == line.Name && l.VehicleType == line.VehicleType && l.CityCode == line.CityCode);

                var vehicle = item.ToVehicle();

                if (line.Vehicles.ContainsKey(vehicle.ID))
                {
                    Lines.First(l => l.Name == line.Name && l.VehicleType == line.VehicleType && l.CityCode == line.CityCode).Vehicles.Remove(vehicle.ID);
                    Lines.First(l => l.Name == line.Name && l.VehicleType == line.VehicleType && l.CityCode == line.CityCode).Vehicles.Add(vehicle.ID, vehicle);
                }
                else
                {
                    Lines.First(l => l.Name == line.Name && l.VehicleType == line.VehicleType && l.CityCode == line.CityCode).Vehicles.Add(vehicle.ID, vehicle);
                }
            }

        }


        public static string lineTextToName(string label) => label.Where(x => char.IsNumber(x)).ToString();




    }
}
