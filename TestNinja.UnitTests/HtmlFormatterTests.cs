using System;
using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class HtmlFormatterTests
    {
        [Fact]
        public void FormatAsBold_WhenCalled_ShouldEncloseTheStringWithStrongElement()
        {
            var htmlFormatter = new HtmlFormatter();

            var result = htmlFormatter.FormatAsBold("abc");

            //Assert.Equal(result, "<strong>abc</strong>"); // very specific

            Assert.StartsWith("<strong>", result, StringComparison.OrdinalIgnoreCase);
            Assert.EndsWith("</strong>", result, StringComparison.OrdinalIgnoreCase);
            Assert.Contains("abc", result, StringComparison.OrdinalIgnoreCase);
        }
    }
}