<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Main.ascx.cs" Inherits="InspectorIT.QSContent.Main" %>
<%@ Register TagPrefix="dnn" Namespace="DotNetNuke.Web.Client.ClientResourceManagement" Assembly="DotNetNuke.Web.Client" %>

<dnn:DnnJsInclude ID="qsContent" runat="server" FilePath="~/DesktopModules/InspectorIT/qsContent/js/qsContent.js" />

<script type="text/javascript">
    qsContentRegisteredModules.push(<%=JsonOutput%>);
</script>

<asp:Panel runat="server" ID="plConfig">
    <p><asp:Label runat="server" resourceKey="InitialMessage"></asp:Label></p>    
</asp:Panel>


