using System;
using System.Collections.Generic;

namespace Xamarin.Forms
{
    public class ExtendedPicker : Picker
    {
        public new PairCollection<string, object> Items { get; set; }
        public KeyValuePair<string, object> SelectedItem { get; set; }

        public ExtendedPicker() : base()
        {
            Items = new PairCollection<string, object>();

            Items.ItemAdded += (s, e) => setBaseList();
            Items.ItemRemoved += (s, e) => setBaseList();
            base.SelectedIndexChanged += ExtendedPicker_SelectedIndexChanged;
        }

        public void Clear()
        {
            if (Items.Count > 0)
                Items.Clear();

            if (base.Items.Count > 0)
                base.Items?.Clear();
        }

        private void ExtendedPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedItem = Items[base.SelectedIndex];
        }

        private void setBaseList()
        {
            base.Items.Clear();

            foreach (var item in Items)
                base.Items.Add(item.Key);
        }
    }
}
