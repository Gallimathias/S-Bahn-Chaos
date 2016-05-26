﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBahnChaosApp
{
    public class SubscribeView : ContentPage
    {
        public delegate void CloseWithOKEventHandler(object sender, Line vehicle);
        public event CloseWithOKEventHandler CloseWithOK;

        List<string> availableLines;
        Picker picker;
        Picker filter;
        Image image;
        Button confirmButton;

        public SubscribeView()
        {
            filter = new Picker();
            image = new Image();
            picker = new Picker();
            availableLines = getListofAvailableLines();
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

        public void FillPicker()
        {
            picker.Items.Clear();

            foreach (var vehicle in availableLines)
                picker.Items.Add(vehicle);
        }
        public void FillPicker(VehicleType type)
        {
            availableLines = getListofAvailableLines(type);

        }

        private void setupPage()
        {
            Title = "Add new Subscribing";

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

                CloseWithOK?.Invoke(this, getNewVehicle(type, name));

            };
        }

        private Line getNewVehicle(char type, string name)
        {
            throw new NotImplementedException();
        }

        private List<string> getListofAvailableLines()
        {
            var webRequest = WebRequest.Create("http://192.168.178.132:12344/lists/");
            webRequest.Method = "POST";
            byte[] buffer = Encoding.UTF8.GetBytes("get-all");
            var stream = webRequest.GetRequestStream();

            stream.Write(buffer, 0, buffer.Length);

            var response = webRequest.GetResponse();
          
            using (StreamReader reader = new StreamReader(response.GetResponseStream()))
            {
                var a = reader.ReadToEnd();
            }
            
            return new List<string>();
        }
        private List<string> getListofAvailableLines(VehicleType type)
        {
            throw new NotImplementedException();
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
