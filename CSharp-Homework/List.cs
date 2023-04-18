using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Homework
{
    public class List<T>
    {
        private const int DefaultCapacity = 10;     // 배열의 처음 크기 설정

        private T[] array;
        private int size;

        public List()
        {
            array = new T[DefaultCapacity];
            size = 0;
        }
        public int Capacity { get { return array.Length; } }
        public int Count { get { return size; } }

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= size) { throw new ArgumentOutOfRangeException("index"); }
                return array[index];
            }
            set
            {
                if (index < 0 || index >= size) { throw new ArgumentOutOfRangeException("index"); }
                array[index] = value;
            }
        }

        public void Add(T item)
        {
            if (size < array.Length)
            {
                array[size++] = item;
            }
            else
            {
                AddCapacity();
                array[size++] = item;
            }

        }
        public int IndexOf(T item)
        {
            return Array.IndexOf(array, item, 0, size);
        }
        public bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;

        }
        public void RemoveAt(int index)
        {
            if (index < 0 || index >= size)
            {
                throw new ArgumentOutOfRangeException("index");
            }
            size--;
            Array.Copy(array, index + 1, array, index, size - index);

        }
        public T? Find(Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i < size; i++)
            {
                if (match(array[i]))
                {
                    return array[i];
                }
            }
            return default(T);
        }
        public int FindIndex(Predicate<T> match)
        {
            if (match == null)
            {
                throw new ArgumentNullException();
            }
            for (int i = 0; i < size; i++)
            {
                if (match(array[i]))
                {
                    return i;
                }
            }
            return -1;
        }
        public void AddCapacity()
        {
            int addcapacity = array.Length * 2;
            T[] newArray = new T[addcapacity];
            Array.Copy(array, 0, newArray, 0, size);
            array = newArray;
        }
    }
}
