// ***********************************************************************
// Assembly         : QSContent
// Author           : Jonathan Sheely
// Created          : 05-12-2013
//
// Last Modified By : Jonathan Sheely
// Last Modified On : 05-12-2013
// ***********************************************************************
// <copyright file="Admin.ascx.cs" company="InspectorIT">
//     Copyright (c) InspectorIT. All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using DotNetNuke.Entities.Modules;
using InspectorIT.QSContent.Components.Controller;
using InspectorIT.QSContent.Components.Entities;

namespace InspectorIT.QSContent
{
    /// <summary>
    /// Class Admin
    /// </summary>
    public partial class Admin : PortalModuleBase
    {
        /// <summary>
        /// Handles the Load event of the Page control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                ModuleController mc = new ModuleController();
                Dictionary<int, ModuleInfo> modules = mc.GetTabModules(TabId).Where(x => x.Value.IsDeleted==false && x.Value.ModuleID!=ModuleId).ToDictionary(x => x.Key, x => x.Value);
                foreach (KeyValuePair<int,ModuleInfo> m in modules)
                {
                    ddlModules.Items.Add(new ListItem(((ModuleInfo)m.Value).ModuleTitle, m.Key.ToString()));
                }
                BindItems();

            }
        }

        /// <summary>
        /// Binds the items.
        /// </summary>
        private void BindItems()
        {
            rptModules.DataSource = DataController.GetChecks(ModuleId,true);
            rptModules.DataBind();
        }

        /// <summary>
        /// Handles the ItemDataBound event of the rptModules control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="RepeaterItemEventArgs"/> instance containing the event data.</param>
        protected void rptModules_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if(e.Item.DataItem!=null)
            {
                CheckInfo checkInfo = e.Item.DataItem as CheckInfo;

                ModuleInfo modInfo = new ModuleController().GetModule(checkInfo.CheckModuleID);

                if(modInfo==null)
                {
                    DataController.DeleteCheck(checkInfo.ID,ModuleId);
                    e.Item.Visible = false;
                }else
                {
                    ((Literal) e.Item.FindControl("txtModuleTitle")).Text = modInfo.ModuleTitle;
                    ((ImageButton)e.Item.FindControl("btnDelete")).CommandArgument = checkInfo.ID.ToString();    
                }

                
            }
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            DataController.AddCheck(ModuleId, txtQuerystring.Text, cbHideMatch.Checked, Convert.ToInt32(ddlModules.SelectedValue), UserId);
            BindItems();
            txtQuerystring.Text = "";

        }

        /// <summary>
        /// Handles the Click event of the btnDelete control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ImageClickEventArgs"/> instance containing the event data.</param>
        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            DataController.DeleteCheck(Convert.ToInt32(((ImageButton) sender).CommandArgument),ModuleId);
            BindItems();
        }
    }
}