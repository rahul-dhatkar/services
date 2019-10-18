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
//** Purpose   : Otp Helper                                                                **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      12-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

using OtpNet;
using System;

namespace Web.Api.Join2Waitlist.Controllers.Helpers
{
    /// <summary>
    /// OtpHelper
    /// </summary>
    public static class OtpHelper
    {

        /// <summary>
        /// The totp
        /// </summary>
        private static Totp _totp;

        /// <summary>
        /// Initializes the <see cref="OtpHelper"/> class.
        /// </summary>
        static OtpHelper()
        {
            var key = KeyGeneration.GenerateRandomKey(20);
            var base32String = Base32Encoding.ToString(key);
            var base32Bytes = Base32Encoding.ToBytes(base32String);
            _totp = new Totp(base32Bytes, mode: OtpHashMode.Sha512);
        }

        /// <summary>
        /// Generates this instance.
        /// </summary>
        /// <returns></returns>
        public static string Generate()
        {
            return _totp.ComputeTotp(DateTime.UtcNow);
        }

        /// <summary>
        /// Verifies the specified opt value.
        /// </summary>
        /// <param name="optValue">The opt value.</param>
        /// <returns></returns>
        public static bool Verify(string optValue)
        {
            long time;
            return _totp.VerifyTotp(optValue, out time);
        }
    }
}