using Contesto.V2.Core.Infrastructure.Data.Base;

namespace FinoBank.Cola.Repository.DomainModels
{
    public class MerchantTypeDomainModel : BaseDomainMasterModel<int>
    {
        public string Code { get; set; }
    }
}