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
//** Purpose   : Logger Type Enum                                                          **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Infrastructure.LoggerService.Dtos
{
    /// <summary>
    /// Logger Type Enum
    /// </summary>
    public enum LoggerTypeEnum
    {
        /// <summary>
        /// The text file
        /// </summary>
        TextFile = 0,
        /// <summary>
        /// The database
        /// </summary>
        Database = 1
    }
}
