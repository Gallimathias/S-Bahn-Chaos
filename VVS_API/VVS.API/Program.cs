using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace VVS.API
{
    class Program
    {
        
        static Server server;
        static Thread background;
        static void Main(string[] args)
        {
            
            server = new Server();
            APIConnection.Data = new List<RawData>();
            APIConnection.Lines = new List<Line>();
            
            background = new Thread(() => {
                while (true)
                {
                    Console.Write("Get Data.....");
                    var t = APIConnection.ReadData();
                    t.Wait();
                    
                    lock (APIConnection.Data)
                    {
                        APIConnection.Data = t.Result;
                    }
                    Console.WriteLine("Finish");
                    Console.Write("Convert.....");
                    lock (APIConnection.Lines)
                    {
                        APIConnection.RawToLine();
                    }
                    Console.WriteLine("Complete");

                    foreach (var item in APIConnection.Lines.FindAll(l => l.Delay > 0))
                    {
                        Console.WriteLine($"{item.ToString()} +{item.Delay.ToString()}");
                    }

                    Thread.Sleep(5000);
                }
            });
            background.IsBackground = true;
            background.Start();

            

            server.Start(12344);

            
            while (true) { }

        }
    }
}
