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
using System.Web.UI;
using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Modules.Actions;
using DotNetNuke.Framework;
using DotNetNuke.Security;
using DotNetNuke.UI;
using DotNetNuke.UI.Skins;
using DotNetNuke.UI.Skins.Controls;
using InspectorIT.QSContent.Components.Controllers;
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
            List<ValidateCheckInfo> clientSideChecks = new List<ValidateCheckInfo>();
            List<ValidateCheckInfo> serverSideChecks = new List<ValidateCheckInfo>();
            List<CheckInfo> checks = DataController.GetChecks(ModuleId);
            foreach (CheckInfo checkInfo in checks)
            {
                foreach (string qsKey in Request.QueryString.Keys.Cast<string>())
                {
                    var keyValue = qsKey + "=" + Request.QueryString[qsKey];
                    foreach (string checkValue in checkInfo.Querystring.Split(','))
                    {
                        bool isVisible = keyValue == checkValue || Regex.IsMatch(keyValue, checkValue);
                        if (isVisible)
                        {
                            if (checkInfo.ServerSide)
                            {
                                if (!serverSideChecks.Exists(x => x.ModuleID == checkInfo.CheckModuleID && x.Visible))
                                {

                                    serverSideChecks.Add(new ValidateCheckInfo() {ModuleID = checkInfo.CheckModuleID, Visible = !checkInfo.HideMatch});
                                }
                            }
                            else
                            {
                                if (!clientSideChecks.Exists(x => x.ModuleID == checkInfo.CheckModuleID && x.Visible))
                                {
                                    clientSideChecks.Add(new ValidateCheckInfo() {ModuleID = checkInfo.CheckModuleID, Visible = !checkInfo.HideMatch});
                                }
                            }
                        }


                    }
                }

                //Additional validation on the checkInfo in case there was not a querystring to test against.
                if (checkInfo.ServerSide)
                {
                    if (!serverSideChecks.Exists(x => x.ModuleID == checkInfo.CheckModuleID && x.Visible))
                    {

                        serverSideChecks.Add(new ValidateCheckInfo() {ModuleID = checkInfo.CheckModuleID, Visible = checkInfo.HideMatch});
                    }
                }
                else
                {
                    if (!clientSideChecks.Exists(x => x.ModuleID == checkInfo.CheckModuleID && x.Visible))
                    {
                        clientSideChecks.Add(new ValidateCheckInfo() {ModuleID = checkInfo.CheckModuleID, Visible = checkInfo.HideMatch});
                    }
                }

            }

            if (!IsEditable)
            {
                //Hide Modules only if not an admin.
                foreach (ValidateCheckInfo validateCheckInfo in serverSideChecks)
                {
                    if (!validateCheckInfo.Visible)
                    {
                        var control = ControlUtilities.FindFirstDescendent<Control>(Page, x => x.ID == "ctr" + validateCheckInfo.ModuleID.ToString());
                        if (control != null) control.Visible = false;
                    }
                }

                //Add the QS Module to hide it. It needs to be visible server side because otherwise it won't output it's payload.
                clientSideChecks.Add(new ValidateCheckInfo() {ModuleID = ModuleId, Visible = false});
                JsonOutput = JsonExtensionsWeb.ToJson(clientSideChecks);
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