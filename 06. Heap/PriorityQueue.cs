using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    internal class PriorityQueue<TElement>
    {
        private struct Node                     // 노드 구조체 정의
        {
            public TElement element;
            public int priority;
        }

        private List<Node> nodes;               // 노드의 자료형을 가진 리스트 정의

        public PriorityQueue()                  // 생성자 및 인스턴스 생성
        {
            this.nodes = new List<Node>();
        }

        public int Count { get { return nodes.Count; } }    // 배열의 크기 get으로 가져오기

        public void Eequeue(TElement element, int priority)     // enqueue 메소드 정의(노드의 우선순위과 값 매개변수)
        {
            Node newNode = new Node() { element = element, priority = priority};    // 노드 생성
            nodes.Add(newNode);                             // list형태이기 때문에 Add함수로 노드 추가
            int newNodeIndex = Count - 1;                   // 추가한 노드의 인덱스는 (크기 - 1)
            while(newNodeIndex > 0)     // 새로운 노드의 인덱스가 위쪽으로 가면서 힙상태 유지해야하기 때문에 0보다 클 때까지 진행
            {
                int parentIndex = GetParentIndex(newNodeIndex); // 추가한 노드의 부모노드 인덱스 구하기
                Node parentNode = nodes[parentIndex];           // 해당 인덱스를 가지는 부모노드

                if ( newNode.priority < parentNode.priority)    // 새 노드의 우선순위가 더 작다면 부모노드랑 바꿈
                {
                    nodes[newNodeIndex] = parentNode;
                    nodes[parentIndex] = newNode;
                    newNodeIndex = parentIndex;
                }
                else
                {
                    break;
                }
            }
        }

        public TElement Dequeue()       // 0순위 노드 출력 및 힙상태 유지시키는 메소드
        {
            TElement zeroNode = nodes[0].element;   // 출력될 처음 노드

            nodes.RemoveAt(0);                      // 처음 노드 제거
            Node newFirstNode = nodes[nodes.Count - 1]; // 새로운 첫번째노드 = 현재 마지막노드
            nodes[0] = newFirstNode;                // 첫번째인덱스에 넣음
            nodes.RemoveAt(nodes.Count - 1);        // 처음으로 복사한 마지막 노드 삭제

            int newIndex = 0;                       //  새로운 노드의 인덱스 = 0
            while(newIndex < nodes.Count)           // 새로운노드의 인덱스가 아래로 가면서 힙상태 유지해야하기 때문에 count보다 작을 때까지 진행
            {
                int leftChildIndex = GetLeftChildIndex(newIndex);   // 왼쪽 자식노드의 인덱스 구하기
                int rightChildIndex = GetRightChildIndex(newIndex); // 오른쪽 "

                // 자식이 둘 다 있는 경우
                if(nodes.Count > 0 && nodes.Count % 2 == 1)
                {   // 왼쪽, 오른쪽 자식 비교
                    int lessChildIndex = nodes[leftChildIndex].priority > nodes[rightChildIndex].priority
                        ? rightChildIndex : leftChildIndex;
                    if (nodes[lessChildIndex].priority < nodes[newIndex].priority)
                    {   // 더 작은거랑 비교해서 바꿈
                        nodes[newIndex] = nodes[lessChildIndex];
                        nodes[lessChildIndex] = newFirstNode;
                        newIndex = lessChildIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                // 자식이 한개만 있는 경우 == 왼쪽만 있는 경우
                else if (nodes.Count > 0 && nodes.Count % 2 == 0)
                {   // 비교해서 바꿈
                    if (nodes[newIndex].priority > nodes[leftChildIndex].priority)
                    {
                        nodes[newIndex] = nodes[leftChildIndex];
                        nodes[leftChildIndex] = newFirstNode;
                        newIndex = leftChildIndex;
                    }
                    else
                    {
                        break;
                    }
                }
                // 자식이 없는 경우
                else
                {
                    break;
                }
            }
            return zeroNode;
        }

        public TElement Peak()      // 가장 첫번째 노드 반환
        {
            TElement targetNode = nodes[0].element;
            return targetNode;
        }

        public int GetLeftChildIndex(int parentIndex)       // 왼쪽자식노드 인덱스 구하는 메소드
        {
            return (parentIndex * 2) + 1;
        }
        public int GetRightChildIndex(int parentIndex)      // 오른쪽자식노드 인덱스 구하는 메소드
        {
            return (parentIndex * 2) + 2;
        }
        public int GetParentIndex(int childIndex)           // 부모노드 인덱스 구하는 메소드
        {
            int parentIndex = (childIndex - 1) / 2;
            return parentIndex;
        }
    }

}
