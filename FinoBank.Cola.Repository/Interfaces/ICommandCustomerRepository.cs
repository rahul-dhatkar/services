using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface ICommandCustomerRepository //: ICommandGenericRepository<CustomerDomainModel, long>
    {
        Task<long> CheckAndCreateCustomer(CustomerDomainModel model);
    }
}