using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class EmploeeControllerTests
    {
        [Fact]
        public void DeleteEmploee_WhenCalled_DeleteEmploeeFromDb()
        {
            var storage = new Mock<IEmploeeStorage>();
            var controller = new EmployeeController(storage.Object);

            controller.DeleteEmployee(1);
            
            storage.Verify(s => s.DeleteEmployee(1));
        }
        
        [Fact]
        public void DeleteEmploee_WhenCalled_RedirectToEmpleeAction()
        {
            var storage = new Mock<IEmploeeStorage>();
            var controller = new EmployeeController(storage.Object);

            var result = controller.DeleteEmployee(1);
            
            var viewResult = Assert.IsType<RedirectResult>(result);
        }        
    }
}