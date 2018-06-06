using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BionCore.Tools
{
    public interface IBPList<T>: IList<T>, IList, IReadOnlyList<T>, IReadOnlyCollection<T>
    {
        void AddRange(IEnumerable<T> collection);
        ReadOnlyCollection<T> AsReadOnly();
        int BinarySearch(T item);
        int BinarySearch(T item, IComparer<T> comparer);
        int BinarySearch(int index, int count, T item, IComparer<T> comparer);
        List<TOutput> ConvertAll<TOutput>(Converter<T, TOutput> converter);
        void CopyTo(T[] array);
        void CopyTo(int index, T[] array, int arrayIndex, int count);
        bool Exists(Predicate<T> match);
        T Find(Predicate<T> match);
        List<T> FindAll(Predicate<T> match);
        int FindIndex(Predicate<T> match);
        int FindIndex(int startIndex, Predicate<T> match);
        int FindIndex(int startIndex, int count, Predicate<T> match);
        T FindLast(Predicate<T> match);
        int FindLastIndex(Predicate<T> match);
        int FindLastIndex(int startIndex, Predicate<T> match);
        int FindLastIndex(int startIndex, int count, Predicate<T> match);
        void ForEach(Action<T> action);
        List<T> GetRange(int index, int count);
        int IndexOf(T item, int index);
        int IndexOf(T item, int index, int count);
        void InsertRange(int index, IEnumerable<T> collection);
        int LastIndexOf(T item);
        int LastIndexOf(T item, int index);
        int LastIndexOf(T item, int index, int count);
        int RemoveAll(Predicate<T> match);
        void RemoveRange(int index, int count);
        void Reverse();
        void Reverse(int index, int count);
        void Sort();
        void Sort(IComparer<T> comparer);
        void Sort(Comparison<T> comparison);
        void Sort(int index, int count, IComparer<T> comparer);
        T[] ToArray();
        List<T> ToList();
        void TrimExcess();
        bool TrueForAll(Predicate<T> match);
    }
}