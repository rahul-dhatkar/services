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
//** Created   : 27-Jun-18                                                                 **
//** Purpose   : Configuration Config                                                      **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      27-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Infrastructure.ConfigurationService.Dtos
{
    /// <summary>
    /// Configuration Config
    /// </summary>
    public class ConfigurationConfig
    {
        /// <summary>
        /// Gets or sets the database connection string.
        /// </summary>
        /// <value>
        /// The database connection string.
        /// </value>
        public string DbConnectionString { get; set; }

        /// <summary>
        /// Gets or sets the run time environment.
        /// </summary>
        /// <value>
        /// The run time environment.
        /// </value>
        public string RunTimeEnvironment { get; set; }

        /// <summary>
        /// Gets or sets the json file path.
        /// </summary>
        /// <value>
        /// The json file path.
        /// </value>
        public string JsonFilePath { get; set; }
    }
}