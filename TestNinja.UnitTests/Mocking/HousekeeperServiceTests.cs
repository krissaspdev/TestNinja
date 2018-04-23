using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using TestNinja.Mocking;
using Xunit;

namespace TestNinja.UnitTests.Mocking
{
    public class HousekeeperServiceTests
    {
        private Mock<IStatementGenerator> _statementGenerator;
        private HousekeeperService _service;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private DateTime _statementDate = new DateTime(2018, 1, 1);
        private Housekeeper _houseKeeper;
        private string _statementFileName;

        public HousekeeperServiceTests()
        {
            _houseKeeper = new Housekeeper{ Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c"};
            
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.Setup(uof => uof.Query<Housekeeper>()).Returns(new List<Housekeeper>
            {
                _houseKeeper

            }.AsQueryable());

            _statementFileName = "fileName";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator
                .Setup(sg => sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate))
                .Returns(() => _statementFileName); // lazy evauation            
            
            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();
            
            _service = new HousekeeperService(unitOfWork.Object, 
                _statementGenerator.Object,
                _emailSender.Object, 
                _messageBox.Object);            
        }
        
        [Fact]
        public void SendStatementEmails_WhenCalled_GenerateStatements()
        {            
            _service.SendStatementEmails(_statementDate);            
            _statementGenerator.Verify(sg => 
                sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate));
            
        }
        
        
        [Theory]
        [InlineData(" ")]
        [InlineData("")]
        [InlineData(null)]
        public void SendStatementEmails_HouseKeeperEmailIsEmpty_ShouldNotGenerateSattement(string email)
        {
            _houseKeeper.Email = email;
            _service.SendStatementEmails(_statementDate);            
            _statementGenerator.Verify(sg => 
                    sg.SaveStatement(_houseKeeper.Oid, _houseKeeper.FullName, _statementDate),
                Times.Never);        
        }
        
        [Fact]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            _service.SendStatementEmails(_statementDate);
            
            VerifyEmailSent();
        }


        [Fact]
        public void SendStatementEmails_SatementFileNameIsNull_ShouldNotEmailTheStatement()
        {
            _statementFileName = null;

            _service.SendStatementEmails(_statementDate);
            
            VerifyEmailNotSent();
        }



        [Fact]
        public void SendStatementEmails_SatementFileNameIsEmpty_ShouldNotEmailTheStatement()
        {
            _statementFileName = "";

            _service.SendStatementEmails(_statementDate);
            
            VerifyEmailNotSent();
        }          
        
        [Fact]
        public void SendStatementEmails_SatementFileNameIsWhitespace_ShouldNotEmailTheStatement()
        {
            _statementFileName = " ";

            _service.SendStatementEmails(_statementDate);
            
            VerifyEmailNotSent();
        }
        
        [Fact]
        public void SendStatementEmails_SendingEmailFails_ShouldDisplayMessageBox()
        {
            _emailSender.Setup(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()
            )).Throws<Exception>();

            _service.SendStatementEmails(_statementDate);
            
            _messageBox.Verify(mb => mb.Show(
                It.IsAny<string>(),
                It.IsAny<string>(),
                MessageBoxButtons.OK
            ));
        }          
         
        
        private void VerifyEmailNotSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()), Times.Never);
        }
        
        private void VerifyEmailSent()
        {
            _emailSender.Verify(es => es.EmailFile(
                _houseKeeper.Email,
                _houseKeeper.StatementEmailBody,
                _statementFileName,
                It.IsAny<string>()));
        }
        
        //dodać THeory do 2 pozostałych
        //http://maciejgos.com/xunit-theory-inlinedata-classdata-memberdata/
    }
}