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
//** Created   : 12-Jun-18                                                                 **
//** Purpose   : BaseCommandSuccessResultViewModel                                         **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      12-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Common.ViewModel.Base
{
    /// <summary>
    /// Base Success Result ViewModel
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseCommandSuccessResultViewModel<T>
    {
        /// <summary>
        /// Gets or sets the response value.
        /// </summary>
        /// <value>
        /// The response value.
        /// </value>
        public T ResponseValue { get; set; }
    }

    /// <summary>
    /// CommandSuccess with return type integer 
    /// </summary>
    public class CommandSuccessIntegerResultViewModel : BaseCommandSuccessResultViewModel<int>
    {

    }

    /// <summary>
    /// CommandSuccess with return type integer long
    /// </summary>
    public class CommandSuccessLongResultViewModel : BaseCommandSuccessResultViewModel<long>
    {

    }

    /// <summary>
    /// CommandSuccess with return type string
    /// </summary>
    public class CommandSuccessStringResultViewModel : BaseCommandSuccessResultViewModel<string>
    {

    }

    /// <summary>
    /// CommandSuccess with return type boolean 
    /// </summary>
    public class CommandSuccessBoolResultViewModel : BaseCommandSuccessResultViewModel<bool>
    {

    }

    /// <summary>
    /// CommandSuccess with return type short 
    /// </summary>
    public class CommandSuccessShortResultViewModel : BaseCommandSuccessResultViewModel<short>
    {

    }
}