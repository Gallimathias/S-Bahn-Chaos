
using System.Collections.Generic;
using System.Linq;
using VVS.API.SQL;

namespace VVS.API.Types
{
    public class Line
    {
        public string Name { get; private set; }
        public string CityCode { get; set; }
        public VehicleType VehicleType
        {
            get { return vehicleType; }
            private set
            {
                vehicleType = value;
            }
        }


        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                SetLine();
            }
        }
        public int Delay => averageDelay();

        private VehicleType vehicleType;
        private int id;

        public Line(string name, VehicleType type = VehicleType.None)
        {
            //Name = ushort.Parse(new string(name.Where(x => char.IsNumber(x)).ToArray()));
            Name = name;
            VehicleType = type;
            Vehicles = new Dictionary<string, Vehicle>();

        }

        public List<Vehicle> GetVehicles() => Vehicles.Values.ToList();

        public Dictionary<string, Vehicle> Vehicles { get; set; }

        public void SetLine()
        {
            foreach (var item in Vehicles)
            {
                item.Value.Line_id = id;
                item.Value.Type = vehicleType;
            }
        }

        private int averageDelay() => Vehicles.Count == 0
                ? 0
                : Vehicles.Values.Sum(v => v.Delay) / Vehicles.Count;

        public override string ToString()
        {
            return $"{VehicleType.ToString()} {Name}";
        }
    }

}
