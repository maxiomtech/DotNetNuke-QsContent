<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="InspectorIT.QSContent.Admin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<%@ Register TagPrefix="dnn" TagName="Label" Src="~/controls/LabelControl.ascx" %>
<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/InspectorIT/QSContent/css/admin.min.css"></dnn:DnnCssInclude>
<dnn:DnnJsInclude ID="AngularJS" runat="server" FilePath="~/DesktopModules/InspectorIT/qsContent/js/angular.min.js" Priority="100" />
<dnn:DnnJsInclude ID="qsAdmin" runat="server" FilePath="~/DesktopModules/InspectorIT/qsContent/js/Admin.js" Priority="101" />

<div class="dnnForm iit-QsContent" ng-app="QsContent">
    <div id="QsAdmin" ng-controller="QsAdminCtl">
        <div class="addMenu">
            <div class="dnnFormItem">
                <dnn:label resourcekey="txtQuerystring" Text="Enter querystring key and value" Suffix=":" runat="server" />
                <input type="text" ID="txtQuerystring" ng-model="qs.Querystring" name="txtQuerystring" />
            </div>

            <div class="dnnFormItem">
                <dnn:label resourcekey="ddlModules" Text="Select the module you want to toggle" Suffix=":" runat="server" />
                <select ng-model="qs.CheckModuleID" ng-value="qs.ModuleId" ng-options="m.ModuleId as m.Name for m in modules | orderBy:'Name'"></select>

            </div>
            <div class="dnnFormItem">
                <dnn:label resourcekey="cbHideMatch" Text="Hide module if querystring matches" Suffix=":" runat="server" />
                
                <input type="checkbox" id="cbHideMatch" ng-model="qs.HideMatch" name="cbHideMatch" />
            </div>
            <div class="dnnFormItem">
                <dnn:label resourcekey="cbServerSideCheck" Text="Server side check" Suffix=":" runat="server" />
                <input type="checkbox" id="cbServerSideCheck" ng-model="qs.ServerSide" name="cbServerSideCheck" />
            </div>
        </div>
    
    
        <ul class="dnnActions dnnClear">
            <li><a ID="btnAdd" href="#" ng-click="SaveCheck($event)" class="dnnPrimaryAction">{{ButtonText}}</a></li>
            <li><a href="#" class="dnnSecondaryAction" ng-show="qs.ID > 0" ng-click="CancelSelection($event)">Cancel</a></li>
        </ul>
        
        
        <table border="0" cellspacing="0" cellpadding="0" class="iit-zebra">
            <tr>
                <th>&nbsp;</th>
                <th>ID</th>
                <th>Querystring</th>
                <th>Module</th>
                <th>Hide When Matched</th>
                <th>Server side check</th>
            </tr>
            <tr ng-repeat="check in checkValues" ng-class-even="'odd'">
                <td><a href="#" ng-click="SelectCheck($event)"><img src="<%= ResolveUrl("~/images/edit.gif") %>"/></a> <a href="#" ng-click="DeleteCheck($event)"><img src="<%= ResolveUrl("~/images/delete.gif") %>"/></a></td>
                <td>{{check.ID}}</td>
                <td>{{check.Querystring | truncate:25}}</td>
                <td>{{getModuleTitle(check.CheckModuleID) | truncate:25 }}</td>
                <td>{{check.HideMatch}}</td>
                <td>{{check.ServerSide}}</td>
            </tr>
        </table>

    </div>    
</div>

<script type="text/javascript">
    QsContent.options = {
        ServicePath: '<%= servicePath %>',
        PortalId: <%=PortalId %>,
        ModuleId: <%=ModuleId %>,
        TabId: <%=TabId %>

        };
</script>