using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructure
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        private Node root;          // 이진탐색트리의 멤버로 노드형태의 지붕노드가 있어야 됨

        public class Node           // 노드 클래스 정의
        {
            public T item;          // 노드의 값
            public Node parent;     // 부모노드
            public Node left;       // 왼쪽 자식노드
            public Node right;      // 오른쪽 자식노드

            public Node(T item, Node parent, Node left, Node right)
            {
                this.item = item;
                this.parent = parent;
                this.left = left;
                this.right = right;
            }
            // 읽기 전용 메서드
            public bool IsRootNode { get { return parent == null; } }
            public bool IsLeftChild { get { return parent != null && parent.left == this; } }
            public bool IsRightChild { get { return parent != null && parent.right == this; } }
            public bool HasNoChild { get { return parent.left == null && parent.right == null; } }
            public bool HasLeftChild { get { return parent.left != null && parent.right == null; } }
            public bool HasRightChild { get { return parent.left == null && parent.right != null; } }
            public bool HasBothChild { get { return parent.left != null && parent.right != null; } }
        }

        public BinarySearchTree()   // 이진탐색트리 클래스 초기화
        {
            this.root = null;
        }

        public void Add(T item)     // Add함수 정의
        {
            Node newNode = new Node(item, null, null, null);    // 새로운노드 인스턴스 생성 및 newNode 변수에 담음

            if (root == null)       // 루트노드가 없는 경우
            {
                root = newNode;     // 새로운노드를 루트로 대입
                return;
            }
            Node current = root;    // current 노드 정의 및 루트 대입
            while(current != null)  // current가 비어있지 않은 경우 계속 진행
            {
                // 새로운노드와 current 비교해서 새로운노드가 더 작은 경우 왼쪽으로
                if (item.CompareTo(current.item) < 0)
                {
                    // current의 왼쪽 자식이 있는 경우
                    if (current.left != null){
                        current = current.left;     // current의 왼쪽노드를 current로 대입
                    }
                    // current의 왼쪽 자식이 없는 경우
                    else
                    {
                        current.left = newNode;
                        newNode.parent = current;
                        return;
                    }
                }
                // 새로운노드와 current노드 비교해서 새로운노드가 더 큰 경우 오른쪽으로
                else if (item.CompareTo(current.item) > 0)
                {
                    // current의 오른쪽 자식이 있는 경우
                    if (current.right != null)
                    {
                        current = current.right;
                    }
                    // current의 오른쪽 자식이 없는 경우
                    else
                    {
                        current.right = newNode;
                        newNode.parent = current;
                        return;
                    }
                }
                // 비교해서 똑같은 경우 c#에서는 중복 허용 X
                else
                {
                    return;
                }
            }
        }

        public Node FindNode(T item)    // FindNode함수 정의
        {
            if(root == null)            // 루트가 비어있는 경우
            {
                return null;            
            }
            Node current = root;
            while(current != null)
            {
                // 비교해서 더 작을 경우 왼쪽으로
                if (item.CompareTo(current.item) < 0)
                {
                    // current의 왼쪽 자식이 있는 경우
                    current = current.left;
                }
                // 비교해서 더 클 경우 오른쪽으로
                else if (item.CompareTo(current.item) > 0)
                {
                    // current의 오른쪽 자식이 있는 경우
                    current = current.right;
                }
                else
                {
                    return current;
                }
            }
            return null;
        }

        public bool TryGetValue(T item, out T outValue)
        {
            if(root == null)            // 루트가 비어있는 경우 false, 읽기전용 outValue 디폴트값
            {
                outValue = default(T);
                return false;
            }
            Node findNode = FindNode(item); // 찾기 기능을 이용해서 찾을 노드 정의
            if(findNode != null)            // 찾았다면 true
            {
                outValue = findNode.item;
                return true;
            }
            else                            // 못찾았으면 false
            {
                outValue = default(T);
                return false;
            }
        }

        public bool Remove(T item)          // 지우기 기능 정의
        {
            Node findNode = FindNode(item); // 지울 노드 찾기 기능으로 정의
            if (findNode == null)           // 못찾았으면 false
            {
                return false;
            }
            else                            // 찾았으면 지우기기능에 찾은 노드 넣어서 지우기
            {                               // true 반환
                EraseNode(findNode);
                return true;
            }
        }

        public void EraseNode(Node node)
        {
            if (node.HasNoChild)
            {
                if (node.IsLeftChild)
                {
                    node.parent.left = null;
                }
                else if (node.IsRightChild)
                {
                    node.parent.right = null;
                }
                else
                {
                    root = null;
                }
            }
            else if(node.HasLeftChild || node.HasRightChild)
            {
                Node parent = node.parent;
                Node child = node.HasLeftChild ? node.left : node.right;

                if (node.IsLeftChild)
                {
                    parent.left = child;
                    child.parent = parent;
                }
                else if (node.IsRightChild)
                {
                    parent.right = child;
                    child.parent = parent;
                }
                else
                {
                    root = child;
                    child.parent = null;
                }
            }
            // 3. 자식노드가 2개인 노드일 경우
            else
            {
                Node replaceNode = node.left;
                while (replaceNode != null)
                {
                    replaceNode = replaceNode.right;
                }
                node.item = replaceNode.item;
                EraseNode(replaceNode);
            }
        }
    }
}
