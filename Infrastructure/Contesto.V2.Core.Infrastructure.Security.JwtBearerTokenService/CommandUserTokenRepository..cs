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
//** Purpose   : CommandUserTokenRepository                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using System.Data;
using System.Threading.Tasks;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Dtos;
using Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Data;
using System.Data.SqlClient;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Contesto.V2.Core.Infrastructure.Security.JwtBearerTokenService
{
    /// <summary>
    /// CommandUserTokenRepository
    /// </summary>
    /// <seealso cref="CommandGenericRepository{UserTokenDomainModel, long}" />
    /// <seealso cref="ICommandUserTokenRepository" />
    internal class CommandUserTokenRepository : ICommandUserTokenRepository //CommandGenericRepository<UserTokenDomainModel, long>, 
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandUserTokenRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public CommandUserTokenRepository(string connectionString) //: base(connectionString)
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        public async Task<long> CreateTokens(UserTokenDomainModel model)
        {
            bool resultStatus = false;
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", model.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("@AccessToken", model.AccessToken, DbType.String, ParameterDirection.Input);
            parameters.Add("@AccessTokenExpiresDateTime", model.AccessTokenExpiresDateTime, DbType.DateTimeOffset, ParameterDirection.Input);
            parameters.Add("@RefreshToken", model.RefreshToken, DbType.String, ParameterDirection.Input);
            parameters.Add("@RefreshTokenSource", model.RefreshTokenSource, DbType.String, ParameterDirection.Input);
            parameters.Add("@RefreshTokenExpiresDateTime", model.RefreshTokenExpiresDateTime, DbType.DateTimeOffset, ParameterDirection.Input);
            parameters.Add("@IsActive", true, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@IsDeleted", false , DbType.Boolean, ParameterDirection.Input);
            var queryInsert = "INSERT INTO dbo.UserTokens (UserId,AccessToken,AccessTokenExpiresDateTime,RefreshToken," +
            "RefreshTokenSource,RefreshTokenExpiresDateTime,IsActive,IsDeleted) values (@UserId,@AccessToken,@AccessTokenExpiresDateTime,@RefreshToken," +
            "@RefreshTokenSource,@RefreshTokenExpiresDateTime,@IsActive,@IsDeleted)";
            await Context.ExecuteWriteSqlAsync(queryInsert, parameters).ConfigureAwait(false);
            var queryString = "SELECT  Id FROM dbo.UserTokens WHERE UserId = @UserId and IsActive=1 and IsDeleted=0";
            long result = await Context.ExecuteSingleRecordReadSqlAsync<long>(queryString, parameters).ConfigureAwait(false);
            //resultStatus = parameters.Get<long>("@ResultStatus");
            return result;
        }

        /// <summary>
        /// Deletes the expired tokens.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="refreshToken">The refresh token.</param>
        /// <param name="refreshTokenSource">The refresh token source.</param>
        /// <returns></returns>
        public async Task<bool> DeleteExpiredTokens(string userId, string refreshToken, string refreshTokenSource)
        {
            bool resultStatus = false;
            dynamic insertedId = null;
            var parameters = new DynamicParameters();
            int idExists = 0;
            parameters.Add("@UserId", userId, DbType.String, ParameterDirection.Input);
            parameters.Add("@RefreshToken", refreshToken, DbType.String, ParameterDirection.Input);
            parameters.Add("@RefreshTokenSource", refreshTokenSource, DbType.String, ParameterDirection.Input);
            parameters.Add("@ResultStatus", resultStatus, DbType.Boolean, ParameterDirection.Output);
            var queryString = "Select Id from UserTokens WHERE UserId = @UserId and IsActive = 1 and IsDeleted = 0" ;
            insertedId = await Context.ExecuteSingleRecordReadSqlAsync<int>(queryString, parameters).ConfigureAwait(false);

            if (insertedId != null && insertedId > 0)
            {
                if (userId != null)
                {
                    await Context.ExecuteWriteSqlAsync(" Update UserTokens set IsActive = 0 , IsDeleted = 1 WHERE UserId = @UserId and IsActive = 1 and IsDeleted = 0 ", parameters).ConfigureAwait(false);
                    return resultStatus = true;
                }
                else if (refreshToken != null)
                {
                    await Context.ExecuteWriteSqlAsync("Update UserTokens set IsActive = 0, IsDeleted = 1 WHERE RefreshToken = @RefreshToken and IsActive = 1 and IsDeleted = 0", parameters).ConfigureAwait(false);
                    return resultStatus = true;
                }
                else if (refreshTokenSource != null)
                {
                    await Context.ExecuteWriteSqlAsync("Update UserTokens set IsActive = 0, IsDeleted = 1 WHERE RefreshTokenSource = @RefreshTokenSource and IsActive = 1 and IsDeleted = 0", parameters).ConfigureAwait(false);
                    return resultStatus = true;
                }
                else
                {
                    await Context.ExecuteWriteSqlAsync("Update UserTokens set IsActive = 0, IsDeleted = 1 WHERE RefreshTokenExpiresDateTime < GETUTCDATE() and IsActive = 1 and IsDeleted = 0", parameters).ConfigureAwait(false);
                    return resultStatus = true;
                }
            }
            //resultStatus = parameters.Get<bool>("@ResultStatus");
            return resultStatus;
        }

        public async Task<long> UpdateTokens(UserTokenDomainModel model)
        {
            bool resultStatus = false;
            var parameters = new DynamicParameters();
            parameters.Add("@UserId", model.UserId, DbType.String, ParameterDirection.Input);
            parameters.Add("@AccessTokenExpiresDateTime", model.AccessTokenExpiresDateTime, DbType.DateTimeOffset, ParameterDirection.Input);
            parameters.Add("@RefreshTokenExpiresDateTime", model.RefreshTokenExpiresDateTime, DbType.DateTimeOffset, ParameterDirection.Input);
            var queryUpdate = "UPDATE dbo.UserTokens with (ROWLOCK) SET AccessTokenExpiresDateTime = @AccessTokenExpiresDateTime," +
            "RefreshTokenExpiresDateTime = @RefreshTokenExpiresDateTime WHERE UserId = @UserId and IsActive = 1 and IsDeleted = 0";
            await Context.ExecuteWriteSqlAsync(queryUpdate, parameters).ConfigureAwait(false);
            var queryString = "SELECT  Id FROM dbo.UserTokens WHERE UserId = @UserId and IsActive = 1 and IsDeleted = 0";
            long result = await Context.ExecuteSingleRecordReadSqlAsync<long>(queryString, parameters).ConfigureAwait(false);
            //resultStatus = parameters.Get<long>("@ResultStatus");
            return result;
        }
    }
}
