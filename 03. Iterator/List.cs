using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iterator
{
    public class List<T> : IEnumerable<T>
    {
        private const int DefaultCapacity = 10;     // 배열의 처음 크기 설정

        private T[] array;
        private int size;

        public List()
        {
            array = new T[DefaultCapacity];         // array 생성
            size = 0;                               // 처음 크기는 0
        }
        public int Capacity { get { return array.Length; } }    // 리스트의 capacity
        public int Count { get { return size; } }               // 리스트의 크기 

        public T this[int index]                    // 리스트의 요소 검색 및 변경
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

        public void Add(T item)                     // Add 기능 구현
        {
            if (size < array.Length)                // 현재 크기가 배열의 크기보다 작으면 item 추가
            {
                array[size++] = item;
            }
            else                                    // 현재 크기가 배열의 크기보다 크거나 같으면 AddCapacity 함수 실행
            {
                AddCapacity();
                array[size++] = item;
            }

        }
        public int IndexOf(T item)                  // IndexOf 기능을 배열의 기능으로 가져옴
        {
            return Array.IndexOf(array, item, 0, size);
        }
        public bool Remove(T item)                  // Remove 기능 구현 반환형은 bool type
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

        public IEnumerator<T> GetEnumerator()
        {
            return new Enumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return new Enumerator(this);
        }

        public struct Enumerator : IEnumerator<T>
        {
            private List<T> list;
            private int index;
            private T current;

            internal Enumerator(List<T> list)
            {
                this.list = list;
                this.index = 0;
                this.current = default(T);
            }

            public T Current { get { return current; } }

            object IEnumerator.Current
            {
                get
                {
                    if(index < 0 || index >= list.Count)
                    {
                        throw new InvalidOperationException();
                    }
                    return Current;
                }
            }
            public void Dispose() { }

            public bool MoveNext()
            {
                if(index < list.Count)
                {
                    current = list[index++];
                    return true;
                }
                else
                {
                    current = default(T);
                    index = list.Count;
                    return false;
                }
            }

            public void Reset()
            {
                index = 0;
                current = default(T);
            }

        }
    }
}
