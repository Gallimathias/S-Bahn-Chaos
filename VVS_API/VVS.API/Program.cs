using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using VVS.API.Types;

namespace VVS.API
{
    class Program
    {
        static void Main(string[] args)
        {
            //DatabaseManager.Connect();

            //APIConnection.Lines = DatabaseManager.GetLines("stgt");

            //foreach (var line in APIConnection.Lines)
            //    foreach (var vehicle in DatabaseManager.GetVehicles(line.Id))
            //        line.Vehicles.Add(vehicle.ID, vehicle);


            APIConnection.BeginReciveData();

            while (true) { };
        }
    }
}
