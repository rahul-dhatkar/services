using FinoBank.Cola.Repository.Commands;
using FinoBank.Cola.Repository.Interfaces;
using FinoBank.Cola.Repository.Queries;
using FinoBank.Cola.Repository.Uom.Interfaces;

namespace FinoBank.Cola.Repository.Uom
{
    /// <summary>
    /// Master Unit Of Work
    /// </summary>
    /// <seealso cref="FinoBank.Cola.Repository.Uom.Interfaces.IUnitOfWork" />
    /// <seealso cref="IUnitOfWork" />
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// The command Sample repository
        /// </summary>
        private ICommandSampleRepository _commandSampleRepository;

        /// <summary>
        /// The query Sample repository
        /// </summary>
        private IQuerySampleRepository _querySampleRepository;

        private IQueryMerchantBulkUploadRepository _queryMerchantBulkUploadRepository;

        /// <summary>
        /// The query Merchant Search repository
        /// </summary>
        private IQueryMerchantSearchRepository _queryMerchantSearchRepository;

        private IQueryAcceptTransactionRequestRepository _queryAcceptTransactionRequestRepository;

        private IQueryUserTokenHistoryRepository _queryUserTokenHistoryRepository;

        private IQueryCustomerSummaryRepository _queryCustomerSummaryRepository;

        /// <summary>
        /// The query transaction history repository
        /// </summary>
        private IQueryTransactionSummaryRepository _queryTransactionSummaryRepository;

        private IQueryTransactionResultRepository _queryTransactionResultRepository;

        private IQueryMerchantSummaryRepository _queryMerchantSummaryRepository;

        private IQueryOTPServiceRepository _queryGenerateOTPServiceRepository;

        private IQueryMerchantTypeMasterDataRepository _queryMerchantTypeMasterDataRepository;
        private IQueryTransactionStatusMasterDataRepository _queryTransactionStatusMasterDataRepository;
        private IQueryTransactionTypeMasterDataRepository _queryTransactionTypeMasterDataRepository;
        private IQueryWithdrawalTypeMasterDataRepository _queryWithdrawalTypeMasterDataRepository;
        private IQueryMerchantDataRepository _queryMerchantDataRepository;

        private IQueryCheckForTransactionRequestExpirationRepository _queryCheckForTransactionRequestExpirationRepository;

        private IQueryCheckForMerchantAcceptanceExpirationRepository _queryCheckForMerchantAcceptanceExpirationRepository;

        /// <summary>
        /// The Command Transaction Request repository
        /// </summary>
        private ICommandTransactionRequestsRepository _commandTransactionRequestsRepository;

        /// <summary>
        /// The Command Transaction Request repository
        /// </summary>
        private ICommandTransactionFeedbacksRepository _commandTransactionFeedbacksRepository;

        /// <summary>
        /// The Command Transaction Request repository
        /// </summary>
        private ICommandUpdateTransactionRequestsRepository _commandUpdateTransactionRequestsRepository;

        /// <summary>
        /// The Command Transaction Request repository
        /// </summary>
        private ICommandMerchantRepository _commandCreateMerchantRepository;

        /// <summary>
        /// The Command Transaction Request repository
        /// </summary>
        private ICommandMerchantSetupRepository _commandMerchantSetupRepository;

        /// <summary>
        /// The command customer repository
        /// </summary>
        private ICommandCustomerRepository _commandCustomerRepository;

        /// <summary>
        /// The command activity repository
        /// </summary>
        private ICommandActivityRepository _commandActivityRepository;

        private ICommandSMSlogRepository _commandSMSlogRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UnitOfWork" /> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public UnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public ICommandSampleRepository CommandSampleRepository
        {
            get
            {
                return _commandSampleRepository ?? new CommandSampleRepository(_connectionString);
            }
            set
            {
                _commandSampleRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the query Sample repository.
        /// </summary>
        /// <value>
        /// The query Sample repository.
        /// </value>
        public IQuerySampleRepository QuerySampleRepository
        {
            get
            {
                return _querySampleRepository ?? new QuerySampleRepository(_connectionString);
            }
            set
            {
                _querySampleRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the query Merchant Search repository.
        /// </summary>
        /// <value>
        /// The query Merchant Search repository.
        /// </value>
        public IQueryMerchantSearchRepository QueryMerchantSearchRepository
        {
            get
            {
                return _queryMerchantSearchRepository ?? new QueryMerchantSearchRepository(_connectionString);
            }
            set
            {
                _queryMerchantSearchRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the query customer transaction summary repository.
        /// </summary>
        /// <value>
        /// The query Customer Transaction Summary repository.
        /// </value>
        public IQueryTransactionSummaryRepository QueryTransactionSummaryRepository
        {
            get
            {
                return _queryTransactionSummaryRepository ?? new QueryTransactionSummaryRepository(_connectionString);
            }
            set
            {
                _queryTransactionSummaryRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public ICommandTransactionRequestsRepository CommandTransactionRequestsRepository
        {
            get
            {
                return _commandTransactionRequestsRepository ?? new CommandTransactionRequestsRepository(_connectionString);
            }
            set
            {
                _commandTransactionRequestsRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public ICommandTransactionFeedbacksRepository CommandTransactionFeedbacksRepository
        {
            get
            {
                return _commandTransactionFeedbacksRepository ?? new CommandTransactionFeedbacksRepository(_connectionString);
            }
            set
            {
                _commandTransactionFeedbacksRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public ICommandUpdateTransactionRequestsRepository CommandUpdateTransactionRequestsRepository
        {
            get
            {
                return _commandUpdateTransactionRequestsRepository ?? new CommandUpdateTransactionRequestsRepository(_connectionString);
            }
            set
            {
                _commandUpdateTransactionRequestsRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public ICommandMerchantRepository CommandCreateMerchantRepository
        {
            get
            {
                return _commandCreateMerchantRepository ?? new CommandMerchantRepository(_connectionString);
            }
            set
            {
                _commandCreateMerchantRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public ICommandMerchantSetupRepository CommandMerchantSetupRepository
        {
            get
            {
                return _commandMerchantSetupRepository ?? new CommandMerchantSetupRepository(_connectionString);
            }
            set
            {
                _commandMerchantSetupRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public IQueryTransactionResultRepository QueryTransactionResultRepository
        {
            get
            {
                return _queryTransactionResultRepository ?? new QueryTransactionResultRepository(_connectionString);
            }
            set
            {
                _queryTransactionResultRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public IQueryMerchantSummaryRepository QueryMerchantSummaryRepository
        {
            get
            {
                return _queryMerchantSummaryRepository ?? new QueryMerchantSummaryRepository(_connectionString);
            }
            set
            {
                _queryMerchantSummaryRepository = value;
            }
        }

        public IQueryMerchantDataRepository QueryMerchantDataRepository
        {
            get
            {
                return _queryMerchantDataRepository ?? new QueryMerchantDataRepository(_connectionString);
            }
            set
            {
                _queryMerchantDataRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command customer repository.
        /// </summary>
        /// <value>
        /// The command customer repository.
        /// </value>
        public ICommandCustomerRepository CommandCustomerRepository
        {
            get
            {
                return _commandCustomerRepository ?? new CommandCustomerRepository(_connectionString);
            }
            set
            {
                _commandCustomerRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command customer repository.
        /// </summary>
        /// <value>
        /// The command customer repository.
        /// </value>
        public ICommandSMSlogRepository CommandSMSlogRepository
        {
            get
            {
                return _commandSMSlogRepository ?? new CommandSMSlogRepository(_connectionString);
            }
            set
            {
                _commandSMSlogRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        public IQueryOTPServiceRepository QueryGenerateOTPServiceRepository
        {
            get
            {
                return _queryGenerateOTPServiceRepository ?? new QueryOTPServiceRepository(_connectionString);
            }
            set
            {
                _queryGenerateOTPServiceRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the query customer summary repository.
        /// </summary>
        /// <value>
        /// The query customer summary repository.
        /// </value>
        public IQueryCustomerSummaryRepository QueryCustomerSummaryRepository
        {
            get
            {
                return _queryCustomerSummaryRepository ?? new QueryCustomerSummaryRepository(_connectionString);
            }
            set
            {
                _queryCustomerSummaryRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the query check for transaction request expiration repository.
        /// </summary>
        /// <value>
        /// The query check for transaction request expiration repository.
        /// </value>
        public IQueryCheckForTransactionRequestExpirationRepository QueryCheckForTransactionRequestExpirationRepository
        {
            get
            {
                return _queryCheckForTransactionRequestExpirationRepository ?? new QueryCheckForTransactionRequestExpirationRepository(_connectionString);
            }
            set
            {
                _queryCheckForTransactionRequestExpirationRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the query check for merchant acceptance expiration repository.
        /// </summary>
        /// <value>
        /// The query check for merchant acceptance expiration repository.
        /// </value>
        public IQueryCheckForMerchantAcceptanceExpirationRepository QueryCheckForMerchantAcceptanceExpirationRepository
        {
            get
            {
                return _queryCheckForMerchantAcceptanceExpirationRepository ?? new QueryCheckForMerchantAcceptanceExpirationRepository(_connectionString);
            }
            set
            {
                _queryCheckForMerchantAcceptanceExpirationRepository = value;
            }
        }

        /// <summary>
        /// Gets or sets the command activity repository.
        /// </summary>
        /// <value>
        /// The command activity repository.
        /// </value>
        public ICommandActivityRepository CommandActivityRepository
        {
            get
            {
                return _commandActivityRepository ?? new CommandActivityRepository(_connectionString);
            }
            set
            {
                _commandActivityRepository = value;
            }
        }

        public IQueryMerchantTypeMasterDataRepository QueryMerchantTypeMasterDataRepository
        {
            get
            {
                return _queryMerchantTypeMasterDataRepository ?? new QueryMerchantTypeMasterDataRepository(_connectionString);
            }
            set
            {
                _queryMerchantTypeMasterDataRepository = value;
            }
        }

        public IQueryTransactionStatusMasterDataRepository QueryTransactionStatusMasterDataRepository
        {
            get
            {
                return _queryTransactionStatusMasterDataRepository ?? new QueryTransactionStatusMasterDataRepository(_connectionString);
            }
            set
            {
                _queryTransactionStatusMasterDataRepository = value;
            }
        }

        public IQueryTransactionTypeMasterDataRepository QueryTransactionTypeMasterDataRepository
        {
            get
            {
                return _queryTransactionTypeMasterDataRepository ?? new QueryTransactionTypeMasterDataRepository(_connectionString);
            }
            set
            {
                _queryTransactionTypeMasterDataRepository = value;
            }
        }

        public IQueryWithdrawalTypeMasterDataRepository QueryWithdrawalTypeMasterDataRepository
        {
            get
            {
                return _queryWithdrawalTypeMasterDataRepository ?? new QueryWithdrawalTypeMasterDataRepository(_connectionString);
            }
            set
            {
                _queryWithdrawalTypeMasterDataRepository = value;
            }
        }

        public IQueryAcceptTransactionRequestRepository QueryAcceptTransactionRequestRepository
        {
            get
            {
                return _queryAcceptTransactionRequestRepository ?? new QueryAcceptTransactionRequestRepository(_connectionString);
            }
            set
            {
                _queryAcceptTransactionRequestRepository = value;
            }
        }

        public IQueryUserTokenHistoryRepository QueryUserTokenHistoryRepository
        {
            get
            {
                return _queryUserTokenHistoryRepository ?? new QueryUserTokenHistoryRepository(_connectionString);
            }
            set
            {
                _queryUserTokenHistoryRepository = value;
            }
        }

        public IQueryMerchantBulkUploadRepository QueryMerchantBulkUploadRepository
        {
            get
            {
                return _queryMerchantBulkUploadRepository ?? new QueryMerchantBulkUploadRepository(_connectionString);
            }
            set
            {
                _queryMerchantBulkUploadRepository = value;
            }
        }
    }
}