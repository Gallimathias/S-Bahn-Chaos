using System;

namespace SBahnChaosApp.Core
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