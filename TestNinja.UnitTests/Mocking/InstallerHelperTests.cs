using System.Net;
using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class InstallerHelperTests
    {
        private Mock<IFileDownloader> _fileDownloader;
        private InstallerHelper _installerHelper;

        public InstallerHelperTests()
        {
            _fileDownloader = new Mock<IFileDownloader>();
            _installerHelper = new InstallerHelper(_fileDownloader.Object);
        }

        [Fact]
        public void DownloadInstaller_DownloadFails_ReturnFalse()
        {
            _fileDownloader.Setup(fd => fd.DownloadFile(It.IsAny<string>(), It.IsAny<string>())).Throws<WebException>();

            var result = _installerHelper.DownloadInstaller("cutomerName", "installerName");
            
            Assert.Equal(false, result);
        }
        
        [Fact]
        public void DownloadInstaller_DownloadComplets_ReturnTrue()
        {
            var result = _installerHelper.DownloadInstaller("cutomerName", "installerName");
            
            Assert.Equal(true, result);
        }
        
    }
}