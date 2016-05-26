
namespace TestServer
{
    public class Line
    {
        public string Name { get; set; }

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


        public Line(string name, int delay)
        {
            Name = name;
            Delay = delay.ToString();
        }
        public Line(string name, int delay, VehicleType type)
        {
            Name = name;
            VehicleType = type;
            Delay = delay.ToString();
        }

        public override string ToString()
        {
            return $"{VehicleType.ToString()} {Name}";
        }
    }

    public enum VehicleType
    {
        None,
        S,
        U,
        B
    }
}
