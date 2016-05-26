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
        public delegate void CloseWithOKEventHandler(object sender, Line vehicle);
        public event CloseWithOKEventHandler CloseWithOK;
        List<Line> avaiblVehicles;
        Picker picker;
        Picker filter;
        Image image;
        Button confirmButton;

        public SubscribeView()
        {
            filter = new Picker();
            image = new Image();
            picker = new Picker();
            avaiblVehicles = new List<Line>();
            confirmButton = new Button { Text = "Confirm", TextColor = Color.Lime };
            
            setupPage();
            subscribe();
            FillPicker();


            Content = new StackLayout
            {
                Children = {
                    image, filter, picker, confirmButton
                }
            };
        }

        private void setupPage()
        {
            Title = "Add new Subscribing";

            
            avaiblVehicles.Add(new Line("60", 0, VehicleType.S));
            avaiblVehicles.Add(new Line("2", 0, VehicleType.U));
            avaiblVehicles.Add(new Line("3", 0, VehicleType.U));

            filter.Items.Add("All");
            filter.Items.Add("S-Bahn");
            filter.Items.Add("U-Bahn");
            filter.Items.Add("Bus");

            filter.SelectedIndex = 0;

            image.Source = $"ic_{filter.Items[filter.SelectedIndex][0].ToString().ToLower()}.png";
        }

        private void subscribe()
        {
            filter.SelectedIndexChanged += (s, o) =>
            {
                char type = filter.Items[filter.SelectedIndex][0];

                image.Source = $"ic_{type.ToString().ToLower()}.png";

                if (filter.SelectedIndex > 0)
                    FillPicker(getVehicleType(type));
                else
                    FillPicker();

            };

            confirmButton.Clicked += (s, o) =>
            {
                char type = picker.Items[picker.SelectedIndex][0];
                string name = picker.Items[picker.SelectedIndex].Substring(1);

                Line vehicle = avaiblVehicles.Find(v => v.VehicleType == getVehicleType(type) && v.Name == name);

                CloseWithOK?.Invoke(this, vehicle);

            };
        }

        public void FillPicker()
        {
            picker.Items.Clear();

            foreach (Line vehicle in avaiblVehicles)
                picker.Items.Add($"{vehicle.VehicleType.ToString().ToUpper()}{vehicle.Name}");
        }
        public void FillPicker(VehicleType type)
        {
            picker.Items.Clear();
            var tmp = avaiblVehicles.FindAll(v => v.VehicleType == type);

            foreach (Line vehicle in tmp)
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
