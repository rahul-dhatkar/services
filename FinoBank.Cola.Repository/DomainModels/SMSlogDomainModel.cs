using System.Collections.Generic;
using Contesto.V2.Core.Infrastructure.Data.Base;

namespace FinoBank.Cola.Repository.DomainModels
{
    public class SMSlogDomainModel : BaseDomainModel<int>
    {
        public long TransactionId { get; set; }
        public int MerchantId { get; set; }
       // public List<MerchantId> MerchantId { get; set; }
    }

    //public class MerchantId
    //{
    //    public int Id { get; set; }
    //}
}
