using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Services;
using System.Web.Services;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using DotNetNuke.Security.Permissions;
using InspectorIT.QSContent.Components.Controllers;
using InspectorIT.QSContent.Components.Entities;

namespace InspectorIT.QSContent
{
    /// <summary>
    /// Summary description for WebService
    /// </summary>
    [WebService(Namespace = "http://inspectorit.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [ScriptService]
    public class WebService : System.Web.Services.WebService
    {

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public GetAdminPayload GetAdmin(int tabId, int moduleId)
        {
            if (ValidateAuthentication() && HasPermission(moduleId))
            {
                ModuleController mc = new ModuleController();
                List<Modules> modules = mc.GetTabModules(tabId).Where(x => x.Value.IsDeleted == false && x.Value.ModuleID != moduleId)
                                          .Select(x => new Modules() {ModuleId = x.Key, Name = x.Value.ModuleTitle}).ToList();

                return new GetAdminPayload {CheckValues = DataController.GetChecks(moduleId),Modules = modules};
            }
            return null;

        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public GetAdminPayload SaveCheck(int tabId, int moduleId, CheckInfo checkInfo)
        {
            if (ValidateAuthentication() && HasPermission(moduleId))
            {
                if (checkInfo.ID > 0)
                {
                    DataController.UpdateCheck(checkInfo.ID, moduleId, checkInfo.Querystring, checkInfo.HideMatch,
                                           checkInfo.ServerSide, checkInfo.CheckModuleID,checkInfo.CreatedByUserID);
                }
                else
                {
                    DataController.AddCheck(moduleId, checkInfo.Querystring, checkInfo.HideMatch,
                                        checkInfo.ServerSide, checkInfo.CheckModuleID, UserInfo.UserID);    
                }
                
                return GetAdmin(tabId,moduleId);

            }
            return null;
        }

        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public bool DeleteCheck(int tabId, int moduleId, int checkId)
        {
            if (ValidateAuthentication() && HasPermission(moduleId))
            {
                DataController.DeleteCheck(checkId,moduleId);
                return true;
            }
            return false;
        }

        private bool ValidateAuthentication()
        {
            if (HttpContext.Current.Request.IsAuthenticated)
            {
                UserInfo user = UserController.GetUserByName(PortalSettings.Current.PortalId, HttpContext.Current.User.Identity.Name);
                HttpContext.Current.Items["UserInfo"] = user;
                if (user != null && user.UserID != -1)
                {
                    UserInfo = user;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

            return true;

        }

        private bool HasPermission(int moduleId)
        {
            var moduleInfo = new ModuleController().GetModule(moduleId);
            bool hasPermissions = false;
            if (moduleInfo != null)
            {
                hasPermissions = ModulePermissionController.HasModuleAccess(SecurityAccessLevel.Edit, "EDIT", moduleInfo);

            }

            return hasPermissions;
        }

        private UserInfo _userInfo;
        private UserInfo UserInfo
        {
            get { return _userInfo; }
            set { _userInfo = value; }
        }
    }
}
