using AutoMapper;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.Mappers;
using FinoBank.Cola.Manager.Queries;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.Interfaces;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.UnitTests
{
    [TestClass]
    public class QueryAcceptTransactionRequestManagerServiceTest
    {
        #region Variables

        private Mock<IUnitOfWork> mockUnitOfWork;
        private Mock<IQueryAcceptTransactionRequestManagerService> mockQueryAcceptTransactionRequestManagerService;
        private Mock<IQueryAcceptTransactionRequestRepository> mockQueryAcceptTransactionRequestRepository;

        private QueryAcceptTransactionRequestManagerService queryAcceptTransactionRequestManagerService;

        #endregion Variables


        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ModelsAutoMapper>();
            });

            mockUnitOfWork = new Mock<IUnitOfWork>();
            mockQueryAcceptTransactionRequestManagerService = new Mock<IQueryAcceptTransactionRequestManagerService>();
            mockQueryAcceptTransactionRequestRepository = new Mock<IQueryAcceptTransactionRequestRepository>();

            
            mockQueryAcceptTransactionRequestRepository.Setup(x => x.AcceptTransactionRequest(It.IsAny<long>(),It.IsAny<string>())).ReturnsAsync(true);

            mockUnitOfWork.SetupProperty(repo => repo.QueryAcceptTransactionRequestRepository, mockQueryAcceptTransactionRequestRepository.Object);


            queryAcceptTransactionRequestManagerService = new QueryAcceptTransactionRequestManagerService(Mapper.Instance, mockUnitOfWork.Object);

        }

        [TestCleanup]
        public void TestCleanup()
        {
            Mapper.Reset();
        }

        [TestMethod]
        public async Task AcceptTransactionRequest_Successfully()
        {
            //Arrange
            var acceptTransactionRequestData = new AcceptTransactionRequestViewModel { TransactionId = 18122018184003107, RefCode = "Test123" };

            //Act
            var result = await queryAcceptTransactionRequestManagerService.AcceptTransactionRequest(acceptTransactionRequestData).ConfigureAwait(false) as OperationResult<CommandSuccessBoolResultViewModel>;

            //Assert
            mockQueryAcceptTransactionRequestRepository.Verify(repo => repo.AcceptTransactionRequest(It.IsAny<long>(), It.IsAny<string>()), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data != null);
        }
    }
}