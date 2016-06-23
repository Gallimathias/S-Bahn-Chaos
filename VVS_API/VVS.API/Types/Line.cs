
using System.Collections.Generic;
using System.Linq;

namespace VVS.API.Types
{
    public class Line
    {
        public ushort Name { get; set; }

        public string CityCode { get; private set; }
        public VehicleType VehicleType
        {
            get { return vehicleType; }
            private set
            {
                vehicleType = value;
            }
        }
        private VehicleType vehicleType;

        public int Delay { get { return averageDelay(); } }

        public Line(string name, VehicleType type = VehicleType.None)
        {
            Name = ushort.Parse(new string(name.Where(x => char.IsNumber(x)).ToArray()));
            VehicleType = type;
            Vehicles = new Dictionary<string, Vehicle>();
        }

        public Dictionary<string, Vehicle> Vehicles { get; set; }

        private int averageDelay()
        {
            int sum = 0;
            foreach (var item in Vehicles)
                sum += item.Value.Delay;

            if (Vehicles.Count > 0)
                return sum / Vehicles.Count;
            else
                return 0;
        }

        public override string ToString()
        {
            return $"{VehicleType.ToString()} {Name}";
        }
    }



    public enum VehicleType : byte
    {
        None,
        S,
        U,
        B,
        R,
        SEV,
        Z
    }
}
