<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommonReportView.aspx.cs" Inherits="CommonReportView" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91"
     Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>--%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
           <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
   
</head>
<body>
    <form id="form1" runat="server">    
    <div>   
          <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="20000"  runat="server"></asp:ScriptManager>
          <rsweb:ReportViewer ID="ReportViewer1" runat="server" AsyncRendering="False" Height="900px" Width="800px" PageCountMode="Actual" ShowPrintButton="true" >
          </rsweb:ReportViewer>
    </div>  `       
    </form>
</body>
</html>
