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
    public class QueryMerchantSearchManagerServiceTest
    {
        #region Variables

        private Mock<IUnitOfWork> mockUnitOfWork;

        private Mock<IQueryMerchantSearchManagerService> mockQueryMerchantSearchManagerService;
        private Mock<IQueryMerchantSearchRepository> mockQueryMerchantSearchRepository;

        private QueryMerchantSearchManagerService queryMerchantSearchManagerService;

        #endregion Variables

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ModelsAutoMapper>();
            });

            mockUnitOfWork = new Mock<IUnitOfWork>();

            mockQueryMerchantSearchManagerService = new Mock<IQueryMerchantSearchManagerService>();
            mockQueryMerchantSearchRepository = new Mock<IQueryMerchantSearchRepository>();

            mockQueryMerchantSearchRepository.Setup(x => x.GetMerchantSearchDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()));
            mockUnitOfWork.SetupProperty(repo => repo.QueryMerchantSearchRepository, mockQueryMerchantSearchRepository.Object);

            queryMerchantSearchManagerService = new QueryMerchantSearchManagerService(Mapper.Instance, null, mockUnitOfWork.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Mapper.Reset();
        }

        [TestMethod]
        public async Task MerchantSearch_Successfully_WithMerchants()
        {
            //Arrange
            var requestParam = new MerchantSearchRequestViewModel() { CustomerType = "F", CustomerRefCode = "A1", CustomerMobile = "989000000", Amount = 500, CurrentLatitude = 18.513533, CurrentLongitude = 73.8495854, ByMerchantTypeId = 1, ByTransactionTypeId = 2, ByWithdrawalTypeId = 1, Distance = 5, SortColumn = null, SortDirection = null, PageIndex = 1, PageSize = 5,  TotalCount = 5 };

            var merchantDataResult = new List<MerchantSearchResultDomainModel>()
            {
                new MerchantSearchResultDomainModel() { Id = 1, Name = "Merchant1", IsActive=true, IsDeleted=false},
                new MerchantSearchResultDomainModel() { Id = 2, Name = "Merchant2", IsActive=true, IsDeleted=false},
                new MerchantSearchResultDomainModel() { Id = 3, Name = "Merchant3", IsActive=true, IsDeleted=false},
                new MerchantSearchResultDomainModel() { Id = 4, Name = "Merchant4", IsActive=true, IsDeleted=false},
                new MerchantSearchResultDomainModel() { Id = 5, Name = "Merchant5", IsActive=true, IsDeleted=false}
            };
            var summaryDataResult = new Tuple<List<MerchantSearchResultDomainModel>, int>(merchantDataResult, 5);
            
            //Act
            mockQueryMerchantSearchRepository.Setup(x => x.GetMerchantSearchDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(summaryDataResult);
            var result = await queryMerchantSearchManagerService.GetMerchantSearchDataWithPagingAsync(requestParam).ConfigureAwait(false) as OperationResult<MerchantSearchResultViewModel>;

            //Assert
            mockQueryMerchantSearchRepository.Verify(repo => repo.GetMerchantSearchDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Result.Count == 5);
        }

        [TestMethod]
        public async Task MerchantSearch_Successfully_WithoutMerchants()
        {
            //Arrange
            var requestParam = new MerchantSearchRequestViewModel() { CustomerType = "F", CustomerRefCode = "A1", CustomerMobile = "989000000", Amount = 500, CurrentLatitude = 18.513533, CurrentLongitude = 73.8495854, ByMerchantTypeId = 1, ByTransactionTypeId = 2, ByWithdrawalTypeId = 1, Distance = 5, SortColumn = null, SortDirection = null, PageIndex = 1, PageSize = 5, TotalCount = 5 };
            var merchantDataResult = new List<MerchantSearchResultDomainModel>()
            {
            };
            var summaryDataResult = new Tuple<List<MerchantSearchResultDomainModel>, int>(merchantDataResult, 0);

            //Act
            mockQueryMerchantSearchRepository.Setup(x => x.GetMerchantSearchDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(summaryDataResult);
            var result = await queryMerchantSearchManagerService.GetMerchantSearchDataWithPagingAsync(requestParam).ConfigureAwait(false) as OperationResult<MerchantSearchResultViewModel>;

            //Assert
            mockQueryMerchantSearchRepository.Verify(repo => repo.GetMerchantSearchDataWithPaging(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Result.Count == 0);
        }
    }
}