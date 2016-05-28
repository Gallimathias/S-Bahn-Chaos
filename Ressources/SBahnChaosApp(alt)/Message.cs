using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBahnChaosApp
{
    public class Message : INotifyPropertyChanged
    {
        public DateTime DateTime { get; set; }

        public Stop fromStop
        {
            get { return fromStop; }
            set
            {
                fromStopName = value.Name;
                fromStopTime = value.DateTime.ToString("HH:mm");
            }
        }
        private string fromStopName;
        private string fromStopTime;

        public Stop toStop
        {
            get { return toStop; }
            set
            {
                toStopName = value.Name;
                toStopTime = value.DateTime.ToString("HH:mm");
            }
        }
        private string toStopName;
        private string toStopTime;

        public string Text { get; set; }
        public string Header { get { return $"von {fromStopName} {fromStopTime} \nnach {toStopName} {toStopTime}"; } }

        public event PropertyChangedEventHandler PropertyChanged;
        public Message(string text, DateTime dateTime)
        {
            DateTime = dateTime;
            Text = text;
        }
        
    }

    public enum MessageType
    {
        delay,
        fault,
        information,
        system
    }
}
