using System.Linq;
using FluentAssertions;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class MathTests
    {
        private Math _math;
        public MathTests()
        {
            _math = new Math();
        }

        [Fact]
        public void Add_WhenCalled_ReturnTheSumaOfArguments()
        {
            var result = _math.Add(1, 2);

            Assert.Equal(3, result);
        }

        [Theory]
        [InlineData(1, 2, 2)]
        [InlineData(2, 1, 2)]
        [InlineData(1, 1, 1)]
        public void Max_WhenCalled_ReturnTheGreaterArgument(int a, int b, int expectedResult)
        {
            var result = _math.Max(a, b);
            Assert.Equal(expectedResult, result);
        }

        [Fact]
        public void GetOddNumbers_LimitIsGreaterThenZero_ReturnOddNumbersUpToLimit()
        {
            var result = _math.GetOddNumbers(5);

            // to general
            //Assert.NotEmpty(result);

            //Assert.Equal(result.Count(), 3);

            // more specific
            //Assert.Contains(1, result);
            //Assert.Contains(3, result);
            //Assert.Contains(5, result);

            result.Should().BeEquivalentTo(new [] {1, 3, 5});
        }
    }
}