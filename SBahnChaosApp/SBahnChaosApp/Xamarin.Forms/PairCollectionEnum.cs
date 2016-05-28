using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Xamarin.Forms
{
    public class PairCollectionEnum<TKey, TValue> : IEnumerator<KeyValuePair<TKey, TValue>>
    {
        public KeyValuePair<TKey, TValue> Current { get { return Collection[position]; } }

        public PairCollection<TKey, TValue> Collection;
        int position;

        public PairCollectionEnum(PairCollection<TKey, TValue> collection)
        {
            position = -1;
            Collection = collection;
        }

        object IEnumerator.Current { get { return Current; } }

        public bool MoveNext()
        {
            position++;
            return (position < Collection.Count);
        }

        public void Reset() => position = -1;

        private IEnumerator getEnumerator() => this;

        #region IDisposable Support
        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                Collection = null;

                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
