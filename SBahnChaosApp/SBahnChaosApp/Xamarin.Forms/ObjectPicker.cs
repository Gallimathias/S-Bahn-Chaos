using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Xamarin.Forms
{
    public class ObjectPicker : Picker
    {
        public object SelectedItem
        {
            get; set;
        }

        public new EventedList<object> Items
        {
            get { return Items; }
            set
            {
                Items = value;
                setBaseList();
            }
        }
        
        public ObjectPicker() : base()
        {
            Items = new EventedList<object>();

            Items.ItemAdded += (s, e) => setBaseList();
            Items.ItemRemoved += (s, e) => setBaseList();

            base.SelectedIndexChanged += (s, e) =>
            {
                SelectedItem = Items[SelectedIndex];
            };
        }

        private void setBaseList()
        {
            base.Items.Clear();

            foreach (var item in Items)
                base.Items.Add(item.ToString());
        }
    }
}
