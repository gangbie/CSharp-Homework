using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DataStructure
{
    public class LinkedListNode<T>
    {
        internal LinkedList<T> list;
        internal LinkedListNode<T> prev;
        internal LinkedListNode<T> next;
        private T item;

        public LinkedListNode(T value)
        {
            this.list = null;
            this.next = null;
            this.prev = null;
            this.item = value;
        }
        public LinkedListNode(LinkedList<T> list, T value)
        {
            this.list = list;
            this.next = null;
            this.prev = null;
            this.item = value;
        }
        public LinkedListNode(LinkedList<T> list, LinkedListNode<T> prev, LinkedListNode<T> next, T value)
        {
            this.list = list;
            this.next = next;
            this.prev = prev;
            this.item = value;
        }
        public LinkedList<T> List { get { return list; } }
        public LinkedListNode<T> Prev { get { return prev; } }
        public LinkedListNode<T> Next { get { return next; } }
        public T Item { get { return item; } set { item = value; } }

    }
    // AddFirst, AddLast, AddBefore, AddAfter, Remove(T value), Remove(node), Find
    public class LinkedList<T>
    {
        private LinkedListNode<T> head;
        private LinkedListNode<T> tail;
        private int count;
        public LinkedList()
        {
            this.head = null;
            this.tail = null;
            count = 0;
        }
        public LinkedListNode<T> First { get { return head; } }
        public LinkedListNode<T> Last { get { return tail; } }
        public int Count { get { return count; } }

        public LinkedListNode<T> AddFirst(T value)
        {
            // 1. 새로운 노드 만들기
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            // 2. 헤드 또는 테일 지정
            if(head != null)
            {
                head.prev = newNode;
                head = newNode;
            }
            else
            {
                head = newNode;
                tail = newNode;
            }
            // 3. 크기 증가
            count++;
            return newNode;
        }
        public LinkedListNode<T> AddLast(T value)
        {
            // 1. 새로운 노드 만들기
            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            // 2. 헤드 또는 테일 지정
            if (tail != null)
            {
                tail.next = newNode;
                tail = newNode;
            }
            else
            {
                head = newNode;
                tail = newNode;
            }
            // 3. 크기 증가
            count++;
            return newNode;
        }
        public LinkedListNode<T> AddBefore(LinkedListNode<T> node, T value)
        {
            // 예외1 : node가 연결리스트에 포함된 노드가 아닌 경우
            if (node.list != this)
                throw new InvalidOperationException();
            // 예외2 : 노드가 null인 경우
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            // 1. 만약에 node.prev에 값이 있으면 중간에 넣는거니까 관계 4개 다 바꿈
            if(node.prev != null)
            {
                newNode.prev = node.prev;
                newNode.next = node;
                node.prev = newNode;
                node.prev.next = newNode;
            }
            else
            {
                newNode.next = node;
                node.prev = newNode;
                head = newNode;
            }
            count++;
            return newNode;
        }
        public LinkedListNode<T> AddAfter(LinkedListNode<T> node, T value)
        {
            // 예외1 : node가 연결리스트에 포함된 노드가 아닌 경우
            if (node.list != this)
                throw new InvalidOperationException();
            // 예외2 : 노드가 null인 경우
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            LinkedListNode<T> newNode = new LinkedListNode<T>(this, value);
            // 1. 만약에 node.next에 값이 있으면 중간에 넣는거니까 관계 4개 다 바꿈
            if (node.next != null)
            {
                newNode.prev = node;
                newNode.next = node.next;
                node.next = newNode;
                node.next.prev = newNode;
            }
            else
            {
                newNode.prev = node;
                node.next = newNode;
                tail = newNode;
            }
            count++;
            return newNode;
        }
        public void Remove(LinkedListNode<T> node)
        {
            // 예외1 : node가 연결리스트에 포함된 노드가 아닌 경우
            if (node.list != this)
                throw new InvalidOperationException();
            // 예외2 : 노드가 null인 경우
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if (node == head)
            {
                head = node.next;
            }
            else if(node == tail)
            {
                tail = node.prev;
            }
            else
            {
                node.prev.next = node.next;
                node.next.prev = node.prev;
            }
            count--;
        }
        public bool Remove(T value)
        {
            LinkedListNode<T> target = Find(value);
            if (target != null)
            {
                Remove(target);
                return true;
            }
            else
            {
                return false;
            }
        }

        public LinkedListNode<T> Find(T value)
        {
            LinkedListNode<T> target = head;
            EqualityComparer<T> comparer = EqualityComparer<T>.Default;
            while(target != null)
            {
                if(comparer.Equals(value, target.Item))
                {
                    return target;
                }
                else
                {
                    target = target.next;
                }
            }
            return null;
        }
    }
    
}
