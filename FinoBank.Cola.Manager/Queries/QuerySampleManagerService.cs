using AutoMapper;
using Contesto.V2.Core.Common.Manager.Base;
using Contesto.V2.Core.Common.Manager.Helpers;
using Contesto.V2.Core.Common.Manager.Results;
using FinoBank.Cola.Manager.Interfaces;
using FinoBank.Cola.Manager.ViewModels;
using FinoBank.Cola.Repository.Uom.Interfaces;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Manager.Queries
{
    /// <summary>
    /// Query Startup Kit ManagerService
    /// </summary>
    /// <seealso cref="Contesto.V2.Core.Common.Manager.Base.BaseManager" />
    /// <seealso cref="FinoBank.Cola.Manager.Interfaces.IQuerySampleManagerService" />
    /// <seealso cref="BaseManager" />
    /// <seealso cref="IQuerySampleManagerService" />
    public class QuerySampleManagerService : BaseManager, IQuerySampleManagerService
    {
        #region "Variables"

        /// <summary>
        /// The startup kit unit of work
        /// </summary>
        private readonly IUnitOfWork _startupKitUnitOfWork;

        #endregion "Variables"

        #region "Constructor"

        /// <summary>
        /// Initializes a new instance of the <see cref="QuerySampleManagerService" /> class.
        /// </summary>
        /// <param name="mapper">The mapper.</param>
        /// <param name="cache">The cache.</param>
        /// <param name="startupKitUnitOfWork">The startup kit unit of work.</param>
        public QuerySampleManagerService(IMapper mapper, IMemoryCache cache, IUnitOfWork startupKitUnitOfWork) : base(mapper, cache, null)
        {
            _startupKitUnitOfWork = startupKitUnitOfWork;
        }

        #endregion "Constructor"

        #region "Masters"

        /// <summary>
        /// Gets the master by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public async Task<OperationResult<SampleViewModel>> GetStartupKitById(int id)
        {
            var dbResults = await _startupKitUnitOfWork.QuerySampleRepository.GetById(id).ConfigureAwait(false);
            return ResponseBuilderHelper<SampleViewModel>.Instance.BuildSucessResult(MappService.Map<SampleViewModel>(dbResults));
        }

        /// <summary>
        /// Gets the startup kits.
        /// </summary>
        /// <param name="searchText">The search text.</param>
        /// <returns></returns>
        public async Task<OperationResult<List<SampleViewModel>>> GetStartupKits(string searchText = null)
        {
            var dbResults = await _startupKitUnitOfWork.QuerySampleRepository.GetAllData(searchText).ConfigureAwait(false);
            return ResponseBuilderHelper<List<SampleViewModel>>.Instance.BuildSucessResult(MappService.Map<List<SampleViewModel>>(dbResults));
        }

        /// <summary>
        /// Gets the startup kit summary.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<SampleSummaryResultViewModel>> GetStartupKitSummary(SampleSummaryRequestViewModel model)
        {
            var masterSummaryData = await _startupKitUnitOfWork.QuerySampleRepository.GetGridSummaryDataWithPaging<SampleViewModel>(model.SortColumn, model.SortDirection, model.PageIndex, model.PageSize, model.SearchText).ConfigureAwait(false);
            var results = MappService.Map<List<SampleViewModel>>(masterSummaryData.Item1);
            return ResponseBuilderHelper<SampleSummaryResultViewModel>.Instance
                .BuildSucessResult(new SampleSummaryResultViewModel
                {
                    Result = results,
                    TotalCount = masterSummaryData.Item2,
                    SortColumn = model.SortColumn,
                    SortDirection = model.SortDirection,
                    PageIndex = model.PageIndex,
                    PageSize = model.PageSize,
                    SearchText = model.SearchText
                });
        }

        /// <summary>
        /// Gets the startup kit summary by type identifier.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        public async Task<OperationResult<SampleSummaryResultViewModel>> GetStartupKitSummaryByTypeId(SampleSummaryRequestViewModel model)
        {
            var masterSummaryData = await _startupKitUnitOfWork.QuerySampleRepository.GetGridSummaryDataWithPaging(model.TypeId, model.SortColumn, model.SortDirection, model.PageIndex, model.PageSize, model.SearchText).ConfigureAwait(false);
            var results = MappService.Map<List<SampleViewModel>>(masterSummaryData.Item1);
            return ResponseBuilderHelper<SampleSummaryResultViewModel>.Instance
                .BuildSucessResult(new SampleSummaryResultViewModel
                {
                    Result = results,
                    TotalCount = masterSummaryData.Item2,
                    SortColumn = model.SortColumn,
                    SortDirection = model.SortDirection,
                    PageIndex = model.PageIndex,
                    PageSize = model.PageSize,
                    SearchText = model.SearchText
                });
        }

        #endregion "Masters"
    }
}