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
            DatabaseManager.Connect();
            APIConnection.BeginReciveData();
            
            while (true) {
                Console.ReadLine();
                var l = APIConnection.Lines.ToList();
                DatabaseManager.InsertLines(l);
                DatabaseManager.Submit();
                Console.WriteLine("Finish Database Insert");
                APIConnection.Lines = DatabaseManager.GetIds(l);
                Console.WriteLine("Get all Id's");
                
            };
        }
    }
}
