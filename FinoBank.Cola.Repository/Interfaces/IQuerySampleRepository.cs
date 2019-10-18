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
//** Created   : 06-20-18                                                                  **
//** Purpose   : IQueryStartupKitRepository                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      06-20-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using FinoBank.Cola.Repository.DomainModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinoBank.Cola.Repository.Interfaces
{
    /// <summary>
    /// Interface Query Master Repository
    /// </summary>
    /// <seealso cref="IQueryGenericRepository{StartupKitDomainModel}" />
    public interface IQuerySampleRepository : IQueryGenericRepository<SampleDomainModel>
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
        Task<Tuple<List<SampleDomainModel>, int>> GetGridSummaryDataWithPaging(int typeId, string SortColumn, string SortDirection, int? pageIndex, int? pageSize, string searchTxt);

        /// <summary>
        /// Determines whether the specified model is exist.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        Task<bool> IsExist(SampleDomainModel model);
    }
}