using System;

namespace VVS.API.Types
{
    public class Stop : Station
    {
        public DateTime DateTime { get; set; }

        public Stop(string name, DateTime dateTime) : base(name)
        {
            DateTime = dateTime;
        }

    }
}