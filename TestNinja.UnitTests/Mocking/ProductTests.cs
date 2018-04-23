using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class ProductTests
    {
        [Fact]
        public void GetPrice_GoldCustomer_Apply30PercentDiccount()
        {
            var product = new Product {ListPrice = 100};
            
            var result = product.GetPrice(new Customer {IsGold = true});            
            
            Assert.Equal(70, result);
        }
    }
}