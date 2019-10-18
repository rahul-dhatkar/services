using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Repository.DomainModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    public interface IQueryCustomerSummaryManagerService
    {
        Task<OperationResult<CustomerDomainModel>> GetCustomerDetailsByMobileNumber(string mobileNumber);
    }
}