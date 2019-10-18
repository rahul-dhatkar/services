using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryMerchantDataRepository : IQueryGenericSqlRepository<MerchantDataDomainModel>
    {
        Task<MerchantDataDomainModel> GetMerchantDataById(string refCode);
    }
}