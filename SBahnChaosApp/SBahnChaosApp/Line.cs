﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBahnChaosApp
{
    public class Line : INotifyPropertyChanged
    {
        public ushort Name { get; set; }

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

        public Line(ushort name, VehicleType type = VehicleType.None)
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
                case VehicleType.None:
                    break;
                case VehicleType.SBahn:
                    Image = "ic_s.png";
                    break;
                case VehicleType.UBahn:
                    Image = "ic_u.png";
                    break;
                case VehicleType.Bus:
                    break;
                case VehicleType.RBahn:
                    break;
                case VehicleType.SEVBus:
                    break;
                case VehicleType.Zahnradbahn:
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

        public override string ToString()
        {
            return $"{VehicleType.ToString()} {Name.ToString()}";
        }
    }

    public enum VehicleType : byte
    {
        None,
        SBahn,
        UBahn,
        Bus,
        RBahn,
        SEVBus,
        Zahnradbahn
    }
}
