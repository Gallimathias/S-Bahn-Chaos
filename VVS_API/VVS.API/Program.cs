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
        static Thread backgroundThread;
        static void Main(string[] args)
        {
            DatabaseManager.Connect();
            APIConnection.Initzialize();

            var lineList = DatabaseManager.GetLines().Result;

            APIConnection.Lines = TypeConverter.lineListToDictionary(lineList);

            foreach (var line in APIConnection.Lines)
                line.Value.Vehicles = TypeConverter.vehicleListToDictionary(
                    DatabaseManager.GetVehicles(line.Value.Id.Value).Result);

            backgroundThread = new Thread(async () =>
            {
                while (true)
                {
                    await APIConnection.ReciveData();
                    
                    await DatabaseManager.Submit();
                }
            });

            backgroundThread.IsBackground = true;

            backgroundThread.Start();

            while (true) { Thread.Sleep(1000); };
        }
    }
}
