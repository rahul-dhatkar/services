using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface ICommandMerchantSetupRepository //: ICommandGenericRepository<MerchantSetupDomainModel, int>
    {
        Task<int> Update(MerchantSetupDomainModel model);
    }
}