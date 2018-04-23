using TestNinja.Fundamentals;
using Xunit;

namespace TestNinja.UnitTests
{
    public class CustomerControllerTests
    {
        [Fact]
        public void GetCustomer_IdIsZero_ReturnNotFound()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(0);

            //Assert.IsType(typeof(NotFound), result);
            Assert.IsType<NotFound>(result);
        }

        [Fact]
        public void GetCustomer_IdIsNotZero_ReturnOk()
        {
            var controller = new CustomerController();

            var result = controller.GetCustomer(1);

            Assert.IsType<Ok>(result);
        }        
    }
}