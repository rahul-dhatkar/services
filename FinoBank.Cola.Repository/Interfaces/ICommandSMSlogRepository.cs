using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface ICommandSMSlogRepository //: ICommandGenericRepository<SMSlogDomainModel, int>
    {
        Task<long> Create(SMSlogDomainModel model);
        Task<bool> Delete(int timeStamp);
    }
}
