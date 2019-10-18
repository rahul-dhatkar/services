using Contesto.V2.Core.Data;
using Contesto.V2.Core.Data.Interfaces;
using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Commands
{
    internal class CommandMerchantRepository : ICommandMerchantRepository 
    {
        protected readonly IDataContext Context = null;
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandSampleRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandMerchantRepository(string connectionString) 
        {
            Context = new DataContext<SqlConnection>(connectionString);
        }

        public async Task<long> Create(MerchantDomainModel model)
        {
            int insertedMerchantId = 0;
            var parameters = new DynamicParameters();
            parameters.Add("@RefCode", model.RefCode, DbType.String, ParameterDirection.Input);
            parameters.Add("@Name", model.Name, DbType.String, ParameterDirection.Input);
            parameters.Add("@MerchantTypeId", model.MerchantTypeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@AddressLine1", model.AddressLine1, DbType.String, ParameterDirection.Input);
            parameters.Add("@AddressLine2", model.AddressLine2, DbType.String, ParameterDirection.Input);
            parameters.Add("@District", model.District, DbType.String, ParameterDirection.Input);
            parameters.Add("@City", model.City, DbType.String, ParameterDirection.Input);
            parameters.Add("@State", model.State, DbType.String, ParameterDirection.Input);
            parameters.Add("@Country", model.Country, DbType.String, ParameterDirection.Input);
            parameters.Add("@PinCode", model.PinCode, DbType.String, ParameterDirection.Input);
            parameters.Add("@Email", model.Email, DbType.String, ParameterDirection.Input);
            parameters.Add("@Telephone", model.Telephone, DbType.String, ParameterDirection.Input);
            parameters.Add("@Extension", model.Extension, DbType.String, ParameterDirection.Input);
            parameters.Add("@Fax", model.Fax, DbType.String, ParameterDirection.Input);
            parameters.Add("@MobileNumber", model.MobileNumber, DbType.String, ParameterDirection.Input);
            parameters.Add("@CreatedBy", model.CreatedBy, DbType.String, ParameterDirection.Input);
            parameters.Add("@IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);
            parameters.Add("@IsDeleted", model.IsDeleted, DbType.Boolean, ParameterDirection.Input);

            var queryMerchant = "INSERT INTO[dbo].[Merchants]" +
            "(RefCode, Name, MerchantTypeId, AddressLine1, AddressLine2, District, City, State, Country, PinCode, Email," +
            " Telephone, Extension, Fax, MobileNumber, CreatedBy, CreatedDateTime," +
            "IsActive, IsDeleted)" +
            "values " + 
            "(@RefCode, @Name, @MerchantTypeId, @AddressLine1, @AddressLine2, @District, @City, @State, @Country, @PinCode, @Email," +
            " @Telephone, @Extension, @Fax, @MobileNumber, @CreatedBy," +
            " GETDATE(), @IsActive, @IsDeleted)";

            await Context.ExecuteWriteSqlAsync(queryMerchant, parameters).ConfigureAwait(false);

            insertedMerchantId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT TOP 1 Id FROM Merchants WHERE MobileNumber = @MobileNumber AND RefCode =@RefCode AND IsActive = 1 " +
                                "AND IsDeleted = 0", parameters).ConfigureAwait(false);

            if (insertedMerchantId > 0)
            {
                parameters.Add("@MerchantId", insertedMerchantId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@DepositCashBalance", model.DepositCashBalance, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@WithdrawCashBalance", model.WithdrawCashBalance, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@IsOnline", model.IsOnline, DbType.String, ParameterDirection.Input);
                parameters.Add("@Latitude", model.Latitude, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@Longitude", model.Longitude, DbType.Decimal, ParameterDirection.Input);
                parameters.Add("@CreatedBy", model.CreatedBy, DbType.String, ParameterDirection.Input);
                parameters.Add("@IsActive", true, DbType.String, ParameterDirection.Input);
                parameters.Add("@IsDeleted", false, DbType.String, ParameterDirection.Input);

                var queryMerchantSetup = "INSERT INTO [dbo].[MerchantSetups]" +
                 "(MerchantId, DepositCashBalance, WithdrawCashBalance, IsOnline, Latitude, Longitude, CreatedBy, CreatedDateTime, IsActive, IsDeleted)" +
                 "values" +
                 "(@MerchantId, @DepositCashBalance, @WithdrawCashBalance, @IsOnline, @Latitude, @Longitude, @CreatedBy, GETDATE(), @IsActive, @IsDeleted)";

                await Context.ExecuteWriteSqlAsync(queryMerchantSetup, parameters).ConfigureAwait(false);
            }
            //insertedId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT MerchantId FROM MerchantSetups WHERE MerchantId = @MerchantId AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);

            return insertedMerchantId;
        }

        public async Task<int> Update(MerchantDomainModel model)
        {
            
            var parameters = new DynamicParameters();
            parameters.Add("@RefCode", model.RefCode, DbType.String, ParameterDirection.Input);
            int merchantId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT TOP 1 Id FROM Merchants WHERE RefCode = @RefCode AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);

            if (merchantId > 0)
            {
                parameters.Add("@MerchantId", merchantId, DbType.Int32, ParameterDirection.Input);
                parameters.Add("@Name", model.Name, DbType.String, ParameterDirection.Input);
                parameters.Add("@MerchantTypeId", model.MerchantTypeId, DbType.Int16, ParameterDirection.Input);
                parameters.Add("@AddressLine1", model.AddressLine1, DbType.String, ParameterDirection.Input);
                parameters.Add("@AddressLine2", model.AddressLine2, DbType.String, ParameterDirection.Input);
                parameters.Add("@District", model.District, DbType.String, ParameterDirection.Input);
                parameters.Add("@City", model.City, DbType.String, ParameterDirection.Input);
                parameters.Add("@State", model.State, DbType.String, ParameterDirection.Input);
                parameters.Add("@Country", model.Country, DbType.String, ParameterDirection.Input);
                parameters.Add("@PinCode", model.PinCode, DbType.String, ParameterDirection.Input);
                parameters.Add("@Email", model.Email, DbType.String, ParameterDirection.Input);
                parameters.Add("@Telephone", model.Telephone, DbType.String, ParameterDirection.Input);
                parameters.Add("@Extension", model.Extension, DbType.String, ParameterDirection.Input);
                parameters.Add("@Fax", model.Fax, DbType.String, ParameterDirection.Input);
                parameters.Add("@MobileNumber", model.MobileNumber, DbType.String, ParameterDirection.Input);
                parameters.Add("@ModifiedBy", model.ModifiedBy, DbType.String, ParameterDirection.Input);
                parameters.Add("@IsActive", model.IsActive, DbType.Boolean, ParameterDirection.Input);

                var querystring = "UPDATE [dbo].[Merchants] SET RefCode = @RefCode , Name = @Name , MerchantTypeId = @MerchantTypeId ,AddressLine1 = @AddressLine1 ," +
                " AddressLine2 = @AddressLine2 , District = @District,City = @City, [State] = @State ,Country = @Country , PinCode = @PinCode , Email = @Email ," +
                " Telephone = @Telephone , Extension = @Extension , Fax = @Fax ,MobileNumber = @MobileNumber ," +
                " ModifiedBy = @ModifiedBy , ModifiedDateTime = GETDATE() ,IsActive = @IsActive where ID = @MerchantId ";

                await Context.ExecuteWriteSqlAsync(querystring, parameters).ConfigureAwait(false);
            }
            //updatedId = await Context.ExecuteSingleRecordReadSqlAsync<int>("SELECT Id FROM Merchants WHERE ID = @MerchantId AND RefCode = @RefCode AND IsActive = 1 AND IsDeleted = 0", parameters).ConfigureAwait(false);
            return merchantId;
        }
        public async Task<bool> Delete(string refCode)
        {
            var parameters = new DynamicParameters();

            parameters.Add("@RefCode", refCode, DbType.String, ParameterDirection.Input);
            parameters.Add("@ResultStatus", true, DbType.Boolean, ParameterDirection.Output);

            var results = await Context.ExecuteWriteSqlAsync("UPDATE [dbo].[Merchants] SET IsActive = 0,IsDeleted = 1 WHERE RefCode = @RefCode", parameters).ConfigureAwait(false);

            return Convert.ToBoolean(results);
        }
    }
}


