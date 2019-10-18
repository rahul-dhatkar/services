using AutoMapper;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.Mappers;
using FinoBank.Cola.Manager.Queries;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.UnitTests
{
    [TestClass]
    public class QueryTransactionSummaryManagerServiceTest
    {
        #region Variables

        private Mock<IUnitOfWork> mockUnitOfWork;

        private Mock<IQueryTransactionSummaryManagerService> mockQueryTransactionSummaryManagerService;
        private Mock<IQueryTransactionSummaryRepository> mockQueryTransactionSummaryRepository;

        private QueryTransactionSummaryManagerService queryTransactionSummaryManagerService;

        #endregion Variables

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ModelsAutoMapper>();
            });

            mockUnitOfWork = new Mock<IUnitOfWork>();

            mockQueryTransactionSummaryManagerService = new Mock<IQueryTransactionSummaryManagerService>();
            mockQueryTransactionSummaryRepository = new Mock<IQueryTransactionSummaryRepository>();

            mockUnitOfWork.SetupProperty(repo => repo.QueryTransactionSummaryRepository, mockQueryTransactionSummaryRepository.Object);
            queryTransactionSummaryManagerService = new QueryTransactionSummaryManagerService(Mapper.Instance, null, mockUnitOfWork.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Mapper.Reset();
        }

        [TestMethod]
        public async Task CustomerTransactionSummary_Successfully_WithData()
        {
            //Arrange
            var requestParam = new CustomerTransactionSummaryRequestViewModel() { CustomerType = "F", CustomerRefCode = "A1", CustomerMobile = "9800000000", TransactionStatusId = 1, FromDate = System.DateTime.Today, ToDate = System.DateTime.Today, SortColumn = null, SortDirection = null, PageIndex = 1, PageSize = 5, TotalCount = 5 };

            var transactionDataResult = new List<TransactionSummaryResultDomainModel>()
            {
                new TransactionSummaryResultDomainModel() { Id = 1, ReferenceNumber="11111111", CustomerMobile="989000000", ActualAmount=200,  IsActive=true, IsDeleted=false},
                new TransactionSummaryResultDomainModel() { Id = 2, ReferenceNumber="22222222", CustomerMobile="989000000", ActualAmount=300, IsActive=true, IsDeleted=false},
                new TransactionSummaryResultDomainModel() { Id = 3, ReferenceNumber="33333333", CustomerMobile="989000000", ActualAmount=400,IsActive=true, IsDeleted=false},
                new TransactionSummaryResultDomainModel() { Id = 4, ReferenceNumber="44444444", CustomerMobile="989000000", ActualAmount=500,IsActive=true, IsDeleted=false},
                new TransactionSummaryResultDomainModel() { Id = 5, ReferenceNumber="55555555", CustomerMobile="989000000", ActualAmount=600, IsActive=true, IsDeleted=false}
            };
            var summaryDataResult = new Tuple<List<TransactionSummaryResultDomainModel>, int>(transactionDataResult, 5);

            //Act
            mockQueryTransactionSummaryRepository.Setup(x => x.GetCustomerTransactionSummaryDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(summaryDataResult);
            var result = await queryTransactionSummaryManagerService.GetCustomerTransactionSummaryDataWithPaging(requestParam).ConfigureAwait(false) as OperationResult<TransactionSummaryResultViewModel>;

            //Assert
            mockQueryTransactionSummaryRepository.Verify(repo => repo.GetCustomerTransactionSummaryDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Result.Count == 5);
        }

        [TestMethod]
        public async Task CustomerTransactionSummary_Successfully_WithNoData()
        {
            //Arrange
            var requestParam = new CustomerTransactionSummaryRequestViewModel() { CustomerType = "F", CustomerRefCode = "A1", CustomerMobile = "9800000000", TransactionStatusId = 1, FromDate = System.DateTime.Today, ToDate = System.DateTime.Today, SortColumn = null, SortDirection = null, PageIndex = 1, PageSize = 5, TotalCount = 5 };

            var transactionDataResult = new List<TransactionSummaryResultDomainModel>()
            {
            };
            var summaryDataResult = new Tuple<List<TransactionSummaryResultDomainModel>, int>(transactionDataResult, 5);

            //Act
            mockQueryTransactionSummaryRepository.Setup(x => x.GetCustomerTransactionSummaryDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(summaryDataResult);
            var result = await queryTransactionSummaryManagerService.GetCustomerTransactionSummaryDataWithPaging(requestParam).ConfigureAwait(false) as OperationResult<TransactionSummaryResultViewModel>;

            //Assert
            mockQueryTransactionSummaryRepository.Verify(repo => repo.GetCustomerTransactionSummaryDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Result.Count == 0);
        }

        [TestMethod]
        public async Task MerchantTransactionSummary_Successfully_WithData()
        {
            //Arrange
            var requestParam = new MerchantTransactionSummaryRequestViewModel() { TransactionStatusId = 1, FromDate = System.DateTime.Today, ToDate = System.DateTime.Today, SortColumn = null, SortDirection = null, PageIndex = 1, PageSize = 5, TotalCount = 5 };

            var transactionDataResult = new List<TransactionSummaryResultDomainModel>()
            {
                new TransactionSummaryResultDomainModel() { Id = 1, ReferenceNumber="11111111", CustomerMobile="989000000", ActualAmount=200,  IsActive=true, IsDeleted=false},
                new TransactionSummaryResultDomainModel() { Id = 2, ReferenceNumber="22222222", CustomerMobile="989000000", ActualAmount=300, IsActive=true, IsDeleted=false},
                new TransactionSummaryResultDomainModel() { Id = 3, ReferenceNumber="33333333", CustomerMobile="989000000", ActualAmount=400,IsActive=true, IsDeleted=false},
                new TransactionSummaryResultDomainModel() { Id = 4, ReferenceNumber="44444444", CustomerMobile="989000000", ActualAmount=500,IsActive=true, IsDeleted=false},
                new TransactionSummaryResultDomainModel() { Id = 5, ReferenceNumber="55555555", CustomerMobile="989000000", ActualAmount=600, IsActive=true, IsDeleted=false}
            };
            var summaryDataResult = new Tuple<List<TransactionSummaryResultDomainModel>, int>(transactionDataResult, 5);

            //Act
            mockQueryTransactionSummaryRepository.Setup(x => x.GetMerchantTransactionSummaryDataWithPaging(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(summaryDataResult);
            var result = await queryTransactionSummaryManagerService.GetMerchantTransactionSummaryDataWithPaging(requestParam).ConfigureAwait(false) as OperationResult<TransactionSummaryResultViewModel>;

            //Assert
            mockQueryTransactionSummaryRepository.Verify(repo => repo.GetMerchantTransactionSummaryDataWithPaging(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Result.Count == 5);
        }

        [TestMethod]
        public async Task MerchantTransactionSummary_Successfully_WithNoData()
        {
            //Arrange
            var requestParam = new MerchantTransactionSummaryRequestViewModel() { TransactionStatusId = 1, FromDate = System.DateTime.Today, ToDate = System.DateTime.Today, SortColumn = null, SortDirection = null, PageIndex = 1, PageSize = 5, TotalCount = 5 };

            var transactionDataResult = new List<TransactionSummaryResultDomainModel>()
            {
            };
            var summaryDataResult = new Tuple<List<TransactionSummaryResultDomainModel>, int>(transactionDataResult, 5);

            //Act
            mockQueryTransactionSummaryRepository.Setup(x => x.GetMerchantTransactionSummaryDataWithPaging(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(summaryDataResult);
            var result = await queryTransactionSummaryManagerService.GetMerchantTransactionSummaryDataWithPaging(requestParam).ConfigureAwait(false) as OperationResult<TransactionSummaryResultViewModel>;

            //Assert
            mockQueryTransactionSummaryRepository.Verify(repo => repo.GetMerchantTransactionSummaryDataWithPaging(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Result.Count == 0);
        }
    }
}