using Contesto.V2.Core.Infrastructure.Data;
using Dapper;
using FinoBank.Cola.Repository.DomainModels;
using FinoBank.Cola.Repository.Interfaces;
using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Queries
{
    internal class QueryCustomerSummaryRepository : QueryGenericSqlRepository<CustomerDomainModel>, IQueryCustomerSummaryRepository
    {
        internal QueryCustomerSummaryRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Tuple<CustomerDomainModel>> GetCustomerDetailsByMobileNumber(string mobileNumber)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Mobile", mobileNumber, DbType.String, ParameterDirection.Input);

            var results = await Context.ExecuteReadSqlAsync<CustomerDomainModel>("SELECT Id,RefCode,FirstName,LastName,Type,Mobile,IsVerified,CreatedBy,CreatedDateTime,ModifiedBy,ModifiedDateTime,IsActive,IsDeleted FROM Customers WHERE Mobile=@Mobile", parameters).ConfigureAwait(false);

            return new Tuple<CustomerDomainModel>(results.FirstOrDefault());
        }
    }
}



