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

            getLinesFromDB();

            backgroundThread = new Thread(async () =>
            {
                while (true)
                {
                    await APIConnection.ReciveData();

                    await DatabaseManager.Submit();

                    updateLines();

                    await APIConnection.GetMessages();

                    await DatabaseManager.Submit();
                }
            });

            backgroundThread.IsBackground = true;

            backgroundThread.Start();

            while (true) { Thread.Sleep(1000); };
        }

        static void getLinesFromDB()
        {
            APIConnection.Lines = TypeConverter.lineListToDictionary(DatabaseManager.GetLines().Result);

            foreach (var line in APIConnection.Lines)
                line.Value.Vehicles = TypeConverter.vehicleListToDictionary(
                    DatabaseManager.GetVehicles(line.Value.Id.Value).Result);
        }

        static void updateLines()
        {
            foreach (var line in APIConnection.Lines.Values.Where(l => !l.Id.HasValue))
                line.Id = DatabaseManager.GetId(line);

            foreach (var line in APIConnection.Lines.Values)
                foreach (var vehicle in line.GetVehicles().Where(v => !v.Database_Id.HasValue))
                    vehicle.Database_Id = DatabaseManager.GetId(vehicle);
        }
    }
}
