// ***********************************************************************
// Assembly         : QSContent
// Author           : Jonathan Sheely
// Created          : 05-12-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 05-12-2013
// ***********************************************************************
// <copyright file="Main.ascx.cs" company="InspectorIT">
//     Copyright (c) InspectorIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Security;
using InspectorIT.QSContent.Components.Controller;
using InspectorIT.QSContent.Components.Entities;

namespace InspectorIT.QSContent
{
    /// <summary>
    /// Class Main
    /// </summary>
    public partial class Main : PortalModuleBase, IActionable
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsEditable)
            {
                List<ValidateCheckInfo> outputChecks = new List<ValidateCheckInfo>();
                List<CheckInfo> checks = DataController.GetChecks(ModuleId);
                foreach (CheckInfo checkInfo in checks)
                {
                    //Need to figure out how to do regEx with a UrlDecode. qsValuecheckInfo on first where clause.
                    //.Where(qsValuecheckInfo => Regex.IsMatch(keyValue,qsValuecheckInfo))
                    outputChecks.AddRange(Request.QueryString.Keys.Cast<string>()
                                                .Select(key => key + "=" + Request.QueryString[key]).SelectMany(keyValue => checkInfo.Querystring.Split(',')
                                                .Where(qsValuecheckInfo => Regex.IsMatch(keyValue, qsValuecheckInfo))
                                                .Where(qsValuecheckInfo => !outputChecks.Exists(x => x.ModuleID == checkInfo.CheckModuleID && x.Visible)), (keyValue, qsValuecheckInfo) => keyValue)
                                                .Select(keyValue => new ValidateCheckInfo() {ModuleID = checkInfo.CheckModuleID, Visible = !checkInfo.HideMatch}).ToList());
                    
                    //If no querystring value is found that matches; just hide the module.
                    if (!outputChecks.Exists(x => x.ModuleID == checkInfo.CheckModuleID))
                    {
                        outputChecks.Add(new ValidateCheckInfo() {ModuleID = checkInfo.CheckModuleID, Visible = checkInfo.HideMatch});
                    }
                }

                //Add the QS Module to hide it. It needs to be visible server side because otherwise it won't output it's payload.
                outputChecks.Add(new ValidateCheckInfo(){ModuleID = ModuleId, Visible = false});
                JsonOutput = JsonExtensionsWeb.ToJson(outputChecks);
                plConfig.Visible = false;
            }
        }

        /// <summary>
        /// Gets or sets the json output.
        /// </summary>
        /// <value>The json output.</value>
        protected string JsonOutput { get; set; }

        /// <summary>
        /// Gets the module actions.
        /// </summary>
        /// <value>The module actions.</value>
        ModuleActionCollection IActionable.ModuleActions
        {
            get
            {
                ModuleActionCollection actions = new ModuleActionCollection();
                actions.Add(GetNextActionID(), "Configuration", "", "", "Edit.gif",
                     EditUrl("Admin"), "", false, SecurityAccessLevel.Edit, true, true);

                return actions;
            }
        }
    }
}