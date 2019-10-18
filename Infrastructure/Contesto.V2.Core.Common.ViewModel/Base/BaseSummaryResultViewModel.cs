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
//** Created   : 07-Jun-18                                                                 **
//** Purpose   : Base Summary Result ViewModel                                             **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** NAM Team      07-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Common.ViewModel.Base
{
    /// <summary>
    /// Base Summary Result ViewModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <seealso cref="Contesto.V2.Core.Common.ViewModel.Base.BaseGridPagingViewModel" />
    public class BaseSummaryResultViewModel<T> : BaseGridPagingViewModel
    {
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        /// <value>
        /// The result.
        /// </value>
        public T Result { get; set; }
    }
}