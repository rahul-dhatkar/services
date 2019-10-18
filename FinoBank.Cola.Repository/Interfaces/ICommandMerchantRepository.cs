using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface ICommandMerchantRepository //: ICommandGenericRepository<MerchantDomainModel, int>
    {
        Task<long> Create(MerchantDomainModel model);
        Task<int> Update(MerchantDomainModel model);
        Task<bool> Delete(string refCode);
    }
} 