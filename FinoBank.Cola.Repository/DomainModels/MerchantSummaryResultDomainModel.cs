using Contesto.V2.Core.Infrastructure.Data.Base;
using System;

namespace FinoBank.Cola.Repository.DomainModels
{
    public class MerchantSummaryResultDomainModel : BaseDomainMasterModel<int>
    {
        public string RefCode { get; set; }
        public byte MerchantTypeId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string District { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PinCode { get; set; }
        public string Email { get; set; }
        public string Telephone { get; set; }
        public string Extension { get; set; }
        public string Fax { get; set; }
        public string MobileNumber { get; set; }
        public decimal? DepositCashBalance { get; set; }
        public decimal? WithdrawCashBalance { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public bool IsOnline { get; set; }
        public int MerchantId { get; set; }
        public DateTime? LimitSetupDate { get; set; }
    }
}