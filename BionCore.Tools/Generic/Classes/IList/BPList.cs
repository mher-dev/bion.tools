using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;

namespace BionCore.Tools
{
    public class BPList<T> : IList<T>, IList, IReadOnlyList<T>, IReadOnlyCollection<T>, IBPList<T>
    {
        #region -------- Constructors --------
        public BPList()
        {
            this.InnerObject = new List<T>();
        }

        public BPList(int capacity)
        {
            this.InnerObject = new List<T>(capacity);
        }

        public BPList(IEnumerable<T> collection)
        {
            this.InnerObject = new List<T>(collection);
        }
        #endregion [-------- Constructors --------]

        private List<T> InnerObject { get; set; }

        #region -------- IList --------

        public virtual T this[int index]
        {
            get
            {
                return ((IList<T>)InnerObject)[index];
            }

            set
            {
                ((IList<T>)InnerObject)[index] = value;
            }
        }

        public virtual int Count
        {
            get
            {
                return ((IList<T>)InnerObject).Count;
            }
        }

        public virtual bool IsReadOnly
        {
            get
            {
                return ((IList<T>)InnerObject).IsReadOnly;
            }
        }

        public virtual bool IsFixedSize
        {
            get
            {
                return ((IList)InnerObject).IsFixedSize;
            }
        }

        public virtual object SyncRoot
        {
            get
            {
                return ((IList)InnerObject).SyncRoot;
            }
        }

        public virtual bool IsSynchronized
        {
            get
            {
                return ((IList)InnerObject).IsSynchronized;
            }
        }

        object IList.this[int index]
        {
            get
            {
                return ((IList)InnerObject)[index];
            }

            set
            {
                ((IList)InnerObject)[index] = value;
            }
        }

        public virtual void Add(T item)
        {
            ((IList<T>)InnerObject).Add(item);
        }

        public virtual void Clear()
        {
            ((IList<T>)InnerObject).Clear();
        }

        public virtual bool Contains(T item)
        {
            return ((IList<T>)InnerObject).Contains(item);
        }

        public virtual void CopyTo(T[] array, int arrayIndex)
        {
            ((IList<T>)InnerObject).CopyTo(array, arrayIndex);
        }

        public virtual IEnumerator<T> GetEnumerator()
        {
            return ((IList<T>)InnerObject).GetEnumerator();
        }

        public virtual int IndexOf(T item)
        {
            return ((IList<T>)InnerObject).IndexOf(item);
        }

        public virtual void Insert(int index, T item)
        {
            ((IList<T>)InnerObject).Insert(index, item);
        }

        public virtual bool Remove(T item)
        {
            return ((IList<T>)InnerObject).Remove(item);
        }

        public virtual void RemoveAt(int index)
        {
            ((IList<T>)InnerObject).RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IList<T>)InnerObject).GetEnumerator();
        }



        public virtual List<T> ToList()
        {
            return InnerObject.ToList();
        }

        public int Add(object value)
        {
            return ((IList)InnerObject).Add(value);
        }

        public virtual bool Contains(object value)
        {
            return ((IList)InnerObject).Contains(value);
        }

        public virtual int IndexOf(object value)
        {
            return ((IList)InnerObject).IndexOf(value);
        }

        public virtual void Insert(int index, object value)
        {
            ((IList)InnerObject).Insert(index, value);
        }

        public virtual void Remove(object value)
        {
            ((IList)InnerObject).Remove(value);
        }

        public virtual void CopyTo(Array array, int index)
        {
            ((IList)InnerObject).CopyTo(array, index);
        }

        #endregion [-------- IList --------]



        public virtual void AddRange(IEnumerable<T> collection)
        {
            this.InnerObject.AddRange(collection);
        }
        
        public virtual ReadOnlyCollection<T> AsReadOnly()
        {
            return this.InnerObject.AsReadOnly();
        }

        public virtual int BinarySearch(T item)
        {
            return this.InnerObject.BinarySearch(item);
        }


        public virtual int BinarySearch(T item, IComparer<T> comparer)
        {
            return this.InnerObject.BinarySearch(item, comparer);

        }


        public virtual int BinarySearch(int index, int count, T item, IComparer<T> comparer)
        {
            return this.InnerObject.BinarySearch(index, count, item, comparer);
        }

        public virtual List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter)
        {
            return this.InnerObject.ConvertAll(converter);

        }


        public virtual void CopyTo(T[] array)
        {
            this.InnerObject.CopyTo(array);
        }



        public virtual void CopyTo(int index, T[] array, int arrayIndex, int count)
        {
            this.InnerObject.CopyTo(index, array, arrayIndex, count);

        }

        public virtual bool Exists(Predicate<T> match)
        {
            return this.InnerObject.Exists(match);
        }


        public virtual T Find(Predicate<T> match)
        {
            return this.InnerObject.Find(match);

        }

        public virtual List<T> FindAll(Predicate<T> match)
        {
            return this.InnerObject.FindAll(match);
        }


        public virtual int FindIndex(Predicate<T> match)
        {
            return this.InnerObject.FindIndex(match);
        }


        public virtual int FindIndex(int startIndex, Predicate<T> match)
        {
            return this.InnerObject.FindIndex(startIndex, match);
        }


        public virtual int FindIndex(int startIndex, int count, Predicate<T> match)
        {
            return this.InnerObject.FindIndex(startIndex, count, match);

        }


        public virtual T FindLast(Predicate<T> match)
        {
            return this.InnerObject.FindLast(match);

        }


        public virtual int FindLastIndex(Predicate<T> match)
        {
            return this.InnerObject.FindLastIndex(match);
        }


        public virtual int FindLastIndex(int startIndex, Predicate<T> match)
        {
            return this.InnerObject.FindLastIndex(startIndex, match);
        }


        public int FindLastIndex(int startIndex, int count, Predicate<T> match)
        {
            return this.InnerObject.FindLastIndex(startIndex, count, match);
        }


        public virtual void ForEach(Action<T> action)
        {
            this.InnerObject.ForEach(action);
        }



        public virtual List<T> GetRange(int index, int count)
        {
            return this.InnerObject.GetRange(index, count);
        }




        public virtual int IndexOf(T item, int index)
        {
            return this.InnerObject.IndexOf(item, index);
        }


        public virtual int IndexOf(T item, int index, int count)
        {
            return this.InnerObject.IndexOf(item, index, count);
        }

        public virtual void InsertRange(int index, IEnumerable<T> collection)
        {
            this.InnerObject.InsertRange(index, collection);
        }


        public virtual int LastIndexOf(T item)
        {
            return this.InnerObject.LastIndexOf(item);
        }


        public virtual int LastIndexOf(T item, int index)
        {
            return this.InnerObject.LastIndexOf(item, index);
        }

        public virtual int LastIndexOf(T item, int index, int count)
        {
            return this.InnerObject.LastIndexOf(item, index, count);
        }



        public virtual int RemoveAll(Predicate<T> match)
        {
            return this.InnerObject.RemoveAll(match);
        }


        public virtual void RemoveRange(int index, int count)
        {
            this.InnerObject.RemoveRange(index, count);
        }


        public virtual void Reverse()
        {
            this.InnerObject.Reverse();
        }


        public virtual void Reverse(int index, int count)
        {
            this.InnerObject.Reverse(index, index);
        }


        public virtual void Sort()
        {
            this.InnerObject.Sort();
        }


        public virtual void Sort(Comparison<T> comparison)
        {
            this.InnerObject.Sort(comparison);
        }


        public virtual void Sort(IComparer<T> comparer)
        {
            this.InnerObject.Sort(comparer);
        }


        public virtual void Sort(int index, int count, IComparer<T> comparer)
        {
            this.InnerObject.Sort(index, count, comparer);
        }


        public virtual T[] ToArray()
        {
            return this.InnerObject.ToArray();
        }


        public virtual void TrimExcess()
        {
            this.InnerObject.TrimExcess();
        }


        public virtual bool TrueForAll(Predicate<T> match)
        {
            return this.InnerObject.TrueForAll(match);
        }

    }
}
