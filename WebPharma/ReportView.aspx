<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportView.aspx.cs" Inherits="Presentation_Common_ReportView" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
     Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<%--<%@ Register assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
     namespace="CrystalDecisions.Web" tagprefix="CR" %>--%>
<script src="aspnet_client/system_web/4_0_30319/crystalreportviewers13/js/crviewer/crv.js"></script>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head  runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>     
              <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="true" ToolPanelView="None"
                   DisplayStatusbar="False" HasCrystalLogo="False" HasDrillUpButton="False" HasToggleGroupTreeButton="False"
                   HasToggleParameterPanelButton="False" PrintMode="ActiveX" Height="50px" Width="350px" />        
    </div>
    </form>
</body>
</html>

