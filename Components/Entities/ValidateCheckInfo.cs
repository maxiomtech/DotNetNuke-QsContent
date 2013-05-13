// ***********************************************************************
// Assembly         : QSContent
// Author           : Jonathan Sheely
// Created          : 05-12-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 05-12-2013
// ***********************************************************************
// <copyright file="ValidateCheckInfo.cs" company="InspectorIT">
//     Copyright (c) InspectorIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
namespace InspectorIT.QSContent.Components.Entities
{
    /// <summary>
    /// Class ValidateCheckInfo
    /// </summary>
    public class ValidateCheckInfo
    {
        /// <summary>
        /// Gets or sets the module ID.
        /// </summary>
        /// <value>The module ID.</value>
        public int ModuleID { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ValidateCheckInfo"/> is visible.
        /// </summary>
        /// <value><c>true</c> if visible; otherwise, <c>false</c>.</value>
        public bool Visible { get; set; }
    }
}