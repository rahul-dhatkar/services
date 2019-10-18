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
//** Created   : 04-Jun-18                                                                 **
//** Purpose   : Stored Procedure Name Helper                                              **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      04-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Infrastructure.Data.Helpers
{
    /// <summary>
    /// Stored Procedure Name Helper
    /// </summary>
    internal class StoredProcedureNameHelper
    {
        /// <summary>
        /// The get by identifier sp prefix
        /// </summary>
        private static readonly string _getByIdSPPrefix = "Get";

        /// <summary>
        /// The get by identifier sp postfix
        /// </summary>
        private static readonly string _getByIdSPPostfix = "ById";

        /// <summary>
        /// The create sp prefix
        /// </summary>
        private static readonly string _createSPPrefix = "Create";

        /// <summary>
        /// The update sp prefix
        /// </summary>
        private static readonly string _updateSPPrefix = "Update";

        /// <summary>
        /// The delete sp prefix
        /// </summary>
        private static readonly string _deleteSPPrefix = "Delete";

        /// <summary>
        /// The is exist sp prefix
        /// </summary>
        private static readonly string _isExistSPPrefix = "Is";

        /// <summary>
        /// The is exist sp postfix
        /// </summary>
        private static readonly string _isExistSPPostfix = "Exist";

        /// <summary>
        /// The get all sp prefix
        /// </summary>
        private static readonly string _getAllSPPrefix = "GetAll";

        /// <summary>
        /// The get all with paging sp prefix
        /// </summary>
        private static readonly string _getAllWithPagingSPPrefix = "Get";

        /// <summary>
        /// The get all with paging sp postfix
        /// </summary>
        private static readonly string _getAllWithPagingSPPostfix = "WithPaging";

        /// <summary>
        /// The entity postfix
        /// </summary>
        private static readonly string _entityPostfix = "DomainModel";

        /// <summary>
        /// Gets the name of the by identifier sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetByIdSPName<T>()
        {
            return string.Concat(_getByIdSPPrefix, typeof(T).Name.Replace(_entityPostfix, string.Empty), _getByIdSPPostfix);
        }

        /// <summary>
        /// Creates the name of the sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string CreateSPName<T>()
        {
            return string.Concat(_createSPPrefix, typeof(T).Name.Replace(_entityPostfix, string.Empty));
        }

        /// <summary>
        /// Updates the name of the sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string UpdateSPName<T>()
        {
            return string.Concat(_updateSPPrefix, typeof(T).Name.Replace(_entityPostfix, string.Empty));
        }

        /// <summary>
        /// Deletes the name of the sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string DeleteSPName<T>()
        {
            return string.Concat(_deleteSPPrefix, typeof(T).Name.Replace(_entityPostfix, string.Empty));
        }

        /// <summary>
        /// Determines whether [is exist sp name].
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string IsExistSPName<T>()
        {
            return string.Concat(_isExistSPPrefix, typeof(T).Name.Replace(_entityPostfix, string.Empty), _isExistSPPostfix);
        }

        /// <summary>
        /// Gets the name of all sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetAllSPName<T>()
        {
            if (typeof(T).Name.Contains(_entityPostfix))
                return string.Concat(_getAllSPPrefix, typeof(T).Name.Replace(_entityPostfix, "s"));
            else
                return string.Concat(_getAllSPPrefix, typeof(T).Name + "s");
        }

        /// <summary>
        /// Gets the name of all with paging summary sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetAllWithPagingSummarySPName<T>()
        {
            return string.Concat(_getAllWithPagingSPPrefix, typeof(T).Name.Replace(_entityPostfix, string.Empty), _getAllWithPagingSPPostfix);
        }

        /// <summary>
        /// Gets the name of all with paging sp.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetAllWithPagingSPName<T>()
        {
            if (typeof(T).Name.Contains(_entityPostfix))
                return string.Concat(_getAllWithPagingSPPrefix, typeof(T).Name.Replace(_entityPostfix, "s"), _getAllWithPagingSPPostfix);
            else
                return string.Concat(_getAllWithPagingSPPrefix, typeof(T).Name, "s", _getAllWithPagingSPPostfix);
        }
    }
}