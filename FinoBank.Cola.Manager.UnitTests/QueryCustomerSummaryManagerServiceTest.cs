using AutoMapper;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.Mappers;
using FinoBank.Cola.Manager.Queries;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.UnitTests
{
    [TestClass]
    public class QueryCustomerSummaryManagerServiceTest
    {
        #region Variables

        private Mock<IUnitOfWork> mockUnitOfWork;

        private Mock<IQueryCustomerSummaryManagerService> mockQueryCustomerSummaryManagerService;
        private Mock<IQueryCustomerSummaryRepository> mockQueryCustomerSummaryRepository;

        private QueryCustomerSummaryManagerService queryCustomerSummaryManagerService;

        #endregion Variables

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ModelsAutoMapper>();
            });

            mockUnitOfWork = new Mock<IUnitOfWork>();



            mockQueryCustomerSummaryManagerService = new Mock<IQueryCustomerSummaryManagerService>();
            mockQueryCustomerSummaryRepository = new Mock<IQueryCustomerSummaryRepository>();

            mockUnitOfWork.SetupProperty(repo => repo.QueryCustomerSummaryRepository, mockQueryCustomerSummaryRepository.Object);
            queryCustomerSummaryManagerService = new QueryCustomerSummaryManagerService(Mapper.Instance, null, mockUnitOfWork.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Mapper.Reset();
        }

        [TestMethod]
        public async Task CustomerSummary_GetCustomerDetailsByMobileNumber_WithData()
        {
            //Arrange
            var dataResult = new CustomerDomainModel() { Mobile= "9800000000", RefCode="C1", IsActive=true, IsDeleted=false };
            var summaryDataResult = new Tuple<CustomerDomainModel> (dataResult);

            //Act
            mockQueryCustomerSummaryRepository.Setup(x => x.GetCustomerDetailsByMobileNumber(It.IsAny<string>())).ReturnsAsync(summaryDataResult);
            var result = await queryCustomerSummaryManagerService.GetCustomerDetailsByMobileNumber("9800000000").ConfigureAwait(false) as OperationResult<CustomerDomainModel>;

            //Assert
            mockQueryCustomerSummaryRepository.Verify(repo => repo.GetCustomerDetailsByMobileNumber(It.IsAny<string>()), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data != null);
        }
    }
}