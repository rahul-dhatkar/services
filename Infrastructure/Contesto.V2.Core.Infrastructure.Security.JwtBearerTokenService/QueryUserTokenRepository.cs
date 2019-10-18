//-------------------------------------------------------------------------------------------
//** Copyright © 2018, Fulcrum Digital                                  **
//** All rights reserved.                                                                  **
//**                                                                                       **
//** Redistribution, re-engineering or use of this code - in source                        **
//** or binary forms with or without modifications, are not                                **
//** permitted without prior written consent from appropriate person                       **
//** in Fulcrum Digital                                                 **
//**                                                                                       **
//**                                                                                       **
//** Author    : Fulcrum World Wide                                                        **
//** Created   : 08-06-18                                                                  **
//** Purpose   : QueryUserTokenRepository                                                  **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Dtos;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using System.Linq;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService
{
    /// <summary>
    /// 
    /// </summary>
    /// <seealso cref="QueryGenericRepository{UserTokenDomainModel}" />
    /// <seealso cref="IQueryUserTokenRepository" />
    internal class QueryUserTokenRepository : QueryGenericSqlRepository<UserTokenDomainModel>, IQueryUserTokenRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryUserTokenRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public QueryUserTokenRepository(string connectionString) : base(connectionString)
        {

        }

        public async Task<UserTokenDomainModel> GetByUserId(string userId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", userId, DbType.String, ParameterDirection.Input);
            var queryString = "SELECT  Id , UserId , AccessToken , AccessTokenExpiresDateTime ," + 
            " RefreshToken, RefreshTokenSource,RefreshTokenExpiresDateTime FROM  " +
            " UserTokens with(NOLOCK) WHERE UserId = @UserId AND IsActive=1 AND IsDeleted =0" ;
            var result = await Context.ExecuteReadSqlAsync<UserTokenDomainModel>(queryString, parameters).ConfigureAwait(false);
            return result.FirstOrDefault();
        }
    }
}
