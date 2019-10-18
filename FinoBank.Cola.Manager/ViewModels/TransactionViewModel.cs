using Contesto.V2.Core.Common.ViewModel.Base;
using System;

namespace FinoBank.Cola.Manager.ViewModels
{
    /// <summary>
    ///
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseViewModel{System.Int32}" />
    public class TransactionViewModel : BaseViewModel<long>
    {
        public string ReferenceNumber { get; set; }

        public int MerchantId { get; set; }

        public long CustomerId { get; set; }

        public float? CustomerLatitude { get; set; }

        public float? CustomerLongitude { get; set; }

        public int TransactionTypeId { get; set; }

        public int? WithdrawalTypeId { get; set; }

        public DateTime RequestedDateTime { get; set; }

        public int RequestedAmount { get; set; }

        public DateTime? RequestCompletedDateTime { get; set; }

        public int TransactionStatusId { get; set; }

        public int? ActualAmount { get; set; }

        public string Remarks { get; set; }

        //----------------------------------------

        public DateTime? Validity { get; set; }

        public string MerchantName { get; set; }

        public string MerchantAddress { get; set; }

        public string MerchantType { get; set; }

        public string MerchantMobile { get; set; }

        public string TransactionStatus { get; set; }

        public string TransactionType { get; set; }

        public string WithdrawalType { get; set; }

        public string CustomerMobile { get; set; }

        public string CustomerType { get; set; }

        public Guid? UniqueId { get; set; }

        public string CustomerName { get; set; }

        public float? MerchantLatitude { get; set; }

        public float? MerchantLongitude { get; set; }
    }
}