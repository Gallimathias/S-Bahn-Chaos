using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        ObservableCollection<Line> availableLines;
        ExtendedPicker picker;
        ExtendedPicker filter;
        Image image;
        Button confirmButton;

        public SubscribeView()
        {
            image = new Image();
            picker = new ExtendedPicker();
            filter = new ExtendedPicker();
            availableLines = getListofAvailableLines();
            confirmButton = new Button { Text = "Confirm", TextColor = Color.Lime };

            setupPage();
            subscribe();

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

            filter.SelectedIndex = 1;
            var a = filter.SelectedItem;
            
            image.Source = $"ic_{a.Value.ToString().ToLower()}.png";
        }

        private void subscribe()
        {
            filter.SelectedIndexChanged += (s, o) =>
            {
                var type = filter.SelectedItem.Value;

                image.Source = $"ic_{type.ToString().ToLower()}.png";
            };

            //confirmButton.Clicked += (s, o) =>
            //    CloseWithOK?.Invoke(this, ((Line)picker.SelectedItem));
        }

        private ObservableCollection<Line> getListofAvailableLines()
        {
            var webRequest = WebRequest.Create("http://192.168.178.132:12344/lists/");
            webRequest.Method = "POST";
            byte[] buffer = Encoding.UTF8.GetBytes("get-all");
            var stream = webRequest.GetRequestStream();

            stream.Write(buffer, 0, buffer.Length);
            var response = webRequest.GetResponse();
            byte[] buff = new byte[64 * 1024];
            ObservableCollection<Line> lines = new ObservableCollection<Line>();//(response.ContentLength / sizeof(ushort));
            using (BinaryReader reader = new BinaryReader(response.GetResponseStream()))
            {
                for (int i = 0; i < response.ContentLength / 2; i++)
                {
                    var tmp = reader.ReadUInt16();
                    VehicleType type = (VehicleType)(tmp >> 13);
                    ushort name = (ushort)(tmp & 0x1FFF);
                    lines.Add(new Line(name, type));
                }
                reader.Read(buff, 0, buff.Length);
            }

            return lines;
        }
        private ObservableCollection<Line> getListofAvailableLines(VehicleType type)
        {
            throw new NotImplementedException();
        }

        private VehicleType getVehicleType(char type)
        {
            switch (type)
            {
                case 'S':
                    return VehicleType.SBahn;
                case 'U':
                    return VehicleType.UBahn;
                case 'B':
                    return VehicleType.Bus;
                case 'R':
                    return VehicleType.RBahn;
                case 'V':
                    return VehicleType.SEVBus;
                case 'Z':
                    return VehicleType.Zahnradbahn;
                default:
                    return VehicleType.None;
            }
        }
    }
}
