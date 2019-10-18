using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryCustomerSummaryRepository : IQueryGenericSqlRepository<CustomerDomainModel>
    {
        Task<Tuple<CustomerDomainModel>> GetCustomerDetailsByMobileNumber(string mobileNumber);
    }
}