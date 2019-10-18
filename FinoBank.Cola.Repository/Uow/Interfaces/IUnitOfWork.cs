using FinoBank.Cola.Repository.Interfaces;

namespace FinoBank.Cola.Repository.Uom.Interfaces
{
    /// <summary>
    /// Interface Unit Of Work
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        ICommandSampleRepository CommandSampleRepository { get; set; }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        ICommandTransactionRequestsRepository CommandTransactionRequestsRepository { get; set; }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        ICommandTransactionFeedbacksRepository CommandTransactionFeedbacksRepository { get; set; }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        ICommandUpdateTransactionRequestsRepository CommandUpdateTransactionRequestsRepository { get; set; }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        ICommandMerchantRepository CommandCreateMerchantRepository { get; set; }

        /// <summary>
        /// Gets or sets the command Sample repository.
        /// </summary>
        /// <value>
        /// The command Sample repository.
        /// </value>
        ICommandMerchantSetupRepository CommandMerchantSetupRepository { get; set; }

        /// <summary>
        /// Gets or sets the query Sample repository.
        /// </summary>
        /// <value>
        /// The query Sample repository.
        /// </value>
        IQuerySampleRepository QuerySampleRepository { get; set; }

        IQueryAcceptTransactionRequestRepository QueryAcceptTransactionRequestRepository { get; set; }

        /// <summary>
        /// Gets or sets the query Merchant Search repository.
        /// </summary>
        /// <value>
        /// The query Merchant Search repository.
        /// </value>
        IQueryMerchantSearchRepository QueryMerchantSearchRepository { get; set; }

        /// <summary>
        /// Gets or sets the query customer transaction summary repository.
        /// </summary>
        /// <value>
        /// The query Customer Transaction Summary repository.
        /// </value>
        IQueryTransactionSummaryRepository QueryTransactionSummaryRepository { get; set; }

        /// <summary>
        /// Gets or sets the query customer transaction summary repository.
        /// </summary>
        /// <value>
        /// The query Customer Transaction Summary repository.
        /// </value>
        IQueryTransactionResultRepository QueryTransactionResultRepository { get; set; }

        /// <summary>
        /// Gets or sets the query merchant summary repository.
        /// </summary>
        /// <value>
        /// The query merchant summary repository.
        /// </value>
        IQueryMerchantSummaryRepository QueryMerchantSummaryRepository { get; set; }

        IQueryMerchantDataRepository QueryMerchantDataRepository { get; set; }
        /// <summary>
        /// Gets or sets the query master data repository.
        /// </summary>
        /// <value>
        /// The query master data repository.
        /// </value>
        IQueryMerchantTypeMasterDataRepository QueryMerchantTypeMasterDataRepository { get; set; }

        IQueryTransactionStatusMasterDataRepository QueryTransactionStatusMasterDataRepository { get; set; }
        IQueryTransactionTypeMasterDataRepository QueryTransactionTypeMasterDataRepository { get; set; }
        IQueryWithdrawalTypeMasterDataRepository QueryWithdrawalTypeMasterDataRepository { get; set; }

        /// <summary>
        /// Gets or sets the command customer repository.
        /// </summary>
        /// <value>
        /// The command customer repository.
        /// </value>
        ICommandCustomerRepository CommandCustomerRepository { get; set; }

        ICommandSMSlogRepository CommandSMSlogRepository { get; set; }

        /// <summary>
        /// Gets or sets the query generate otp service repository.
        /// </summary>
        /// <value>
        /// The query generate otp service repository.
        /// </value>
        IQueryOTPServiceRepository QueryGenerateOTPServiceRepository { get; set; }

        IQueryMerchantBulkUploadRepository QueryMerchantBulkUploadRepository { get; set; }

        IQueryUserTokenHistoryRepository QueryUserTokenHistoryRepository { get; set; }

        /// <summary>
        /// Gets or sets the query customer summary repository.
        /// </summary>
        /// <value>
        /// The query customer summary repository.
        /// </value>
        IQueryCustomerSummaryRepository QueryCustomerSummaryRepository { get; set; }

        /// <summary>
        /// Gets or sets the query check for merchant acceptance expiration repository.
        /// </summary>
        /// <value>
        /// The query check for merchant acceptance expiration repository.
        /// </value>
        IQueryCheckForMerchantAcceptanceExpirationRepository QueryCheckForMerchantAcceptanceExpirationRepository { get; set; }

        /// <summary>
        /// Gets or sets the query check for transaction request expiration repository.
        /// </summary>
        /// <value>
        /// The query check for transaction request expiration repository.
        /// </value>
        IQueryCheckForTransactionRequestExpirationRepository QueryCheckForTransactionRequestExpirationRepository { get; set; }

        /// <summary>
        /// Gets or sets the command activity repository.
        /// </summary>
        /// <value>
        /// The command activity repository.
        /// </value>
        ICommandActivityRepository CommandActivityRepository { get; set; }
    }
}