using SBahnChaosApp.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SBahnChaosApp.Views
{
    public class SubscribeView : ContentPage
    {
        public delegate void CloseWithOKEventHandler(object sender, Line vehicle);
        public event CloseWithOKEventHandler CloseWithOK;

        EventedList<Line> availableLines;
        ExtendedPicker picker;
        ExtendedPicker filter;
        Image image;
        Button confirmButton;

        public SubscribeView()
        {
            image = new Image();
            picker = new ExtendedPicker();
            filter = new ExtendedPicker();
            availableLines = new EventedList<Line>();
            confirmButton = new Button { Text = "Confirm", TextColor = Color.Lime };

            setupPage();
            subscribe();
            getListofAvailableLines();

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

            filter.Items.Add("All", VehicleType.None);
            filter.Items.Add("S-Bahn", VehicleType.SBahn);
            filter.Items.Add("U-Bahn", VehicleType.UBahn);
            filter.Items.Add("R-Bahn", VehicleType.RBahn);
            filter.Items.Add("Bus", VehicleType.Bus);
            filter.Items.Add("SEV-Bus", VehicleType.SEVBus);
            filter.Items.Add("Zahnradbahn", VehicleType.Zahnradbahn);

            filter.SelectedIndex = 0;
            var a = filter.SelectedItem;

            image.Source = $"ic_{a.Value.ToString().ToLower()}.png";
        }

        private void subscribe()
        {
            filter.SelectedIndexChanged += (s, o) =>
            {
                var type = filter.SelectedItem.Value;

                image.Source = $"ic_{type.ToString().ToLower()}.png";
                fillPicker((VehicleType)filter.SelectedItem.Value);
            };

            availableLines.ItemAdded += (s, e) => { picker.Items.Add(e.ToString(), e); };

            confirmButton.Clicked += (s, o) =>
                CloseWithOK?.Invoke(this, (Line)picker.SelectedItem.Value);
        }

        private void getListofAvailableLines()
        {
            var webRequest = WebRequest.Create("http://192.168.178.132:12344/lists/");
            webRequest.Method = "POST";
            byte[] buffer = Encoding.UTF8.GetBytes("get-all");
            var stream = webRequest.GetRequestStream();

            stream.Write(buffer, 0, buffer.Length);
            var response = webRequest.GetResponse();
            byte[] buff = new byte[64 * 1024];
            //EventedList<Line> lines = new EventedList<Line>();//(response.ContentLength / sizeof(ushort));
            availableLines.Clear();
            using (BinaryReader reader = new BinaryReader(response.GetResponseStream()))
            {
                for (int i = 0; i < response.ContentLength / 2; i++)
                {
                    var tmp = reader.ReadUInt16();
                    VehicleType type = (VehicleType)(tmp >> 13);
                    ushort name = (ushort)(tmp & 0x1FFF);
                    availableLines.Add(new Line(name, type));
                }
                reader.Read(buff, 0, buff.Length);
            }

        }

        private void fillPicker(VehicleType type)
        {
            List<Line> tmp;
            if (type != VehicleType.None)
                tmp = availableLines.FindAll(l => l.VehicleType == type).OrderBy(l => l.Name).ToList();
            else
                tmp = availableLines.OrderBy(l => l.VehicleType).ToList();
            

            picker.Clear();

            foreach (var item in tmp)
                picker.Items.Add(item.ToString(), item);
        }

    }
}
