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
//** Purpose   : ICommandLoggerRepository                                                    **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      08-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------


using Contesto.V2.Core.Infrastructure.LoggerService.Dtos;
using Contesto.V2.Core.Infrastructure.Data.Interfaces;
using System.Threading.Tasks;

namespace Contesto.V2.Core.Infrastructure.LoggerService.Interfaces
{
    /// <summary>
    /// Interface Command Logger Repository
    /// </summary>
    /// <seealso cref="ICommandGenericRepository{LoggerDomainModel, System.Int64}" />
    public interface ICommandLoggerRepository  
    {
        Task<long> Create(LoggerDomainModel model);
    }
}
