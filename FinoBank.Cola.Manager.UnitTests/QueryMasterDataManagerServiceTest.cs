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
    public class QueryMasterDataManagerServiceTest
    {
        #region Variables

        private Mock<IUnitOfWork> mockUnitOfWork;

        private Mock<IQueryMasterDataManagerService> mockQueryMasterDataManagerService;

        private Mock<IQueryMerchantTypeMasterDataRepository> mockQueryMerchantTypeMasterDataRepository;
        private Mock<IQueryTransactionStatusMasterDataRepository> mockQueryTransactionStatusMasterDataRepository;
        private Mock<IQueryTransactionTypeMasterDataRepository> mockQueryTransactionTypeMasterDataRepository;
        private Mock<IQueryWithdrawalTypeMasterDataRepository> mockQueryWithdrawalTypeMasterDataRepository;

        private QueryMasterDataManagerService queryMasterDataManagerService;

        #endregion Variables

        [TestInitialize]
        public void TestInitialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ModelsAutoMapper>();
            });

            mockUnitOfWork = new Mock<IUnitOfWork>();
         
            mockQueryMasterDataManagerService = new Mock<IQueryMasterDataManagerService>();

            mockQueryMerchantTypeMasterDataRepository = new Mock<IQueryMerchantTypeMasterDataRepository>();
            mockQueryTransactionStatusMasterDataRepository = new Mock<IQueryTransactionStatusMasterDataRepository>();
            mockQueryTransactionTypeMasterDataRepository = new Mock<IQueryTransactionTypeMasterDataRepository>();
            mockQueryWithdrawalTypeMasterDataRepository = new Mock<IQueryWithdrawalTypeMasterDataRepository>();

            queryMasterDataManagerService = new QueryMasterDataManagerService(Mapper.Instance, null, mockUnitOfWork.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            Mapper.Reset();
        }

        [TestMethod]
        public async Task GetMerchantTypeMaster_WithData()
        {
            //Arrange
            var merchantTypeResult = new List<MerchantTypeDomainModel>()
            {
                new MerchantTypeDomainModel() { Id = 1, Name = "Branch", IsActive=true, IsDeleted=false  },
                new MerchantTypeDomainModel() { Id = 2, Name = "Merchant", IsActive=true, IsDeleted=false},
                new MerchantTypeDomainModel() { Id = 3, Name = "CSP",  IsActive=true, IsDeleted=false},
                new MerchantTypeDomainModel() { Id = 4, Name = "Others",  IsActive=true, IsDeleted=false},
            };

            var summaryDataResult = new Tuple<List<MerchantTypeDomainModel>>(merchantTypeResult);

            //Act
            mockQueryMerchantTypeMasterDataRepository.Setup(x => x.GetMerchantTypeMaster()).ReturnsAsync(summaryDataResult);
            mockUnitOfWork.SetupProperty(repo => repo.QueryMerchantTypeMasterDataRepository, mockQueryMerchantTypeMasterDataRepository.Object);
            var result = await queryMasterDataManagerService.GetMerchantTypeMaster().ConfigureAwait(false) as OperationResult<List<MerchantTypeViewModel>>;

            //Assert
            mockQueryMerchantTypeMasterDataRepository.Verify(repo => repo.GetMerchantTypeMaster(),Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count == 4);
        }

        [TestMethod]
        public async Task GetMerchantTypeMaster_WithNoData()
        {
            //Arrange
            var merchantTypeResult = new List<MerchantTypeDomainModel>()
            {
            };

            var summaryDataResult = new Tuple<List<MerchantTypeDomainModel>>(merchantTypeResult);

            //Act
            mockQueryMerchantTypeMasterDataRepository.Setup(x => x.GetMerchantTypeMaster()).ReturnsAsync(summaryDataResult);
            mockUnitOfWork.SetupProperty(repo => repo.QueryMerchantTypeMasterDataRepository, mockQueryMerchantTypeMasterDataRepository.Object);
            var result = await queryMasterDataManagerService.GetMerchantTypeMaster().ConfigureAwait(false) as OperationResult<List<MerchantTypeViewModel>>;

            //Assert
            mockQueryMerchantTypeMasterDataRepository.Verify(repo => repo.GetMerchantTypeMaster(), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count == 0);
        }

        [TestMethod]
        public async Task GetTransactionTypeMaster_WithData()
        {
            //Arrange
            var transactionStatusResult = new List<TransactionStatusDomainModel>()
            {
                new TransactionStatusDomainModel() { Id = 1, Name = "Initialised", IsActive=true, IsDeleted=false  },
                new TransactionStatusDomainModel() { Id = 2, Name = "Pending", IsActive=true, IsDeleted=false},
                new TransactionStatusDomainModel() { Id = 3, Name = "Completed",  IsActive=true, IsDeleted=false},
                new TransactionStatusDomainModel() { Id = 4, Name = "Cancelled",  IsActive=true, IsDeleted=false},
                new TransactionStatusDomainModel() { Id = 5, Name = "Expired",  IsActive=true, IsDeleted=false},
            };

            var summaryDataResult = new Tuple<List<TransactionStatusDomainModel>>(transactionStatusResult);

            //Act
            mockQueryTransactionStatusMasterDataRepository.Setup(x => x.GetTransactionStatusMaster()).ReturnsAsync(summaryDataResult);
            mockUnitOfWork.SetupProperty(repo => repo.QueryTransactionStatusMasterDataRepository, mockQueryTransactionStatusMasterDataRepository.Object);
            var result = await queryMasterDataManagerService.GetTransactionStatusMaster().ConfigureAwait(false) as OperationResult<List<TransactionStatusViewModel>>;

            //Assert
            mockQueryTransactionStatusMasterDataRepository.Verify(repo => repo.GetTransactionStatusMaster(), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count == 5);
        }

        [TestMethod]
        public async Task GetTransactionTypeMaster_WithNoData()
        {
            //Arrange
            var transactionStatusResult = new List<TransactionStatusDomainModel>()
            {
            };

            var summaryDataResult = new Tuple<List<TransactionStatusDomainModel>>(transactionStatusResult);

            //Act
            mockQueryTransactionStatusMasterDataRepository.Setup(x => x.GetTransactionStatusMaster()).ReturnsAsync(summaryDataResult);
            mockUnitOfWork.SetupProperty(repo => repo.QueryTransactionStatusMasterDataRepository, mockQueryTransactionStatusMasterDataRepository.Object);
            var result = await queryMasterDataManagerService.GetTransactionStatusMaster().ConfigureAwait(false) as OperationResult<List<TransactionStatusViewModel>>;

            //Assert
            mockQueryTransactionStatusMasterDataRepository.Verify(repo => repo.GetTransactionStatusMaster(), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count == 0);
        }

        [TestMethod]
        public async Task GetTransactionStatusMaster_WithData()
        {
            //Arrange
            var transactionTypeResult = new List<TransactionTypeDomainModel>()
            {
                new TransactionTypeDomainModel() { Id = 1, Name = "Deposit", IsActive=true, IsDeleted=false  },
                new TransactionTypeDomainModel() { Id = 2, Name = "Withdrawal", IsActive=true, IsDeleted=false},
            };

            var summaryDataResult = new Tuple<List<TransactionTypeDomainModel>>(transactionTypeResult);

            //Act
            mockQueryTransactionTypeMasterDataRepository.Setup(x => x.GetTransactionTypeMaster()).ReturnsAsync(summaryDataResult);
            mockUnitOfWork.SetupProperty(repo => repo.QueryTransactionTypeMasterDataRepository, mockQueryTransactionTypeMasterDataRepository.Object);
            var result = await queryMasterDataManagerService.GetTransactionTypeMaster().ConfigureAwait(false) as OperationResult<List<TransactionTypeViewModel>>;

            //Assert
            mockQueryTransactionTypeMasterDataRepository.Verify(repo => repo.GetTransactionTypeMaster(), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count == 2);
        }

        [TestMethod]
        public async Task GetTransactionStatusMaster_WithNoData()
        {
            //Arrange
            var transactionTypeResult = new List<TransactionTypeDomainModel>()
            {
            };

            var summaryDataResult = new Tuple<List<TransactionTypeDomainModel>>(transactionTypeResult);

            //Act
            mockQueryTransactionTypeMasterDataRepository.Setup(x => x.GetTransactionTypeMaster()).ReturnsAsync(summaryDataResult);
            mockUnitOfWork.SetupProperty(repo => repo.QueryTransactionTypeMasterDataRepository, mockQueryTransactionTypeMasterDataRepository.Object);
            var result = await queryMasterDataManagerService.GetTransactionTypeMaster().ConfigureAwait(false) as OperationResult<List<TransactionTypeViewModel>>;

            //Assert
            mockQueryTransactionTypeMasterDataRepository.Verify(repo => repo.GetTransactionTypeMaster(), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count == 0);
        }

        [TestMethod]
        public async Task GetWithdrawalTypeMaster_WithData()
        {
            //Arrange
            var withdrawalTypeResult = new List<WithdrawalTypeDomainModel>()
            {
                new WithdrawalTypeDomainModel() { Id = 1, Name = "ATM", IsActive=true, IsDeleted=false  },
                new WithdrawalTypeDomainModel() { Id = 2, Name = "Aadhar", IsActive=true, IsDeleted=false},
                new WithdrawalTypeDomainModel() { Id = 3, Name = "FINO Bank Account", IsActive=true, IsDeleted=false  },
                new WithdrawalTypeDomainModel() { Id = 4, Name = "Any", IsActive=true, IsDeleted=false},
            };

            var summaryDataResult = new Tuple<List<WithdrawalTypeDomainModel>>(withdrawalTypeResult);

            //Act
            mockQueryWithdrawalTypeMasterDataRepository.Setup(x => x.GetWithdrawalTypeMaster()).ReturnsAsync(summaryDataResult);
            mockUnitOfWork.SetupProperty(repo => repo.QueryWithdrawalTypeMasterDataRepository, mockQueryWithdrawalTypeMasterDataRepository.Object);
            var result = await queryMasterDataManagerService.GetWithdrawalTypeMaster().ConfigureAwait(false) as OperationResult<List<WithdrawalTypeViewModel>>;

            //Assert
            mockQueryWithdrawalTypeMasterDataRepository.Verify(repo => repo.GetWithdrawalTypeMaster(), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count == 4);
        }

        [TestMethod]
        public async Task GetWithdrawalTypeMaster_WithNoData()
        {
            //Arrange
            var withdrawalTypeResult = new List<WithdrawalTypeDomainModel>()
            {
            };

            var summaryDataResult = new Tuple<List<WithdrawalTypeDomainModel>>(withdrawalTypeResult);

            //Act
            mockQueryWithdrawalTypeMasterDataRepository.Setup(x => x.GetWithdrawalTypeMaster()).ReturnsAsync(summaryDataResult);
            mockUnitOfWork.SetupProperty(repo => repo.QueryWithdrawalTypeMasterDataRepository, mockQueryWithdrawalTypeMasterDataRepository.Object);
            var result = await queryMasterDataManagerService.GetWithdrawalTypeMaster().ConfigureAwait(false) as OperationResult<List<WithdrawalTypeViewModel>>;

            //Assert
            mockQueryWithdrawalTypeMasterDataRepository.Verify(repo => repo.GetWithdrawalTypeMaster(), Times.Once);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Data.Count == 0);
        }
    }
}