using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBahnChaosApp
{
    public class SubscribeView : ContentPage
    {
        public delegate void CloseWithOKEventHandler(object sender, Vehicle vehicle);
        public event CloseWithOKEventHandler CloseWithOK;
        List<Vehicle> avaiblVehicles;
        Picker picker;
        public SubscribeView()
        {
            Title = "Add new Subscribing";

            avaiblVehicles = new List<Vehicle>();
            avaiblVehicles.Add(new Vehicle("60", 0, VehicleType.S));
            avaiblVehicles.Add(new Vehicle("2", 0, VehicleType.U));
            avaiblVehicles.Add(new Vehicle("3", 0, VehicleType.U));

            Picker filter = new Picker();
            Image image = new Image();

            filter.Items.Add("All");
            filter.Items.Add("S-Bahn");
            filter.Items.Add("U-Bahn");
            filter.Items.Add("Bus");

            filter.SelectedIndex = 0;

            image.Source = $"ic_{filter.Items[filter.SelectedIndex][0].ToString().ToLower()}.png";

            filter.SelectedIndexChanged += (s, o) => 
            {
                char type = filter.Items[filter.SelectedIndex][0];

                image.Source = $"ic_{type.ToString().ToLower()}.png";

                if (filter.SelectedIndex > 0)
                    fillPicker(getVehicleType(type));
                else
                    fillPicker();
                
            };
            picker = new Picker();

            fillPicker();

            Button button = new Button { Text = "Confirm", TextColor = Color.Lime };
            button.Clicked += (s, o) =>
            {
                char type = picker.Items[picker.SelectedIndex][0];
                string name = picker.Items[picker.SelectedIndex].Substring(1);

                Vehicle vehicle = avaiblVehicles.Find(v => v.VehicleType == getVehicleType(type) && v.Name == name);

                CloseWithOK?.Invoke(this, vehicle);

            };

            Content = new StackLayout
            {
                Children = {
                    image, filter, picker, button
                }
            };
        }

        public void fillPicker()
        {
            picker.Items.Clear();

            foreach (Vehicle vehicle in avaiblVehicles)
                picker.Items.Add($"{vehicle.VehicleType.ToString().ToUpper()}{vehicle.Name}");
        }
        public void fillPicker( VehicleType type)
        {
            picker.Items.Clear();
            var tmp = avaiblVehicles.FindAll(v => v.VehicleType == type);

            foreach (Vehicle vehicle in tmp)
                picker.Items.Add($"{vehicle.VehicleType.ToString().ToUpper()}{vehicle.Name}");
        }

        private VehicleType getVehicleType(char type)
        {
            switch (type)
            {
                case 'S':
                   return VehicleType.S;
                case 'U':
                    return VehicleType.U;
                case 'B':
                    return VehicleType.B;
                default:
                    return VehicleType.None;
            }
        }
    }
}
