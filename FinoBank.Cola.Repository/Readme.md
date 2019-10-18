
# FrameworkOne.Core.Infrastructure.Data

This project was generated with [Visual Stdio](https://visualstudio.microsoft.com/vs/whatsnew/) version 2017.


## Release Notes
Sample implementation of Repository Layer
 

# Installation

From FULCRUM { Nuget Gallery } :

```csharp
Install-Package FrameworkOne.Core.Infrastructure.Data
```

## Example 

 `1. Domain Model same as table structure`

   ```csharp
 namespace FrameworkOne.Core.Infrastructure.Data.Base
{
    /// <summary>
    /// Base Domain Master Model
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="FrameworkOne.Core.Infrastructure.Data.Base.BaseDomainModel{T}" />
    public class BaseDomainMasterModel<T> : BaseDomainModel<T>
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }
    }
}
```
 `2. Interfaces`

> Command Repository - Below interface is inherited from  `ICommandGenericRepository` for Create, Update, and delete data

```csharp
using FrameworkOne.Core.Infrastructure.Data.Interfaces;
using FrameworkOne.ManageMater.Repository.DomainModels;

namespace FrameworkOne.ManageStartupKit.Repository.Interfaces
{
    /// <summary>
    /// Interface Command StartupKit Repository
    /// </summary>
    /// <seealso cref="ICommandGenericRepository{MasterDomainModel, System.Int32}" />
    public interface ICommandStartupKitRepository : ICommandGenericRepository<StartupKitDomainModel, int>
    {
    }
}
```

> Query Repository - Below interface is inherited from  `IQueryGenericRepository` form GridSummaryDataWithPaging, GetById, GetAllData, IsExist. 


```csharp
using FrameworkOne.Core.Infrastructure.Data.Interfaces;
using FrameworkOne.ManageMater.Repository.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FrameworkOne.ManageStartupKit.Repository.Interfaces
{
    /// <summary>
    /// Interface Query Master Repository
    /// </summary>
    /// <seealso cref="IQueryGenericRepository{StartupKitDomainModel}" />
    public interface IQueryStartupKitRepository : IQueryGenericRepository<StartupKitDomainModel>
    {
        /// <summary>
        /// Gets the master grid summary data with paging.
        /// </summary>
        /// <param name="typeId">The type identifier.</param>
        /// <param name="SortColumn">The sort column.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchTxt">The search text.</param>
        /// <returns></returns>
        Task<Tuple<List<StartupKitDomainModel>, int>> GetGridSummaryDataWithPaging(int typeId, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt);
       
        /// <summary>
        /// Determines whether the specified model is exist.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> IsExist(StartupKitDomainModel model);
    }
}
```
`Note` :- If 'GenericRepository' is not full feeling your requirement then you can define your own custom method(s). Like in above `IQueryStartupKitRepository` interface the GetGridSummaryDataWithPaging and IsExist are custom methods.

> UnitOfWork - All application repositories are available through below interface

```csharp
using FrameworkOne.ManageStartupKit.Repository.Interfaces;

namespace FrameworkOne.ManageStartupKit.Repository.Uom.Interfaces
{
    /// <summary>
    /// Interface Master Unit Of Work
    /// </summary>
    public interface IStartupKitUnitOfWork
    {

        /// <summary>
        /// Gets or sets the command startup kit repository.
        /// </summary>
        /// <value>
        /// The command startup kit repository.
        /// </value>
        ICommandStartupKitRepository CommandStartupKitRepository { get; set; }

        /// <summary>
        /// Gets or sets the query startup kit repository.
        /// </summary>
        /// <value>
        /// The query startup kit repository.
        /// </value>
        IQueryStartupKitRepository QueryStartupKitRepository { get; set; }
      
    }
}
```

 `3. Implementation Classes`

> Command Repository - Below class is inherited from  `CommandGenericRepository` and implemented from `ICommandStartupKitRepository` for Create, Update, and delete data
 
```csharp
using FrameworkOne.Core.Infrastructure.Data;
using FrameworkOne.ManageStartupKit.Repository.Interfaces;
using FrameworkOne.ManageMater.Repository.DomainModels;

namespace FrameworkOne.ManageStartupKit.Repository.Commands
{
    /// <summary>
    /// Command Master Repository
    /// </summary>
    /// <seealso cref="CommandGenericRepository{MasterDomainModel, System.Int32}" />
    /// <seealso cref="ICommandStartupKitRepository" />
    internal class CommandStartupKitRepository : CommandGenericRepository<StartupKitDomainModel, int>, ICommandStartupKitRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommandStartupKitRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal CommandStartupKitRepository(string connectionString) : base(connectionString)
        {
        }

    }
}
```
> Query Repository - Below interface is inherited from  `QueryGenericRepository` and implemented from `IQueryStartupKitRepository` form GridSummaryDataWithPaging, GetById, GetAllData, IsExist. 

```csharp
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using FrameworkOne.Core.Infrastructure.Data;
using FrameworkOne.ManageStartupKit.Repository.Interfaces;
using FrameworkOne.ManageMater.Repository.DomainModels;
using Dapper;

namespace FrameworkOne.ManageStartupKit.Repository.Queries
{
    /// <summary>
    /// Query Master Repository
    /// </summary>
    /// <seealso cref="QueryGenericRepository{StartupKitDomainModel}" />
    /// <seealso cref="IQueryStartupKitRepository" />
    internal class QueryStartupKitRepository : QueryGenericRepository<StartupKitDomainModel>, IQueryStartupKitRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="QueryStartupKitRepository"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        internal QueryStartupKitRepository(string connectionString) : base(connectionString)
        {
        }

        /// <summary>
        /// Gets the master grid summary data with paging.
        /// </summary>
        /// <param name="typeId">The type identifier.</param>
        /// <param name="SortColumn">The sort column.</param>
        /// <param name="SortDirection">The sort direction.</param>
        /// <param name="pageIndex">Index of the page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <param name="searchTxt">The search text.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Determines whether the specified model is exist.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
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
using FrameworkOne.ManageStartupKit.Repository.Commands;
using FrameworkOne.ManageStartupKit.Repository.Interfaces;
using FrameworkOne.ManageStartupKit.Repository.Queries;
using FrameworkOne.ManageStartupKit.Repository.Uom.Interfaces;

namespace FrameworkOne.ManageStartupKit.Repository.Uom
{
    /// <summary>
    /// Master Unit Of Work
    /// </summary>
    /// <seealso cref="IStartupKitUnitOfWork" />
    public class StartupKitUnitOfWork : IStartupKitUnitOfWork
    {
        /// <summary>
        /// The connection string
        /// </summary>
        private readonly string _connectionString;

        /// <summary>
        /// The command startup kit repository
        /// </summary>
        private ICommandStartupKitRepository _commandStartupKitRepository;

        /// <summary>
        /// The query startup kit repository
        /// </summary>
        private IQueryStartupKitRepository _queryStartupKitRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="StartupKitUnitOfWork"/> class.
        /// </summary>
        /// <param name="connectionString">The connection string.</param>
        public StartupKitUnitOfWork(string connectionString)
        {
            _connectionString = connectionString;
        }


        /// <summary>
        /// Gets or sets the command startup kit repository.
        /// </summary>
        /// <value>
        /// The command startup kit repository.
        /// </value>
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


        /// <summary>
        /// Gets or sets the query startup kit repository.
        /// </summary>
        /// <value>
        /// The query startup kit repository.
        /// </value>
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