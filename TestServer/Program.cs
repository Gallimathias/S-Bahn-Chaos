using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace TestServer
{
    class Program
    {
        
        static Server server;
        static Thread background;
        static void Main(string[] args)
        {
            
            server = new Server();
            DataBaseAPI.Data = new List<RawData>();
            DataBaseAPI.Lines = new List<Line>();
            
            background = new Thread(() => {
                while (true)
                {
                    Console.Write("Get Data.....");
                    var t = DataBaseAPI.ReadData();
                    t.Wait();
                    
                    lock (DataBaseAPI.Data)
                    {
                        DataBaseAPI.Data = t.Result;
                    }
                    Console.WriteLine("Finish");
                    Console.Write("Convert.....");
                    lock (DataBaseAPI.Lines)
                    {
                        DataBaseAPI.RawToLine();
                    }
                    Console.WriteLine("Complete");
                    Thread.Sleep(30000);
                }
            });
            background.IsBackground = true;
            background.Start();

            

            server.Start(12344);

            
            while (true) { }

        }
    }
}
