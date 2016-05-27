using System.Collections.Generic;

namespace Xamarin.Forms
{
    public class PairItemEventArgs<TKey, TValue>
    {
        public KeyValuePair<TKey, TValue> Item { get; private set; }

        public int Index { get; private set; }

        public PairItemEventArgs(int index, KeyValuePair<TKey, TValue> item)
        {
            Item = item;
            Index = index;
        }
    }
}