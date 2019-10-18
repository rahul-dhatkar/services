using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryUserTokenHistoryRepository : QueryGenericSqlRepository<UserTokenDomainModel>, IQueryUserTokenHistoryRepository
    {
        internal QueryUserTokenHistoryRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<List<UserTokenDomainModel>> CheckForUserTokenHistory(int timeStamp)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@SlaInMinutes", timeStamp, DbType.Int32, ParameterDirection.Input);
            var resultstring =  " SELECT Id , UserId , AccessToken , AccessTokenExpiresDateTime , RefreshToken , RefreshTokenSource , " +
                                " RefreshTokenExpiresDateTime , IsActive , IsDeleted " + 
                                " FROM UserTokens where IsActive = 0 and IsDeleted = 1 " ;
            var results = await Context.ExecuteReadSqlAsync<UserTokenDomainModel>(resultstring, parameters).ConfigureAwait(false);
            foreach (var record in results)
            {
                    parameters = new DynamicParameters();
                    parameters.Add("@UserId", record.UserId, DbType.String, ParameterDirection.Input);
                    parameters.Add("@AccessToken", record.AccessToken, DbType.String, ParameterDirection.Input);
                    parameters.Add("@AccessTokenExpiresDateTime", record.AccessTokenExpiresDateTime, DbType.DateTimeOffset, ParameterDirection.Input);
                    parameters.Add("@RefreshToken", record.RefreshToken, DbType.String, ParameterDirection.Input);
                    parameters.Add("@RefreshTokenSource", record.RefreshTokenSource, DbType.String, ParameterDirection.Input);
                    parameters.Add("@RefreshTokenExpiresDateTime", record.RefreshTokenExpiresDateTime, DbType.DateTimeOffset, ParameterDirection.Input);
                    parameters.Add("@IsActive", false, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@IsDeleted", true, DbType.Boolean, ParameterDirection.Input);
                    var insertString = "INSERT INTO UserTokensHistory (UserId,AccessToken,AccessTokenExpiresDateTime,RefreshToken,RefreshTokenSource,RefreshTokenExpiresDateTime," +
                    "IsActive,IsDeleted)" +
                    "values" +
                    "(@UserId, @AccessToken,@AccessTokenExpiresDateTime,@RefreshToken,@RefreshTokenSource,@RefreshTokenExpiresDateTime," +
                    " @IsActive,@IsDeleted)";
                    await Context.ExecuteWriteSqlAsync(insertString, parameters).ConfigureAwait(false);

                    var deleteString = "Delete from UserTokens where UserId = @UserId and IsActive = 0 and IsDeleted=1";
                    await Context.ExecuteReadSqlAsync<UserTokenDomainModel>(deleteString, parameters).ConfigureAwait(false);
              
            }
            return results.ToList();
        }
    }
}
