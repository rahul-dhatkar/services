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
//** Purpose   : Base Manager Autofac Container                                            **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      07-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using System.Reflection;
using Autofac;

namespace Contesto.V2.Core.Common.Manager.Ioc
{
    /// <summary>
    /// Base Manager Autofac Container
    /// </summary>
    /// <seealso cref="Autofac.Module" />
    public abstract class BaseManagerAutofacContainer : Module
    {
    }
}