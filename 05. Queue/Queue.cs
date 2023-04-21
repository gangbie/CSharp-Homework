using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class Queue<T>
    {
        private const int defaultCapacity = 4;      // 배열의 디폴트 크기
        
        private T[] array;                          // 큐 클래스의 멤버변수들
        private int head;
        private int tail;

        public Queue()                              // 큐 생성자 및 초기화
        {
            array = new T[defaultCapacity+1];       // head와 tail을 사용하기 위해 배열을 +1크기로 만듦
            head = 0;
            tail = 0;
        }

        public int Count                            // 카운트 메서드
        {
            get
            {
                if( head <= tail)                   // 테일이 오른쪽에 있는 기본 형태
                {
                    return tail - head;
                }
                else                                // 순환 후 테일이 헤드보다 앞에 있는 경우
                {
                    return tail + (array.Length - head);
                }
            }
        }

        public void Clear()                         // 클리어 메서드
        {
            array = new T[defaultCapacity + 1];     // 초기값으로 세팅
            head = 0;
            tail = 0;
        }

        public void Enqueue(T item)                 // Enqueue 메서드 정의(값 추가)
        {
            if (IsFull())                           // 배열에 자리가 없으면 배열 늘림
            {
                Grow();
            }
            array[tail] = item;                     // 자리가 있으면 맨 뒤에 값 추가
            MoveNext(ref tail);                     // 추가 후 다음 인덱스로 이동
        }

        public T Dequeue()                          // Dequeue 메서드 정의(값 출력)
        {
            if (IsEmpty())                          // 비어있으면 오류메시지
            {
                throw new InvalidOperationException();
            }
            T result = array[head];                 // 맨 앞꺼 result 지역변수에 담음
            MoveNext(ref head);                     // 헤드 다음 인덱스로 이동
            return result;                          // result 반환
        }
        private void MoveNext(ref int index)        // MoveNext 인덱스 옮기는 메서드
        {                                           // 참조를 통해 주소의 값을 전달
            if (index == array.Length - 1)          // 인덱스가 마지막이면 처음으로 돌아가도록 아니면 ++
            {
                index = 0;
            }
            index++;
        }
        public T Peek()                             // 가장 앞의 값 반환하는 Peek 메서드 정의
        {
            if (IsEmpty())
            {
                throw new InvalidOperationException();
            }
            T result = array[head];
            return result;
        }

        public bool IsFull()                        // 배열이 Full인지 확인하는 메서드 정의
        {
            if(head == 0 && tail == array.Length - 1) { return true; }
            else if(head == tail + 1) { return true; }
            else { return false; }
        }
        public bool IsEmpty()                       // 배열이 비어있는지 확인하는 메서드 정의
        {
            if (head == tail) { return true; }
            else { return false; }
        }

        public void Grow()                          // 배열이 full일 때 배열 크기 늘려주는 메서드 정의
        {
            int newCapacity = array.Length * 2;     // 새로운 용량을 이전 크기의 2배로 늘림
            T[] newArray = new T[newCapacity + 1];
            if (!IsEmpty())
            {
                if (head < tail) 
                {
                    Array.Copy(array, newArray, Count);
                }
                else                        // tail이 head 앞에 있는 경우 헤드부터 끝까지 복사 하고
                {                           // 원본 배열의 처음부터 tail까지 추가 복사 필요
                    Array.Copy(array, head, newArray, 0, array.Length - head);
                    Array.Copy(array, 0, newArray, array.Length - head, tail);
                }
                head = 0;
                tail = Count;
            }
            array = newArray;               // 배열 변경
        }
    }
}
