using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Custom.Collections.Generic;
using NUnit.Framework;

namespace GenericQueue.Tests
{
    [TestFixture]
    public class Tests
    {
        [Test]
        public void SimpleTest()
        {
            var queue = new PriorityQueue<int, int>();
            
            Assert.IsFalse(queue.HasItems);
            
            queue.Enqueue(1, 10);
            queue.Enqueue(3, 30);
            queue.Enqueue(5, 50);
            queue.Enqueue(4, 40);
            queue.Enqueue(2, 20);

            Assert.IsTrue(queue.HasItems);
            Assert.AreEqual(50, queue.Peek());
            Assert.AreEqual(50, queue.Peek());
            Assert.AreEqual(50, queue.Dequeue());

            Assert.AreEqual(40, queue.Peek());
            Assert.AreEqual(40, queue.Dequeue());

            Assert.IsTrue(queue.HasItems);
            Assert.AreEqual(30, queue.Peek());
            Assert.AreEqual(30, queue.Peek());
            Assert.AreEqual(30, queue.Peek());
            Assert.AreEqual(30, queue.Dequeue());

            Assert.IsTrue(queue.HasItems);
            Assert.AreEqual(20, queue.Peek());
            Assert.AreEqual(20, queue.Dequeue());

            Assert.AreEqual(10, queue.Peek());
            Assert.AreEqual(10, queue.Peek());
            Assert.AreEqual(10, queue.Peek());
            Assert.AreEqual(10, queue.Peek());
            Assert.AreEqual(10, queue.Peek());
            Assert.IsTrue(queue.HasItems);
            Assert.AreEqual(10, queue.Peek());
            Assert.AreEqual(10, queue.Dequeue());

            Assert.IsFalse(queue.HasItems);
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [Test]
        public void PeekShouldThrowWhenEmpty()
        {
            var queue = new PriorityQueue<int, int>();
            queue.Peek();
        }

        [ExpectedException(typeof(InvalidOperationException))]
        [Test]
        public void DequeueShouldThrowWhenEmpty()
        {
            var queue = new PriorityQueue<int, int>();
            queue.Dequeue();
        }
    }
}
