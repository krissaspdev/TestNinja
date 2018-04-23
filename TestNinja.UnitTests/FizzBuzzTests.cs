using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class FizzBuzzTests
    {
        [Fact]
        public void GetOutput_InputIsDivisibleBy3And5_ReturnFizzBuzz()
        {
            var result = FizzBuzz.GetOutput(15);
            Assert.Equal("FizzBuzz", result);
        }

        [Fact]
        public void GetOutput_InputIsDivisibleBy3Only_ReturnFizz()
        {
            var result = FizzBuzz.GetOutput(3);
            Assert.Equal("Fizz", result);
        }

        [Fact]
        public void GetOutput_InputIsDivisibleBy5only_ReturnBuzz()
        {
            var result = FizzBuzz.GetOutput(5);
            Assert.Equal("Buzz", result);
        }

        [Fact]
        public void GetOutput_InputIsNotDivisibleBy3Or5_ReturnTheSameNumber()
        {
            var result = FizzBuzz.GetOutput(1);
            Assert.Equal("1", result);
        }

    }
}