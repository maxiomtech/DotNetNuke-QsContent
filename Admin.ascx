<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Admin.ascx.cs" Inherits="InspectorIT.QSContent.Admin" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>
<dnn:DnnCssInclude ID="DnnCssInclude1" runat="server" FilePath="~/DesktopModules/InspectorIT/QSContent/css/admin.css"></dnn:DnnCssInclude>


<div class="dnnForm">
    <div class="addMenu">
        <div>
            <label>Enter querystring key and value <div class="subText">(RegEx enabled, comma deliminated values considered as OR)</div></label>
            <asp:TextBox runat="server" ID="txtQuerystring"></asp:TextBox>
        </div>

        <div style="margin-top:8px;">
            <label>Select the module you want to toggle</label>
            <asp:DropDownList runat="server" ID="ddlModules"/>

        </div>
        <div style="margin-top:8px;">
            <label>Hide module if querystring matches.</label>
            <asp:CheckBox runat="server" ID="cbHideMatch" />
        </div>
    </div>
    <ul class="dnnActions dnnClear">
    <li><asp:LinkButton runat="server" ID="btnAdd" Text="Add" OnClick="btnAdd_Click" CssClass="dnnPrimaryAction"></asp:LinkButton></li>
    </ul>
    <asp:Repeater runat="server" ID="rptModules" OnItemDataBound="rptModules_ItemDataBound">
        <HeaderTemplate>
            <table border="0" cellspacing="2" cellpadding="2">
                <tr class='headerTitle'>
                <td style="width:64px;"></td>
                <td>Querystring</td>
                <td>Module</td>
                    <td>Hide Match</td>
                </tr>
        </HeaderTemplate>
        <ItemTemplate>
            <tr>
                <td style="width:64px;"><asp:ImageButton runat="server" ID="btnDelete" ImageUrl="~/images/delete.gif" CommandName="Delete" OnClientClick="return confirm('Are you sure you want delete');" OnClick="btnDelete_Click"></asp:ImageButton></td>
                <td style="text-wrap: none"><%#Eval("Querystring") %></td>
                <td><asp:Literal runat="server" ID="txtModuleTitle"></asp:Literal></td>
                <td><%# Eval("HideMatch") %></td>
            </tr>
        </ItemTemplate>
        <FooterTemplate>
            </table>
        </FooterTemplate>
    </asp:Repeater>
</div>