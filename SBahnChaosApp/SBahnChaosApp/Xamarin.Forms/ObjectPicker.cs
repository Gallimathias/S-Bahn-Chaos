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
            get
            {
                object item;
                dictionaryOfItems.TryGetValue(base.SelectedIndex, out item);
                return item;
            }
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

        private Dictionary<int, object> dictionaryOfItems;

        public ObjectPicker()
        {
            dictionaryOfItems = new Dictionary<int, object>();
            Items = new EventedList<object>();

            Items.ItemAdded += (s, e) => setBaseList();
            Items.ItemRemoved += (s, e) => setBaseList();
        }

        private void setBaseList()
        {
            base.Items.Clear();
            dictionaryOfItems.Clear();

            foreach (var item in Items)
            {
                base.Items.Add(item.ToString());
                int key = base.Items.Count - 1;
                dictionaryOfItems.Add(key, item);
            }
        }
    }
}
