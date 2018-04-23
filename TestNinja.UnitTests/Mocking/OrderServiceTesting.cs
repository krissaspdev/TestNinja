using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class OrderServiceTesting
    {
        [Fact]
        public void PlaceOrder_WhenCalled_StoreTheOrder() // checking interaction between objects
        {
            var storage = new Mock<IStorage>();
            var orderService = new OrderService(storage.Object);

            var order = new Order();
            orderService.PlaceOrder(order);

            storage.Verify(s => s.Store(order));
        }
    }
}