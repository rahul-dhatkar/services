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
//** Created   : 14-Jun-18                                                                 **
//** Purpose   : FaceBook OAuth View Model                                                 **
//**                                                                                       **
//**                                                                                       **
//**                                                                                       **
//** Change Log:                                                                           **
//** ==================================                                                    **
//** Name          Date         Purpose                                                    **
//** Dhiraj G      14-06-18     Created                                                    **
//**                                                                                       **
//-------------------------------------------------------------------------------------------

namespace Contesto.V2.Core.Common.ViewModel.ViewModels
{
    /// <summary>
    /// FaceBook OAuth View Model
    /// </summary>
    public class FaceBookOAuthRequestViewModel
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the picture.
        /// </summary>
        /// <value>
        /// The picture.
        /// </value>
        public Picture Picture { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        /// <value>
        /// The first name.
        /// </value>
        public string First_Name { get; set; }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        /// <value>
        /// The last name.
        /// </value>
        public string Last_Name { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        /// <value>
        /// The gender.
        /// </value>
        public string Gender { get; set; }
    }

    /// <summary>
    /// Picture
    /// </summary>
    public class Picture
    {
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>
        /// The data.
        /// </value>
        public Data Data { get; set; }
    }

    /// <summary>
    /// Data
    /// </summary>
    public class Data
    {
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        public string Url { get; set; }
    }
}