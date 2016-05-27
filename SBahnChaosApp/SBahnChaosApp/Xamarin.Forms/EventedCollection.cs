using System;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms
{
    public class EventedList<T> : List<T>
    {
        public delegate void EventedItemEventHandler(object sender, T e);
        public event EventedItemEventHandler ItemAdded;
        public event EventedItemEventHandler ItemRemoved;

        public EventedList() { }

        public new void Add(T value)
        {
            base.Add(value);
            ItemAdded?.Invoke(this, value);
        }

        public new void Remove(T value)
        {
            base.Remove(value);
            ItemRemoved?.Invoke(this, value);
        }

        
    }
}
