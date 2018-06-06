using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BionCore.Tools;

namespace BionCore
{

    public class List<T, I> : List<T>, IList<I>
        where T : I
    {
        public List()
        {
            this.ObjectCaster = new BDefaultObjectCaster(true);
        }
        public List(IBObjectCaster objectCaster)
        {
            this.ObjectCaster = objectCaster ?? new BDefaultObjectCaster(true);
        }


        public List(IEnumerable<T> collection, IBObjectCaster objectCaster = null) : base(collection)
        {
            this.ObjectCaster = objectCaster ?? new BDefaultObjectCaster(true);

        }

        public List(int capacity, IBObjectCaster objectCaster = null) : base(capacity)
        {
            this.ObjectCaster = objectCaster ?? new BDefaultObjectCaster(true);
        }


        IBObjectCaster ObjectCaster { get; set; }
        protected T Cast(I item)
        {
            object result;
            if (!this.ObjectCaster.Cast(typeof(I), typeof(T), item, out result))
                BionTools.Throw($"Cannot convert from `{typeof(I).FullName}´ to `{typeof(T).FullName}´", text => new InvalidCastException(text));
            return (T)result;
        }



        I IList<I>.this[int index]
        {
            get
            {
                return base[index];
            }

            set
            {
                base[index] = this.Cast(value);
            }
        }

        int ICollection<I>.Count
        {
            get
            {
                return this.Count;
            }
        }

        bool ICollection<I>.IsReadOnly
        {
            get
            {
                return this.As<ICollection<T>>().IsReadOnly;
            }
        }

        void ICollection<I>.Add(I item)
        {
            this.Add(this.Cast(item));
        }

        void ICollection<I>.Clear()
        {
            this.Clear();
        }

        bool ICollection<I>.Contains(I item)
        {
            return this.Contains(this.Cast(item));
        }

        void ICollection<I>.CopyTo(I[] array, int arrayIndex)
        {
            this.As<List<T>>().Cast<I>().ToList().CopyTo(array, arrayIndex);
        }

        IEnumerator<I> IEnumerable<I>.GetEnumerator()
        {
            var enumerator = this.GetEnumerator();
            while (enumerator.MoveNext())
                yield return enumerator.Current;
        }

        int IList<I>.IndexOf(I item)
        {
            return this.IndexOf(this.Cast(item));
        }

        void IList<I>.Insert(int index, I item)
        {
            this.Insert(index, this.Cast(item));
        }

        bool ICollection<I>.Remove(I item)
        {
            return this.Remove(this.Cast(item));
        }

        void IList<I>.RemoveAt(int index)
        {
            this.RemoveAt(index);
        }

        public bool SequenceEqual(IEnumerable<I> second)
        {
            return this.As<IList<I>>().SequenceEqual(second);
        }

        public bool SequenceEqual(IEnumerable<T> second)
        {
            return this.As<IList<T>>().SequenceEqual(second);
        }

        public bool SequenceEqual(IEnumerable<I> second, IEqualityComparer<I> comparer)
        {
            return this.As<IList<I>>().SequenceEqual(second, comparer);
        }

        public bool SequenceEqual(IEnumerable<T> second, IEqualityComparer<T> comparer)
        {
            return this.As<IList<T>>().SequenceEqual(second, comparer);
        }
    }
}
