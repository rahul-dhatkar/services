using Contesto.V2.Core.Data;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using Newtonsoft.Json;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Commands
{
    internal class CommandCustomerRepository : ICommandCustomerRepository
    {
        protected readonly IDataContext Context = null;
      
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandCustomerRepository(string connectionString) 
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        public async Task<long> CheckAndCreateCustomer(CustomerDomainModel model)
        {
            long insertedId = 0;
            if (model.Type == "F")
            {
                var parameters = new DynamicParameters();
                parameters.Add("@RefCode", model.RefCode, DbType.String, ParameterDirection.Input);
                var refCodeExists = await Context.ExecuteReadSqlAsync<CustomerDomainModel>("SELECT ID FROM Customers WHERE RefCode = @RefCode", parameters).ConfigureAwait(false);
               
                    if (refCodeExists.AsList().Count == 0)
                    {
                        parameters.Add("@Mobile", model.Mobile, DbType.String, ParameterDirection.Input);
                        var mobileExists = await Context.ExecuteSingleRecordReadSqlAsync<CustomerDomainModel>("SELECT TOP 1 ID FROM Customers WHERE Mobile = @Mobile", parameters).ConfigureAwait(false);
                        if (mobileExists == null)
                        {
                            parameters = new DynamicParameters();
                            parameters.Add("@RefCode", model.RefCode, DbType.String, ParameterDirection.Input);
                            parameters.Add("@FirstName", model.FirstName, DbType.String, ParameterDirection.Input);
                            parameters.Add("@LastName", model.LastName, DbType.String, ParameterDirection.Input);
                            parameters.Add("@Type", model.Type, DbType.String, ParameterDirection.Input);
                            parameters.Add("@Mobile", model.Mobile, DbType.String, ParameterDirection.Input);
                            parameters.Add("@IsVerified", model.IsVerified, DbType.Boolean, ParameterDirection.Input);
                            parameters.Add("@CreatedBy", model.RefCode, DbType.String, ParameterDirection.Input);
                            parameters.Add("@IsActive", true, DbType.Boolean, ParameterDirection.Input);
                            parameters.Add("@IsDeleted", false, DbType.Boolean, ParameterDirection.Input);
                          
                            await Context.ExecuteWriteSqlAsync("INSERT INTO dbo.Customers(RefCode,FirstName,LastName,Type,Mobile,IsVerified,CreatedBy,CreatedDateTime,IsActive,IsDeleted)values(@RefCode,@FirstName,@LastName,@Type,@Mobile,@IsVerified,@CreatedBy,GETDATE(),@IsActive,@IsDeleted)", parameters).ConfigureAwait(false);
                            insertedId = await Context.ExecuteSingleRecordReadSqlAsync<long>("SELECT TOP 1 Id FROM Customers WHERE Mobile = @Mobile AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
                            return insertedId;
                        }
                        else
                        {
                            parameters = new DynamicParameters();
                            parameters.Add("@RefCode", model.RefCode, DbType.String, ParameterDirection.Input);
                            parameters.Add("@Type", model.Type, DbType.String, ParameterDirection.Input);
                            parameters.Add("@Mobile", model.Mobile, DbType.String, ParameterDirection.Input);
                            parameters.Add("@ModifiedBy", model.RefCode, DbType.String, ParameterDirection.Input);
                           
                            await Context.ExecuteWriteSqlAsync("Update dbo.Customers set RefCode = @RefCode,Type = @Type,ModifiedBy = @RefCode,ModifiedDateTime = GETDATE() where Mobile = @Mobile", parameters).ConfigureAwait(false);
                            insertedId = await Context.ExecuteSingleRecordReadSqlAsync<long>("SELECT TOP 1 Id FROM Customers WHERE Mobile = @Mobile AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
                            return insertedId;
                        }
                    }
                    else
                    {
                      if(refCodeExists.AsList().Count > 0 && refCodeExists.AsList().Count < 2)
                        {
                            parameters = new DynamicParameters();
                            parameters.Add("@RefCode", model.RefCode, DbType.String, ParameterDirection.Input);
                            insertedId = await Context.ExecuteSingleRecordReadSqlAsync<long>("SELECT TOP 1 Id FROM Customers WHERE RefCode = @RefCode AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
                            return insertedId;
                        }
                       return 0;
                    }
            }
            else
            {
                var parameters = new DynamicParameters();
                parameters.Add("@Mobile", model.Mobile, DbType.String, ParameterDirection.Input);
                var mobileExists = await Context.ExecuteSingleRecordReadSqlAsync<CustomerDomainModel>("SELECT TOP 1 ID FROM Customers WHERE Mobile = @Mobile", parameters).ConfigureAwait(false);
                if (mobileExists == null)
                {
                    parameters.Add("@RefCode",null, DbType.String, ParameterDirection.Input);
                    parameters.Add("@FirstName", model.FirstName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@LastName", model.LastName, DbType.String, ParameterDirection.Input);
                    parameters.Add("@Type", model.Type, DbType.String, ParameterDirection.Input);
                    parameters.Add("@Mobile", model.Mobile, DbType.String, ParameterDirection.Input);
                    parameters.Add("@IsVerified", model.IsVerified, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@CreatedBy","Non-Fino", DbType.String, ParameterDirection.Input);
                    parameters.Add("@IsActive", true, DbType.Boolean, ParameterDirection.Input);
                    parameters.Add("@IsDeleted", false, DbType.Boolean, ParameterDirection.Input);
                  
                    await Context.ExecuteWriteSqlAsync("INSERT INTO dbo.[Customers](RefCode,FirstName,LastName,Type,Mobile,IsVerified,CreatedBy,CreatedDateTime,IsActive,IsDeleted)values(@RefCode,@FirstName,@LastName,@Type,@Mobile,@IsVerified,@CreatedBy,GETDATE(),@IsActive,@IsDeleted)", parameters).ConfigureAwait(false);
                    insertedId = await Context.ExecuteSingleRecordReadSqlAsync<long>("SELECT TOP 1 Id FROM Customers WHERE Mobile = @Mobile AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
                    return insertedId;
                }
                else
                {
                    parameters.Add("@Mobile", model.Mobile, DbType.String, ParameterDirection.Input);
                    insertedId = await Context.ExecuteSingleRecordReadSqlAsync<long>("SELECT TOP 1 Id FROM Customers WHERE Mobile = @Mobile AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
                    return insertedId;
                }
            }
        }
    }
}