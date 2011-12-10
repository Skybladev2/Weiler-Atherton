using System;
using System.Collections.Generic;
using System.Text;

namespace TriangulationResearch
{
    class CircularLinkedList<T> : LinkedList<T>
    {
        public LinkedListNode<T> NextOrFirst(LinkedListNode<T> current)
        {
            if (current.Next == null)
                return current.List.First;
            return current.Next;
        }

        public LinkedListNode<T> PreviousOrLast(LinkedListNode<T> current)
        {
            if (current.Previous == null)
                return current.List.Last;
            return current.Previous;
        }

        public CircularLinkedList<T> Reverse()
        {
            CircularLinkedList<T> temp = new CircularLinkedList<T>();
            foreach (var current in this)
                temp.AddFirst(current);

            return temp;
        }
    }
}
