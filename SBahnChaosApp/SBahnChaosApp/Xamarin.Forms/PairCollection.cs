using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Xamarin.Forms
{
    public class PairCollection<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        public int Count { get { return mainDictionary.Count; } }
        private Dictionary<int, KeyValuePair<TKey, TValue>> mainDictionary;

        public KeyValuePair<TKey, TValue> this[int i]
        {
            get
            {
                KeyValuePair<TKey, TValue> value;
                mainDictionary.TryGetValue(i, out value);
                return value;
            }
            set
            {
                mainDictionary.Remove(i);
                mainDictionary.Add(i, value);
            }
        }

        public delegate void PairItemEventHandler(object sender, PairItemEventArgs<TKey, TValue> e);
        public event PairItemEventHandler ItemAdded;
        public event PairItemEventHandler ItemRemoved;

        public PairCollection()
        {
            mainDictionary = new Dictionary<int, KeyValuePair<TKey, TValue>>();
        }

        public void Add(KeyValuePair<TKey, TValue> keyValuePair)
        {
            mainDictionary.Add(Count, keyValuePair);

            ItemAdded?.Invoke(this, new PairItemEventArgs<TKey, TValue>(Count, keyValuePair));
        }
        public void Add(TKey keyValue, TValue value)
        {
            Add(new KeyValuePair<TKey, TValue>(keyValue, value));
        }

        public void Remove(int index)
        {
            var value = mainDictionary.First(c => c.Key == index).Value;

            mainDictionary.Remove(index);
            ItemRemoved?.Invoke(this, new PairItemEventArgs<TKey, TValue>(index, value));
        }

        public void Clear()
        {
            if (Count == 0)
                return;

            mainDictionary.Clear();
        }

        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() =>
            new PairCollectionEnum<TKey, TValue>(this);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }


}