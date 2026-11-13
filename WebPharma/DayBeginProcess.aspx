<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DayBeginProcess.aspx.cs" Inherits="DayBeginProcess" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1" />
    <title>Pharma</title>
    <link href="css/main.css" rel="stylesheet" />
    <%-- <link href="../../CSS/perfect.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .style3
        {
            width: 100%;
            height: 433px;
        }

        .style4
        {
            width: 301px;
        }

        .style6
        {
            width: 301px;
            height: 114px;
        }

        .style7
        {
            height: 114px;
        }

        .style8
        {
            height: 22px;
        }

        .style9
        {
            width: 100%;
            height: 436px;
        }

        .style10
        {
            width: 100%;
            height: 434px;
        }

        .style11
        {
            width: 100%;
            height: 435px;
        }

        .style12
        {
            width: 100%;
            height: 434px;
        }

        .auto-style1
        {
            width: 666px;
        }

        .auto-style4
        {
            height: 19px;
        }
    </style>

    <script language="javascript" type="text/javascript" src="../../Scripts/DateTimePicker.js"></script>

    <script language="javascript" type="text/javascript">


        function SaveClick() {
            document.getElementById('btnSave').click();
            return false;
        }
        function ProcessClick() {
            document.getElementById('btnProcess').click();
            return false;
        }
        function ScriptFillClick(cntrlname) {
            document.getElementById('HdValue').value = cntrlname;
            document.getElementById('btnScriptFill').click();
            return false;
        }

        //function ClearAdd() {
        //    submit = 0;

        //    var toDate = new Date();
        //    $("#txtopnBalanceDate").val(toDate.format("dd/MM/yyyy"));
        //    alert('jj');
        //}
    </script>

</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <div style="background-image: url('../Images/Login/DESIGN1.jpg'); vertical-align: middle; height: 100%; width: 100%">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>

                    <table style="text-align: center; height: 100%; width: 100%;">
                        <tr>
                            <td valign="middle" align="center">
                                <table style="width: 985px; height: 595px">
                                    <tr style="width: 100%; height: 100%">
                                        <td style="background-image: url('../../Images/newdaybeg2.jpg'); background-repeat: no-repeat; width: 100%; height: 100%"
                                            align="left" valign="top">
                                            <table class="style3">
                                                <tr>
                                                    <td class="style4">&nbsp;
                                                    </td>
                                                    <td class="auto-style1">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="style4">&nbsp;
                                                    </td>
                                                    <td class="auto-style1">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr id="trMessage" runat="server">
                                                    <td class="style4">&nbsp;
                                                    </td>
                                                    <td style="border-width: 1px; border-color: #000000; border-top-style: solid; border-right-style: solid; border-left-style: solid;" class="auto-style1">
                                                           <div id="divaccountlinking" style="height: 436px" runat="server" >
                                                            <table align="left" class="style3">
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <table id="Table1" class="style2">
                                                                          
                                                                            <tr>
                                                                                <td class="auto-style4">&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">&nbsp;<asp:Label ID="Label4" runat="server" Font-Bold="True" ForeColor="Red" Height="12px"
                                                                                    Text="Warning :" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label6" runat="server" Font-Bold="True" ForeColor="Red" Height="18px"
                                                                                        Text="Account linking is Enabled, Day Begin Process Not Done " Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style8"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="divdaybegin" style="height: 436px" runat="server">
                                                            <table align="left" class="style3">
                                                                <tr>
                                                                    <td align="center" valign="middle">
                                                                        <table id="tblMessage" class="style2">
                                                                            <tr>
                                                                                <td align="left">&nbsp;<asp:Label ID="Label11" runat="server" ForeColor="Black" Text="    This Process Will Lock All Previous Transactions" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">
                                                                                    <asp:Label ID="Label12" runat="server" Text="Changes The Business Dates To Current Date" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="auto-style4">&nbsp;
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td align="left">&nbsp;<asp:Label ID="Label13" runat="server" Font-Bold="True" ForeColor="Red" Height="12px"
                                                                                    Text="Warning :" Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label14" runat="server" Font-Bold="True" ForeColor="Red" Height="18px"
                                                                                        Text="Please Make Sure That Day Book For The Previous day Is Taken." Font-Size="Medium"></asp:Label>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td class="style8"></td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="divbranch" style="height: 436px" runat="server">
                                                            <table class="style9">
                                                                <tr>
                                                                    <td align="center">
                                                                        <table id="tblOpcashBalance">
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label3" runat="server" Height="16px" Text="Day Date" Width="91px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <table align="left" cellpadding="0" cellspacing="0">
                                                                                        <tr>
                                                                                            <td>
                                                                                                <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtopnBalanceDate" TabIndex="5" AutoComplete="off" Width="88%" MaxLength="9" />
                                                                                                <cc:CalendarExtender ID="txtopnbalanceDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtopnBalanceDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                                                                                <asp:RequiredFieldValidator ID="reqvalOpenbalanceDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtopnBalanceDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                                                                <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalOpenbalanceDate">
                                                                                                </cc:ValidatorCalloutExtender>
                                                                                                <%-- <td style="width: 118px">
                                                                                                <asp:TextBox ID="txtDayEndDate" runat="server" Height="20px" MaxLength="10" SkinID="datebox-mandatory"
                                                                                                    TabIndex="9" ToolTip="Day End Date" Width="116px"></asp:TextBox>
                                                                                            </td>
                                                                                            <td>
                                                                                                <a href="javascript:NewCal('txtDayEndDate','ddmmyyyy',false,'')">
                                                                                                    <img id="Img3" runat="server" alt="Deposit date" binvoice="0" height="16" src="../../Images/Calender/CALLL.GIF"
                                                                                                        style="border-top-style: none; border-right-style: none; border-left-style: none; border-bottom-style: none"
                                                                                                        width="16" /></a>
                                                                                            </td>--%>
                                                                                            </td>
                                                                                        </tr>
                                                                                    </table>
                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label2" runat="server" Text="Store" Width="92px"></asp:Label>
                                                                                </td>
                                                                                <td>



                                                                                    <asp:DropDownList ID="ddlBranch" CssClass="dropdownMandatory" runat="server" AutoPostBack="True" TabIndex="1" OnSelectedIndexChanged="ddlBranch_SelectedIndexChanged" EnableViewState="true" Width="95%"></asp:DropDownList>
                                                                                    <asp:RequiredFieldValidator ID="rfddlBranch" Style="float: right;" runat="server" ErrorMessage="Required Branch" Display="None" Font-Bold="True" Font-Size="Large" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="submit" InitialValue="0" ControlToValidate="ddlBranch">*</asp:RequiredFieldValidator>
                                                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rfddlBranch">
                                                                                    </cc:ValidatorCalloutExtender>



                                                                                </td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label5" runat="server" Height="17px" Text="Opening Stock"
                                                                                        Width="146px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtCashBalance" TabIndex="5" AutoComplete="off" Width="88%" MaxLength="9" />

                                                                                  <%--  <asp:TextBox ID="txtCashBalance" runat="server" AutoPostBack="False" Font-Bold="False"
                                                                                        Font-Size="Medium" Height="15px" SkinID="numbox-mandatory" TabIndex="12" ToolTip="Cash Balance"
                                                                                        Width="155px"></asp:TextBox>--%>
                                                                                </td>
                                                                            </tr>





                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="Label1" runat="server" Height="17px" Text="Opening Liability"
                                                                                        Width="146px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                      <asp:TextBox runat="server" CssClass="textbox" ID="Txtliability" TabIndex="5" AutoComplete="off" Width="88%" MaxLength="9" />

                                                                                   <%-- <asp:TextBox ID="Txtliability" runat="server" AutoPostBack="False" Font-Bold="False"
                                                                                        Font-Size="Medium" Height="15px" SkinID="numbox-mandatory" TabIndex="12" ToolTip="Opening Liability"
                                                                                        Width="155px"></asp:TextBox>--%>
                                                                                </td>
                                                                            </tr>



                                                                            <tr>
                                                                                <td>
                                                                                    <asp:Label ID="lbldifference" runat="server" Height="17px" Text=" Difference"
                                                                                        Width="146px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtdifference" TabIndex="5" AutoComplete="off" Width="88%" MaxLength="9" />

                                                                                  <%--  <asp:TextBox ID="txtdifference" runat="server" AutoPostBack="False" Font-Bold="False"
                                                                                        Font-Size="Medium" Height="15px" SkinID="numbox-mandatory" TabIndex="12" ToolTip="Difference"
                                                                                        Width="155px"></asp:TextBox>--%>
                                                                                </td>
                                                                            </tr>









                                                                            <tr id="IdCheckSum" runat="server" visible="false">
                                                                                <td>
                                                                                    <asp:Label ID="lblDbCheckSum" runat="server" Height="16px" Text="CheckSum" ToolTip="Enter checksum Value"
                                                                                        Width="91px"></asp:Label>
                                                                                </td>
                                                                                <td>
                                                                                    <asp:TextBox ID="txtDbCheckSum" runat="server" AutoPostBack="True" Font-Bold="False"
                                                                                        Font-Size="Medium" Height="25px" SkinID="numbox-mandatory" TabIndex="12" ToolTip="Cash Balance"
                                                                                        Width="134px"></asp:TextBox>
                                                                                </td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="div1" runat="server" style="height: 436px; overflow: auto; width: 603px;">
                                                            <table id="tblAttandanceMarking">
                                                                <tr>
                                                                    <td class="HeaderStyle">
                                                                        <asp:Label ID="lblattendancemarking" runat="server" Height="18px" Text="Attandance Marking"
                                                                            Width="240px" Font-Size="Medium"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <%--<div id="divattendance" runat="server" style="height: 436px; overflow: auto; width: 603px;">--%>
                                                                        <%--<table align="center" cellpadding="0" cellspacing="0" class="style10">
                                                                            <tr>
                                                                                <td> <%--align="left" >--%>

                                                                        <%--<asp:GridView ID="gdvAttendance" runat="server" AutoGenerateColumns="False" OnRowDataBound="gdvAttendance_RowDataBound"
                                                                                        SkinID="gridview">
                                                                                        <Columns>
                                                                                            <asp:TemplateField>
                                                                                                <HeaderTemplate>
                                                                                                    <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="True" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                                                                </HeaderTemplate>
                                                                                                <ItemTemplate>
                                                                                                    <asp:CheckBox ID="chkMark" runat="server" />
                                                                                                </ItemTemplate>
                                                                                                <HeaderStyle HorizontalAlign="Left" />
                                                                                                <ItemStyle HorizontalAlign="Left" />
                                                                                            </asp:TemplateField>
                                                                                            <asp:BoundField DataField="UserCode" HeaderText="UserCode" />
                                                                                            <asp:BoundField DataField="UserName" HeaderText="UserName" />
                                                                                            <asp:BoundField DataField="ID_User" HeaderText="ID_User" />
                                                                                        </Columns>
                                                                                    </asp:GridView>--%>
                                                                        <asp:GridView ID="gdvAttendance" runat="server" CssClass="grid"
                                                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                                                            BackColor="white" BorderColor="white" BorderStyle="Solid" BorderWidth="1px" ForeColor="Black" GridLines="Vertical" OnRowDataBound="gdvAttendance_RowDataBound">
                                                                            <AlternatingRowStyle BackColor="#CCE0EB" />
                                                                            <Columns>
                                                                                <asp:TemplateField>
                                                                                    <HeaderTemplate>
                                                                                        <asp:CheckBox ID="chkHeader" runat="server" AutoPostBack="True" OnCheckedChanged="chkHeader_CheckedChanged" />
                                                                                    </HeaderTemplate>
                                                                                    <ItemTemplate>
                                                                                        <asp:CheckBox ID="chkMark" runat="server" />
                                                                                    </ItemTemplate>
                                                                                    <HeaderStyle HorizontalAlign="Left" />
                                                                                    <ItemStyle HorizontalAlign="Left" />
                                                                                </asp:TemplateField>
                                                                                <asp:BoundField DataField="UserCode" HeaderText="UserCode" />
                                                                                <asp:BoundField DataField="UserName" HeaderText="UserName" />
                                                                                <asp:BoundField DataField="ID_User" HeaderText="ID_User" />
                                                                            </Columns>
                                                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                                                            <FooterStyle BackColor="#CCCCCC" />
                                                                            <HeaderStyle BackColor="#12361D" Font-Bold="True" ForeColor="White" Height="30px" />
                                                                            <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                                <%--    </table>--%>
                                                                <%--     </div>--%>
                                                                <%-- </td>
                                                            </tr>--%>
                                                            </table>
                                                        </div>
                                                        <div id="divgrids" runat="server" style="height: 436px; overflow: auto;">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style11">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <asp:GridView ID="gdvMessage" runat="server" Font-Size="12pt" OnRowDataBound="gdvMessage_RowDataBound"
                                                                            SkinID="gridview">
                                                                            <RowStyle Font-Size="10pt" />
                                                                            <Columns>
                                                                                <asp:ImageField DataImageUrlField="Progress">
                                                                                </asp:ImageField>
                                                                            </Columns>
                                                                            <HeaderStyle Font-Size="12pt" ForeColor="Black" />
                                                                        </asp:GridView>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                        <div id="divdayfinished" runat="server" style="height: 436px">
                                                            <table align="center" cellpadding="0" cellspacing="0" class="style12">
                                                                <tr>
                                                                    <td align="center">
                                                                        <%--  style="border: 1px solid #000000" valign="middle">--%>
                                                                        <asp:Label ID="lblFinishMessage" runat="server" Font-Italic="True" Font-Size="20pt"
                                                                            ForeColor="Red" Text="DAY BEGIN PROCESS FINISHED"></asp:Label>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </td>
                                                </tr>

                                                <tr>

                                                    <td align="right">
                                                        <asp:Button ID="btnProcess" runat="server" OnClick="btnProcess_Click" Style="display: none"
                                                            Text="Process" />
                                                    </td>
                                                    <td align="right" style="border: 1px solid #000000;" class="auto-style1">


                                                        <asp:Button ID="btnScriptFill" runat="server" OnClick="btnScriptFill_Click" Style="display: none"
                                                            Text="Process" />
                                                        <asp:Button ID="btnSave" runat="server" BorderColor="#000000" OnClick="btnSave_Click"
                                                            OnClientClick="this.enabled=false;" SkinID="button" TabIndex="22" Text="Add"
                                                            UseSubmitBehavior="False" />

                                                    </td>

                                                </tr>
                                                <tr id="tr2" runat="server">
                                                    <td class="style4">&nbsp;
                                                    </td>
                                                    <td class="auto-style1">
                                                        <asp:BulletedList ID="bltErrorList" runat="server" Font-Bold="true" ForeColor="#FF3300" Font-Size="Medium">
                                                        </asp:BulletedList>
                                                    </td>
                                                </tr>
                                                <caption>
                                                </caption>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <%--  <asp:HiddenField ID="hdfWebPharmaTransaction" runat="server" />--%>
        <asp:HiddenField ID="hdfDaybegin" runat="server" />
        <asp:HiddenField ID="hfClient" runat="server" />
        <asp:HiddenField ID="hfUser" runat="server" />
        <asp:HiddenField ID="hfIsAdd" runat="server" />
        <asp:HiddenField ID="hfIsUpdate" runat="server" />
        <asp:HiddenField ID="hfIsDelete" runat="server" />
        <%--   <asp:HiddenField ID="HdValue" runat="server" />--%>
    </form>
</body>
</html>
