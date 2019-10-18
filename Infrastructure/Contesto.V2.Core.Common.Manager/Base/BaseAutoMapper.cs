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
//** Purpose   : Base Auto Mapper                                                          **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      12-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using AutoMapper;

namespace Contesto.V2.Core.Common.Manager.Base
{
    /// <summary>
    /// Base Auto Mapper
    /// </summary>
    /// <seealso cref="AutoMapper.Profile" />
    public abstract class BaseAutoMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAutoMapper"/> class.
        /// </summary>
        protected BaseAutoMapper() : base() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseAutoMapper"/> class.
        /// </summary>
        /// <param name="profileName">Name of the profile.</param>
        protected BaseAutoMapper(string profileName) : base(profileName) { }
    }
}