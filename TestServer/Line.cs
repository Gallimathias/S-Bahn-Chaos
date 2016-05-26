
using System.Linq;

namespace TestServer
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

        public string Delay
        {
            get
            {
                if (delay == 0)
                    return "";

                return $"+{delay}";
            }
            set
            {
                delay = int.Parse(value);
            }
        }
        private int delay;

        public Line(string name, int delay, VehicleType type=VehicleType.None)
        {
            Name = ushort.Parse(new string(name.Where(x=>char.IsNumber(x)).ToArray()));
            VehicleType = type;
            Delay = delay.ToString();
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
