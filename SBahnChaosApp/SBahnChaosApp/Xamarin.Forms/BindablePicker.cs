using System;
using System.Collections;

namespace Xamarin.Forms
{
    public class BindablePicker : ObjectPicker
    {

        public BindablePicker()
        {
            SelectedIndexChanged += OnSelectedIndexChanged;
        }

        public static BindableProperty ItemsSourceProperty =
            //BindableProperty.Create<BindablePicker, IEnumerable>(o => o.ItemsSource, default(IEnumerable), propertyChanged: OnItemsSourceChanged);
            BindableProperty.Create(nameof(ItemsSource), typeof(IEnumerable), typeof(BindablePicker), default(IEnumerable), BindingMode.Default, null, propertyChanged: OnItemsSourceChanged);


        public static BindableProperty SelectedItemProperty =
            //BindableProperty.Create<BindablePicker, object>(o => o.SelectedItem, default(object), propertyChanged: OnSelectedItemChanged);
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(BindablePicker), default(object), BindingMode.Default, propertyChanged: OnSelectedItemChanged);

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        public new object SelectedItem
        {
            get { return GetValue(SelectedItemProperty); }
            set { SetValue(SelectedItemProperty, value); }
        }

        private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = bindable as BindablePicker;
            picker.Items.Clear();

            if (newValue != null)
                foreach (var item in (IEnumerable)newValue)
                    picker.Items.Add(item);
        }

        private void OnSelectedIndexChanged(object sender, EventArgs eventArgs)
        {
            if (SelectedIndex < 0 || SelectedIndex > base.Items.Count - 1)
            {
                SelectedItem = null;
            }
            else
            {
                SelectedItem = base.Items[SelectedIndex];
            }
        }

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = bindable as BindablePicker;

            if (newValue != null)
                picker.SelectedIndex = picker.Items.IndexOf(newValue.ToString());
        }
    }
}
