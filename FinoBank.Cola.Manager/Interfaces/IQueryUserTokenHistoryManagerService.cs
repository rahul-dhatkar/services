using System;
using System.Collections.Generic;
using System.Text;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Interfaces
{
    public interface IQueryUserTokenHistoryManagerService
    {
        Task<OperationResult<List<UserTokenViewModel>>> CheckForUserTokenHistory(int timeStamp);
    }
}
