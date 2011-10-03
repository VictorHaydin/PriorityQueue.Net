using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Custom.Collections.Generic
{
    public class PriorityQueue<TPriority, TValue> where TPriority : IComparable<TPriority>
    {
        // Children: 2*ParentIndex+1, 2*ParentIndex+2
        private readonly List<QueueItem> m_Heap = new List<QueueItem>();

        public void Enqueue(TPriority priority, TValue value)
        {
            m_Heap.Add(new QueueItem(priority, value));
            FlowUp(m_Heap.Count - 1);
        }

        public TValue Peek()
        {
            if(!HasItems)
            {
                throw new InvalidOperationException("Queue is empty!");
            }
            return m_Heap[0].Value;
        }

        public TValue Dequeue()
        {
            if (!HasItems)
            {
                throw new InvalidOperationException("Queue is empty!");
            }
            var value = m_Heap[0].Value;
            if(m_Heap.Count == 1)
            {
                m_Heap.Clear();
            }
            else
            {
                SwapItemsByIndexes(0, m_Heap.Count - 1);
                m_Heap.RemoveAt(m_Heap.Count - 1);
                FlowDown(0);
            }
            return value;
        }

        public bool HasItems
        {
            get { return m_Heap.Count > 0; }
        }

        private void FlowUp(int index)
        {
            if(index == 0)
            {
                return;
            }
            var parent = (index - 1)/2;
            if(m_Heap[parent].Priority.CompareTo(m_Heap[index].Priority) < 0) // if child has greater priority than parent
            {
                SwapItemsByIndexes(index, parent);
                // recursive call for parent
                FlowUp(parent);
            }
        }

        private void SwapItemsByIndexes(int index1, int index2)
        {
            var temp = m_Heap[index2];
            m_Heap[index2] = m_Heap[index1];
            m_Heap[index1] = temp;
        }

        private void FlowDown(int index)
        {
            var child1 = index*2 + 1;
            var child2 = index*2 + 2;
            if(child1 < m_Heap.Count && child2 < m_Heap.Count) // both children present
            {
                if(m_Heap[index].Priority.CompareTo(m_Heap[child1].Priority) < 0 || m_Heap[index].Priority.CompareTo(m_Heap[child2].Priority) < 0)
                {
                    int childToSwap;
                    if(m_Heap[child1].Priority.CompareTo(m_Heap[child2].Priority) < 0)
                    {
                        childToSwap = child2;
                    }
                    else
                    {
                        childToSwap = child1;
                    }
                    SwapItemsByIndexes(index, childToSwap);
                    FlowDown(childToSwap);
                }
            }
            else if(child1 < m_Heap.Count) // only left child presents
            {
                if(m_Heap[index].Priority.CompareTo(m_Heap[child1].Priority) < 0) // child has greater priority than parent
                {
                    SwapItemsByIndexes(index, child1);
                }
            } // else do nothing - we are on the bottom
        }

        private class QueueItem
        {
            private readonly TPriority m_Priority;
            private readonly TValue m_Value;

            public QueueItem(TPriority priority, TValue value)
            {
                m_Priority = priority;
                m_Value = value;
            }

            public TValue Value
            {
                get { return m_Value; }
            }

            public TPriority Priority
            {
                get { return m_Priority; }
            }
        }
    }
}
