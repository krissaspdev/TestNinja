using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class StackTests
    {
        [Fact]
        public void Push_ArgIsNull_ThrowArgumentNullException()
        {
            var stack = new Stack<string>();
            Assert.Throws<ArgumentNullException>(() => stack.Push(null));
        }

        [Fact]
        public void Push_ValidArg_AddTheObjectToTheStack()
        {
            var stack = new Stack<string>();
            stack.Push("a");

            Assert.Equal(1, stack.Count);

        }

        [Fact]
        public void Count_EmptyStack_ReturnZero()
        {
            var stack = new Stack<string>();
            Assert.Equal(0, stack.Count);
        }

        [Fact]
        public void Pop_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.Throws<InvalidOperationException>(() => stack.Pop());
        }

        [Fact]
        public void Pop_StackWithAFewObjects_ReturnObjectOnTheTop()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            var result = stack.Pop();

            Assert.Equal("c", result);
        }

        [Fact]
        public void Pop_StackWithAFewObjects_RemoveObjectFromTheTop()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");

            stack.Pop();

            Assert.Equal(2, stack.Count );

        }

        [Fact]
        public void Peek_EmptyStack_ThrowInvalidOperationException()
        {
            var stack = new Stack<string>();

            Assert.Throws<InvalidOperationException>(() => stack.Peek());
        }

        [Fact]
        public void Peek_StackWithObject_ReturnObjectFromTheTopOfTheStack()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");
            

            var result = stack.Peek();

            Assert.Equal("c", result);
        }        

        [Fact]
        public void Peek_StackWithObjects_DoesNotRmoveTheObjectOnTheTopOfTheStack()
        {
            var stack = new Stack<string>();
            stack.Push("a");
            stack.Push("b");
            stack.Push("c");
            

            stack.Peek();

            Assert.Equal(3, stack.Count);
        
        }
    }
}