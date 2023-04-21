using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{ 
    internal class AdapterStack<T>
    {
        private List<T> container;                      // 어뎁터스택의 필드 일반화 리스트

        public AdapterStack()                           // 생성자
        {
            container = new List<T>();                  // 컨테이너 리스트 인스턴스 생성
        }
        public void Push(T item)                        // push 메서드 구현
        {
            container.Add(item);                        // 리스트의 Add와 기능이 같음
        }
        public T Pop()                                  // Pop 메서드 구현
        {
            T item = container[container.Count - 1];    // 지역변수 아이템에 리스트 마지막 값을 담음
            container.RemoveAt(container.Count - 1);    // 마지막 값 지움
            return item;                                
        }
        public T Peek()
        {
            return container[container.Count - 1];
        }
    }
}
