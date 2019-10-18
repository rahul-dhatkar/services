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
//** Purpose   : Send email                                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** NAM Team      11-07-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
namespace FinoBank.Cola.Manager.Helpers
{
    /// <summary>
    /// Validation Message Helper
    /// </summary>
    internal static class ValidationMessageHelper
    {
        /// <summary>
        /// The no record found for criteria
        /// </summary>
        public const string NoRecordFoundForCriteria = "No records found.";

        /// <summary>
        /// The unable to create master
        /// </summary>
        public const string UnableToCreateMaster = "Name already exist.";

        /// <summary>
        /// The unable to create firm
        /// </summary>
        public const string UnableToCreateFirm = "Unable to create firm.";

        /// <summary>
        /// The unable to update firm
        /// </summary>
        public const string UnableToUpdateFirm = "Unable to update firm.";

        /// <summary>
        /// The unable to create person
        /// </summary>
        public const string UnableToCreatePerson = "Unable to create person.";

        /// <summary>
        /// The unable to update person
        /// </summary>
        public const string UnableToUpdatePerson = "Unable to update person.";
    }
}