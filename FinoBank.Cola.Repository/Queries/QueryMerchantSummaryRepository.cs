using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryMerchantSummaryRepository : QueryGenericSqlRepository<MerchantSearchResultDomainModel>, IQueryMerchantSummaryRepository
    {
        internal QueryMerchantSummaryRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<MerchantSearchResultDomainModel>> GetAllData(string searchText = null)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SearchText", searchText, DbType.String, ParameterDirection.Input);
            var results = await Context.ExecuteReadSqlAsync<MerchantSearchResultDomainModel>("SELECT RefCode,Name,MerchantTypeId,AddressLine1,AddressLine2," +
                "IsDeleted,IsActive,ModifiedBy,ModifiedDateTime ,CreatedDateTime ,CreatedBy,MobileNumber,Fax,Extension," +
                "Telephone,Email,PinCode ,Country,State,City,District,LimitSetupDate ,DepositCashBalance,WithdrawCashBalance ,IsOnline,Latitude ,Longitude,Rating,WithdrawalTypes " +
                "from vwGetAllMerchant WHERE  IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
            return results.ToList();
        }

        public async Task<MerchantSearchResultDomainModel> GetMerchantDetailsByRefCode(string refCode)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RefCode", refCode, DbType.String, ParameterDirection.Input);
            var merchantResults = await Context.ExecuteReadSqlAsync<MerchantSearchResultDomainModel>("SELECT Id ,RefCode,Name,MerchantTypeId,AddressLine1,AddressLine2 ," +
            " District, City,[State], Country, PinCode, Email, Telephone, Extension," +
            " Fax, MobileNumber, CreatedBy, CreatedDateTime, ModifiedBy, ModifiedDateTime," +
            " IsActive, IsDeleted FROM Merchants where RefCode = @RefCode", parameters).ConfigureAwait(false);
            if(merchantResults.Count() > 0)
            {
                return merchantResults.FirstOrDefault();
            }
            var results = await Context.ExecuteReadSqlAsync<MerchantSearchResultDomainModel>("SELECT Id, RefCode, Name, MerchantTypeId, AddressLine1, AddressLine2, IsDeleted ," +
                " IsActive,ModifiedBy,ModifiedDateTime,CreatedDateTime,CreatedBy,MobileNumber,Fax,Extension,Telephone,Email,PinCode,Country,State,City ," +
                " District,LimitSetupDate,DepositCashBalance,WithdrawCashBalance,IsOnline,Latitude,Longitude,Rating,WithdrawalTypes from vwGetAllMerchant " +
                " where RefCode = @RefCode", parameters).ConfigureAwait(false);
            return results.FirstOrDefault();
        }
    }
}