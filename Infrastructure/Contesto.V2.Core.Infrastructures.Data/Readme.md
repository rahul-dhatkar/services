
# Contesto.V2.Core.Infrastructure.Data

This project was generated with [Visual Stdio](https://visualstudio.microsoft.com/vs/whatsnew/) version 2017.


## Release Notes
List of interfaces
- IDataContext
- ICommandGenericRepository
- IQueryGenericRepository

List of classes
- CommandGenericRepository
- QueryGenericRepository

# Installation

From FULCRUM { Nuget Gallery } :

```csharp
Install-Package Contesto.V2.Core.Infrastructure.Data
```
# Features

`Contesto.V2.Core.Infrastructure.Data` is a NuGet library that you can add in to your ASP.NET Web API, Console Application, and Windows Service. 

> Work with any database provider since there is no DB specific implementation.

> Database operation are separated in two parts
- Command - Insert, Update, and Delete database table using JSON data.
- Query - Retrieve data from the a database. 

> No need to write code for database calls

## Example 

### 1. Add below line of configuration settings in 

### .NET Core 2.0 and above

`appsettings.json `

```csharp
    "ConnectionStrings": {
    "DefaultConnection": "Data Source=XXX.XXX.XXX.XXX;Initial Catalog=XXXX;User ID=sa;Password=XXX@2018; Trusted_Connection = False; MultipleActiveResultSets = true"
  }

### .NET Framework 4.7 and above

`web.Config`

```csharp
  <connectionStrings>
    <add name="DatabaseConnectionString" connectionString="Data Source=XXX.XXX.XXX.XXX;Initial Catalog=XXXXX;User ID=sa;Password=XXX@2018; Trusted_Connection = False; MultipleActiveResultSets = true" />
  </connectionStrings>
  ```
 
 ### 2. Implementation

 `1. Domain Model same as table structure`

   ```csharp
 namespace Contesto.V2.Core.Infrastructure.Data.Base
{
    public class BaseDomainMasterModel<T> : BaseDomainModel<T>
    {
        public string Name { get; set; }
    }
}
```
 `2. Interfaces`

> Command Repository - Below interface is inherited from  `ICommandGenericRepository` for Create, Update, and delete data

```csharp
namespace Contesto.V2.ManageStartupKit.Repository.Interfaces
{
    public interface ICommandStartupKitRepository : ICommandGenericRepository<StartupKitDomainModel, int>
    {
    }
}
```
> Query Repository - Below interface is inherited from  `IQueryGenericRepository` form GridSummaryDataWithPaging, GetById, GetAllData, IsExist. 

```csharp

namespace Contesto.V2.ManageStartupKit.Repository.Interfaces
{
    public interface IQueryStartupKitRepository : IQueryGenericRepository<StartupKitDomainModel>
    {
        Task<Tuple<List<StartupKitDomainModel>, int>> GetGridSummaryDataWithPaging(int typeId, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt);

        Task<bool> IsExist(StartupKitDomainModel model);
    }
}
```
`Note` :- If 'GenericRepository' is not full feeling your requirement then you can define your own custom method(s). Like in above `IQueryStartupKitRepository` interface the GetGridSummaryDataWithPaging and IsExist are custom methods.

> UnitOfWork - All application repositories are available through below interface

```csharp

namespace Contesto.V2.ManageStartupKit.Repository.Uom.Interfaces
{
    public interface IStartupKitUnitOfWork
    {
        ICommandStartupKitRepository CommandStartupKitRepository { get; set; }

        IQueryStartupKitRepository QueryStartupKitRepository { get; set; }
    }
}
```

 `3. Implementation Classes`

> Command Repository - Below class is inherited from  `CommandGenericRepository` and implemented from `ICommandStartupKitRepository` for Create, Update, and delete data
 
```csharp
namespace Contesto.V2.ManageStartupKit.Repository.Commands
{
    internal class CommandStartupKitRepository : CommandGenericRepository<StartupKitDomainModel, int>, ICommandStartupKitRepository
    {
        internal CommandStartupKitRepository(string connectionString) : base(connectionString)
        {
        }
    }
}
```
> Query Repository - Below interface is inherited from  `QueryGenericRepository` and implemented from `IQueryStartupKitRepository` form GridSummaryDataWithPaging, GetById, GetAllData, IsExist. 

```csharp
namespace Contesto.V2.ManageStartupKit.Repository.Queries
{
    internal class QueryStartupKitRepository : QueryGenericRepository<StartupKitDomainModel>, IQueryStartupKitRepository
    {
        internal QueryStartupKitRepository(string connectionString) : base(connectionString)
        {
        }

        public async Task<Tuple<List<StartupKitDomainModel>, int>> GetGridSummaryDataWithPaging(int typeId, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TypeId", typeId, DbType.Int16, ParameterDirection.Input);
            parameters.Add("@SearchText", searchTxt, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortColumn", SortColumn, DbType.String, ParameterDirection.Input);
            parameters.Add("@SortDirection", SortDirection, DbType.String, ParameterDirection.Input);
            parameters.Add("@PageIndex", pageIndex, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@PageSize", pageSize, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@TotalRecords", 0, DbType.Int32, ParameterDirection.Output);
            var results = await Context.ExecuteReadProcedureAsync<StartupKitDomainModel>("GetGridSummaryDataWithPaging", parameters).ConfigureAwait(false);

            var totalRecords = parameters.Get<Int32>("@TotalRecords");
            return new Tuple<List<StartupKitDomainModel>, int>(results.ToList(), totalRecords);
        }

        public async Task<bool> IsExist(StartupKitDomainModel model)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", model.Id, DbType.Int32, ParameterDirection.Input);
            parameters.Add("@Value", model.Name, DbType.String, ParameterDirection.Input);
            var results = await Context.ExecuteReadProcedureAsync<bool>("IsMasterExist", parameters).ConfigureAwait(false);
            return results.FirstOrDefault();
        }
    }
}
```

> UnitOfWork - Below class implemented form `IStartupKitUnitOfWork`.   

```csharp
using Contesto.V2.ManageStartupKit.Repository.Commands;
using Contesto.V2.ManageStartupKit.Repository.Interfaces;
using Contesto.V2.ManageStartupKit.Repository.Queries;
using Contesto.V2.ManageStartupKit.Repository.Uom.Interfaces;

namespace Contesto.V2.ManageStartupKit.Repository.Uom
{
    public class StartupKitUnitOfWork : IStartupKitUnitOfWork
    {
        private readonly string _connectionString;

        private ICommandStartupKitRepository _commandStartupKitRepository;

        private IQueryStartupKitRepository _queryStartupKitRepository;

        public StartupKitUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }


        public ICommandStartupKitRepository CommandStartupKitRepository
        {
            get
            {
                return _commandStartupKitRepository ?? new CommandStartupKitRepository(_connectionString);
            }
            set
            {
                _commandStartupKitRepository = value;
            }
        }

        public IQueryStartupKitRepository QueryStartupKitRepository
        {
            get
            {
                return _queryStartupKitRepository ?? new QueryStartupKitRepository(_connectionString);
            }
            set
            {
                _queryStartupKitRepository = value;
            }
        }
    }
}
```

Now your repository is ready for use.