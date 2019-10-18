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
    public class QueryMerchantSummaryManagerServiceTest
    {
        #region Variables

        private Mock<IUnitOfWork> mockUnitOfWork;

        private Mock<IQueryMerchantSummaryManagerService> mockQueryMerchantSummaryManagerService;

        private Mock<IQueryMerchantSummaryRepository> mockQueryMerchantSummaryRepository;

        private QueryMerchantSummaryManagerService queryMerchantSummaryManagerService;

        #endregion Variables

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ModelsAutoMapper>();
            });

            mockUnitOfWork = new Mock<IUnitOfWork>();

            mockQueryMerchantSummaryManagerService = new Mock<IQueryMerchantSummaryManagerService>();

            mockQueryMerchantSummaryRepository = new Mock<IQueryMerchantSummaryRepository>();
            mockUnitOfWork.SetupProperty(repo => repo.QueryMerchantSummaryRepository, mockQueryMerchantSummaryRepository.Object);

            queryMerchantSummaryManagerService = new QueryMerchantSummaryManagerService(Mapper.Instance, null, mockUnitOfWork.Object);
        }
        
        [TestCleanup]
        public void TestCleanup()
        {
            Mapper.Reset();
        }

        //[TestMethod]
        //public async Task MerchantSummary_GetAllMerchantData()
        //{
        //    //Arrange
        //    var merchantTypeResult = new List<MerchantSearchResultDomainModel>()
        //    {
        //        new MerchantSearchResultDomainModel() { Id = 1, Name = "Merchant1", WithdrawCashBalance=100, DepositCashBalance=200, IsActive=true, IsDeleted=false },
        //        new MerchantSearchResultDomainModel() { Id = 2, Name = "Merchant2", WithdrawCashBalance=200, DepositCashBalance=400, IsActive=true, IsDeleted=false },
        //        new MerchantSearchResultDomainModel() { Id = 3, Name = "Merchant3", WithdrawCashBalance=300, DepositCashBalance=600, IsActive=true, IsDeleted=false },
        //        new MerchantSearchResultDomainModel() { Id = 4, Name = "Merchant4", WithdrawCashBalance=400, DepositCashBalance=800, IsActive=true, IsDeleted=false },
        //    };

        //    var summaryDataResult = new Tuple<List<MerchantSearchResultDomainModel>>(merchantTypeResult);

        //    // Act
        //    mockQueryMerchantSummaryRepository.Setup(a => a.GetAllData(It.IsAny<string>())).ReturnsAsync(summaryDataResult);

        //    var result = await queryMerchantSummaryManagerService.GetAllMerchantData().ConfigureAwait(false) as OperationResult<List<MerchantViewModel>>;

        //    //Assert
        //    mockQueryMerchantSummaryRepository.Verify(repo => repo.GetAllData("Merchant"), Times.Once);
        //    Assert.IsTrue(result.Success);
        //    Assert.IsTrue(result.Data != null);
        //}

        //[TestMethod]
        //public async Task MerchantSummary_GetMerchantDetailsByRefCode()
        //{
        //    //Arrange
        //    var merchantTypeResult = new MerchantSearchResultDomainModel() {  Id = 1, Name = "Merchant1", WithdrawCashBalance = 100, DepositCashBalance = 200, IsActive = true, IsDeleted = false };

        //    var summaryDataResult = new Tuple<MerchantSearchResultDomainModel>(merchantTypeResult);
            
        //    // Act
        //    mockQueryMerchantSummaryRepository.Setup(a => a.GetMerchantDetailsByRefCode(It.IsAny<string>())).ReturnsAsync(summaryDataResult);

        //    var result = await queryMerchantSummaryManagerService.GetMerchantDetailsByRefCode("A1").ConfigureAwait(false) as OperationResult<MerchantViewModel>;

        //    //Assert
        //    mockQueryMerchantSummaryRepository.Verify(repo => repo.GetMerchantDetailsByRefCode(It.IsAny<string>()), Times.Once);
        //    Assert.IsTrue(result.Success);
        //    Assert.IsTrue(result.Data != null);
        //}

    }
}