using System.Collections.Generic;
using System.Threading.Tasks;
using FinoBank.Cola.Repository.DomainModels;

namespace FinoBank.Cola.Repository.Interfaces
{
    public interface IQueryUserTokenHistoryRepository
    {
        Task<List<UserTokenDomainModel>> CheckForUserTokenHistory(int timeStamp);
    }
}

