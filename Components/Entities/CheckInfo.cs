// ***********************************************************************
// Assembly         : QSContent
// Author           : Jonathan Sheely
// Created          : 05-12-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 05-12-2013
// ***********************************************************************
// <copyright file="CheckInfo.cs" company="InspectorIT">
//     Copyright (c) InspectorIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;

namespace InspectorIT.QSContent.Components.Entities
{
    /// <summary>
    /// Class CheckInfo
    /// </summary>
    public class CheckInfo
    {
        /// <summary>
        /// Gets or sets the ID.
        /// </summary>
        /// <value>The ID.</value>
        public int ID { get; set; }
        /// <summary>
        /// Gets or sets the querystring.
        /// </summary>
        /// <value>The querystring.</value>
        public string Querystring { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [hide match].
        /// </summary>
        /// <value><c>true</c> if [hide match]; otherwise, <c>false</c>.</value>
        public bool HideMatch { get; set; }
        /// <summary>
        /// Gets or sets the module ID.
        /// </summary>
        /// <value>The module ID.</value>
        public int ModuleID { get; set; }
        /// <summary>
        /// Gets or sets the check module ID.
        /// </summary>
        /// <value>The check module ID.</value>
        public int CheckModuleID { get; set; }
        /// <summary>
        /// Gets or sets the created on date.
        /// </summary>
        /// <value>The created on date.</value>
        public DateTime CreatedOnDate { get; set; }
        /// <summary>
        /// Gets or sets the created by user ID.
        /// </summary>
        /// <value>The created by user ID.</value>
        public int CreatedByUserID { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [server side check].
        /// </summary>
        /// <value><c>true</c> if [server side check]; otherwise, <c>false</c>.</value>
        public bool ServerSide { get; set; }
    }
}