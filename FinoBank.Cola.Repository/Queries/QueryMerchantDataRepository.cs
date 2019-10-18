using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryMerchantDataRepository : QueryGenericSqlRepository<MerchantDataDomainModel>, IQueryMerchantDataRepository
    {
        internal QueryMerchantDataRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<MerchantDataDomainModel> GetMerchantDataById(string refCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RefCode", refCode, DbType.String, ParameterDirection.Input);

            var results = await Context.ExecuteReadSqlAsync<MerchantDataDomainModel>("SELECT [RefCode],[Name],[MerchantTypeId],[AddressLine1],[AddressLine2],[IsDeleted],[IsActive],[ModifiedBy],[ModifiedDateTime],[CreatedDateTime],[CreatedBy],[MobileNumber],[Fax],[Extension],[Telephone],[Email],[PinCode],[Country],[State],[City],[District],[LimitSetupDate],[DepositCashBalance],[WithdrawCashBalance],[IsOnline],[Latitude],[Longitude],[Rating],[WithdrawalTypes] from [dbo].[vwGetAllMerchant] where RefCode = @RefCode", parameters).ConfigureAwait(false);

            return results.FirstOrDefault();
        }
    }
}