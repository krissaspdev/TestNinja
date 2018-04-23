using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace TestNinja.UnitTests
{
    public class ToDoItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Done { get; set; }
        public DateTime? DueDate { get; set; }
    }
    

    public class DataService
    {
        public void AddToDoItem(ToDoItem item)
        {
            if(string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentException();
        }
    }



    public class TodoItemsTestData : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] { Guid.NewGuid(), string.Empty, false, null},
            new object[] { Guid.NewGuid(), " ", false, null },
            new object[] { Guid.NewGuid(), null, false, null }
        };
 
        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();
 
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
    
    public class TestDataGenerator : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>
        {
            new object[] {5, 1, 3, 9},
            new object[] {7, 1, 5, 3}
        };

        //public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        //IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public IEnumerator<object[]> GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    
    public class ParameterizedTests
    {
        public bool IsOddNumber(int number)
        {
            return number % 2 != 0;
        }

        [Theory]
        [ClassData(typeof(TestDataGenerator))]
        public void AllNumbers_AreOdd_WithClassData(int a, int b, int c, int d)
        {
            Assert.True(IsOddNumber(a));
            Assert.True(IsOddNumber(b));
            Assert.True(IsOddNumber(c));
            Assert.True(IsOddNumber(d));
        }

        [Theory]
        [ClassData(typeof(TodoItemsTestData))]
        public void AddToDoItem_WhenTitleIsNullOrWhiteSpace_ThrowsArgumentExcepion(Guid id, string title, bool done, DateTime? dueDate)
        {
            //Arrange
            DataService service = new DataService();
            var item = new ToDoItem { Id = id, Name = title, Done = done, DueDate = dueDate };
 
            //Act
            var ex = Assert.Throws<ArgumentException>(() => service.AddToDoItem(item));
 
            //Assert
            Assert.IsType<ArgumentException>(ex);            
        }
    }
}