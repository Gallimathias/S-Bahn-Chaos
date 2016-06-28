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

            backgroundThread = new Thread(async () =>
            {
                while (true)
                {
                    await APIConnection.ReciveData();

                    foreach (var line in APIConnection.Lines)
                    {
                        var id = DatabaseManager.GetId(line.Value);

                        lock (APIConnection.Lines)
                            line.Value.Id = id;
                    }

                    await DatabaseManager.Submit();
                }
            });

            backgroundThread.IsBackground = true;

            backgroundThread.Start();

            while (true) { };
        }
    }
}
