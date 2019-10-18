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
//** Created   : 07-13-2018                                                                **
//** Purpose   : Utility Helper                                                            **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj  G     07-13-2018   Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.IO;

namespace Contesto.V2.Core.Common.Utility
{
    /// <summary>
    /// Utility Helper
    /// </summary>
    public static class UtilityHelper
    {
        /// <summary>
        /// Gets the type of the content.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        /// <summary>
        /// Gets the MIME types.
        /// </summary>
        /// <returns></returns>
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase)
            {
                {".tif", "image/tiff"},
                {".tiff", "image/tiff"},
                { ".jpeg", "image/jpeg"},
                { ".jpg", "image/jpeg"},
                { ".bmp", "image/bmp"},
                { ".doc", "application/msword"},
                { ".docm", "application/vnd.ms-word.document.macroEnabled.12"},
                { ".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                { ".dot", "application/msword"},
                { ".dotm", "application/vnd.ms-word.template.macroEnabled.12"},
                { ".dotx", "application/vnd.openxmlformats-officedocument.wordprocessingml.template"},
                { ".xps", "application/vnd.ms-xpsdocument"},
                { ".pdf", "application/pdf"},
            };
        }
    }
}
