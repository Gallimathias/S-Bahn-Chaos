using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBahnChaosApp
{
    public class Vehicle : INotifyPropertyChanged
    {
        public string Name { get; set; }

        public string CityCode { get; private set; }
        public VehicleType VehicleType
        {
            get { return vehicleType; }
            private set
            {
                vehicleType = value;
                setImage();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VehicleType)));
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
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Delay)));
            }
        }
        private int delay;
        public List<Message> Messages
        {
            get { return messages; }
            set
            {
                messages = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Messages)));
            }
        }
        private List<Message> messages;
        public FileImageSource Image
        {
            get { return image; }
            private set
            {
                image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }
        private FileImageSource image;

        public event PropertyChangedEventHandler PropertyChanged;

        public Vehicle(string name, int delay)
        {
            Name = name;
            Delay = delay.ToString();
            Messages = new List<Message>();
        }
        public Vehicle(string name, int delay, VehicleType type)
        {
            Name = name;
            VehicleType = type;
            Delay = delay.ToString();
            Messages = new List<Message>();
        }

        private void setImage()
        {
            switch (vehicleType)
            {
                case VehicleType.S:
                    Image = "ic_s.png";
                    break;
                case VehicleType.U:
                    Image = "ic_u.png";
                    break;
                case VehicleType.B:
                    break;
                default:
                    break;
            }
        }

        public ObservableCollection<Message> MessagesToOBSCollection()
        {
            ObservableCollection<Message> tmp = new ObservableCollection<Message>();

            foreach (Message message in Messages)
                tmp.Add(message);

            return tmp;
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
