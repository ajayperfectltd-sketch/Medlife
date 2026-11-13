<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AccountsReport.aspx.cs" Inherits="AccountsReport" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <script type="text/javascript">

        //function ClearSEC() {
        //    $('#txtMinPwdLen').val('');
        //    $('#txtMaxPwdLen').val('');
        //    $('#txtPwdChngPeriod').val('');

        //}
        //function ClearSLS() {
        //    $('#txtPortNo').val('');
        //    $('TxtBillPrint').val('');

        //}
        //function ClearGCFA() {
        //    $('#txtDecimalPlace').val('');
        //    $('#txtNoUsers').val('');
        //    $('#ddlCurrency').val('0');

        //}
        //function ClearINV() {
        //    $('#ddlCategoryFirst').val('0');
        //    $('#ddlStore').val('0');

        //}
        //function ClearACC() {
        //    $('#ddlCardAccountHead').val('0');
        //    $('#ddlCardAccountSubHead').val('0');
        //    $('#ddlCashAccountHead').val('0');
        //    $('#ddlCardAccountSubHead').val('0');
        //    $('#ddlPurchaseAccountHead').val('0');

        //    $('#ddlPurchaseAccountSubHead').val('0');
        //    $('#ddlSalesAccountHead').val('0');
        //    $('#ddlSalesAccountSubHead').val('0');
        //}
        //function ClearCheckList() {
        //    $('input:checkbox').removeAttr('checked');
        //}
        function BlurShift() {
            $("#btnShiftBlurTrigger").click();

        }
        function BlurSubHead() {
            $("#btnSubHeadBlurTrigger").click();

        }
        function BlurGroup() {
            $("#btnGroupBlurTrigger").click();

        }
        function ShowDate() {
            var toDate = new Date();
            $("#txtFromDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtToDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtAsOnDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtCurrentDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtPassDate").val(toDate.format("dd/MM/yyyy HH:mm:ss"));


        }
        function popUp() {
            if ($("#hfReportType").val() == "CRPT") {
                url = "ReportView.aspx?uid=" + $("#hfUrl").val() + '&clnt=' + $("#hfClient").val() + '&datepass=' + $("#hfdate").val() + '&Rptype=' + $("#ddlReportType").val();
                popUpObj = window.open(url, 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');
                popUpObj.focus();
            }
            else {
                url = "CommonReportView.aspx?uid=" + $("#hfUrl").val() + '&clnt=' + $("#hfClient").val() + '&datepass=' + $("#hfdate").val();
                popUpObj = window.open(url, 'self', 'left=200,top=50,width=900,height=680,status=0,resizable=0,fullscreen=0,scrollbars=1');
                popUpObj.focus();
            }
          
        }
    </script>



</head>

<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="btn_bg5"> 
                   <div class="heading2">
                       <div class="user">Reports</div>
                       </div>
                      
                
                    <div class="clear"></div>
                    <div class="group_bg2">
                         <div class="main_bg4">  
                        <div id="divAdd" runat="server" style="">
                            <div class="divL" style="width: 250px;">
                                <ul class="contentReportul" style="width: 250px; border-right: 1px solid #e4e4e4; margin-top:38px;  padding-bottom:11px;" >
                                    <asp:TreeView ID="tvSettings" runat="server" CollapseImageUrl="~/images/down2.png" ExpandImageUrl="~/images/side2.png" LeafNodeStyle-ImageUrl="~/images/side2.png"  Width="185px"  OnSelectedNodeChanged="tvSettings_SelectedNodeChanged" ExpandDepth="0">
                                        <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" />
                                        <Nodes>
                                        </Nodes>
                                        <NodeStyle Font-Names="Tahoma" Font-Size="10pt" ForeColor="#3a5f5f" Font-Bold="true" HorizontalPadding="5px" NodeSpacing="3px" VerticalPadding="0px" />
                                        <ParentNodeStyle Font-Bold="False" />
                                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" ForeColor="#B7D1E0" />
                                    </asp:TreeView>
                                </ul>

                            </div>
                            <div id="divPassDate" style="display: none">

                                <asp:TextBox ID="txtPassDate" runat="server" CssClass="textbox" TabIndex="2"></asp:TextBox>
                                <asp:TextBox ID="txtCurrentDate" runat="server" CssClass="textbox" TabIndex="2"></asp:TextBox>

                            </div>
                            <div class="divL" style="width: 65%;">
                                <ul class="contentReportul">

                                    <div style="float: left; direction: ltr; width: 100%;">

                                        <div id="divTitle" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="lblHeadText" runat="server" Font-Size="18px" CssClass="HeadLabel" Text=""></asp:Label>
                                                     <asp:HiddenField ID="hfReportType" runat="server" />

                                                </li>
                                            </ul>
                                        </div>
                                        <div id="divDate" runat="server">
                                            <ul class="contentReportul">
                                                <li>
                                                    <asp:Label ID="Label2" runat="server" Text="From Date" CssClass="label"></asp:Label>
                                                    <asp:TextBox ID="txtFromDate" runat="server" CssClass="textbox" TabIndex="1"></asp:TextBox>
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>


                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                                    </cc:ValidatorCalloutExtender>
                                                    <cc:CalendarExtender ID="txtFromDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy">
                                                    </cc:CalendarExtender>

                                                </li>
                                                <li>
                                                    <asp:Label ID="Label3" runat="server" Text="To Date" CssClass="label"></asp:Label>
                                                    <asp:TextBox ID="txtToDate" runat="server" CssClass="textbox" TabIndex="2"></asp:TextBox>
                                                    <asp:CompareValidator ID="cmpCurrentDate" runat="server" ErrorMessage="Date can not be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtToDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>


                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                                    </cc:ValidatorCalloutExtender>
                                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtToDate" Format="dd/MM/yyyy">
                                                    </cc:CalendarExtender>


                                                </li>
                                            </ul>
                                        </div>
                                        <%-- <div id="divcatgry" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label11" runat="server" Text="Category First" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlCategoryFirst" runat="server" CssClass="dropdown" TabIndex="3">
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                        </div>--%>


                                        <div id="divTransType" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label1" runat="server" Text="Trans Type" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddltransType" runat="server" CssClass="dropdown" TabIndex="4">
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                        </div>
                                        <div id="divAccountHead" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label10" runat="server" Text="Account Head" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlAccountHead" runat="server" CssClass="dropdown" TabIndex="5" onblur="BlurSubHead();">
                                                    </asp:DropDownList>
                                                    <%--  <asp:RequiredFieldValidator ID="rqvddlAcchead" Style="float: left;" runat="server"  ErrorMessage="Required field" Display="Dynamic" Enabled="false" Font-Bold="True" Font-Size="Large" ForeColor="Red" SetFocusOnError="True" ToolTip="Required field" ValidationGroup="s" ControlToValidate="ddlAccountHead" InitialValue="0">*</asp:RequiredFieldValidator>--%>
                                                    <asp:RequiredFieldValidator ID="rfvAccountHead" runat="server" ErrorMessage="Select an account head" ControlToValidate="ddlAccountHead" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="rfvAccountHead">
                                                    </cc:ValidatorCalloutExtender>

                                                </li>
                                            </ul>
                                        </div>

                                        <div id="divAccountSubHead" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label12" runat="server" Text="Account Sub Head" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlAccountSubHead" runat="server" CssClass="dropdown" TabIndex="6">
                                                    </asp:DropDownList>
                                                   <%-- <asp:RequiredFieldValidator ID="rfvAccountSubHead" runat="server" ErrorMessage="Select an account Sub head" ControlToValidate="ddlAccountSubHead" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit" Enabled="false"></asp:RequiredFieldValidator>
                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="rfvAccountSubHead">
                                                    </cc:ValidatorCalloutExtender>--%>

                                                </li>
                                            </ul>
                                        </div>




                                        <div id="divCounter" runat="server">
                                            <ul class="contentReportul">
                                                <li>
                                                    <asp:Label ID="Label13" runat="server" CssClass="label" Text="Counter"></asp:Label>
                                                    <asp:DropDownList ID="ddlCounter" runat="server" CssClass="dropdown" TabIndex="2" onblur="BlurShift();"></asp:DropDownList>
                                                   
                                                </li>
                                            </ul>
                                        </div>
                                        <div id="divShift" runat="server">
                                            <ul class="contentReportul">
                                                 <li>
                                            <asp:Label ID="Label11" runat="server" CssClass="label" Text="Shift"></asp:Label>
                                            <asp:DropDownList ID="ddlShift" runat="server" CssClass="dropdown" TabIndex="3" ></asp:DropDownList>
                                                                                   
                                        </li>
                                            </ul>
                                        </div>
                                        <div id="divAsOnDate" runat="server">
                                            <ul class="contentReportul">
                                                <li>
                                                    <asp:Label ID="Label8" runat="server" Text="As On Date" CssClass="label"></asp:Label>
                                                    <asp:TextBox ID="txtAsOnDate" runat="server" CssClass="textbox" TabIndex="1"></asp:TextBox>
                                                     <asp:CompareValidator ID="CompareValidator2" runat="server" ErrorMessage="Date can not be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtAsOnDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>


                                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                                    </cc:ValidatorCalloutExtender>
                                                    <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtAsOnDate" Format="dd/MM/yyyy">
                                                    </cc:CalendarExtender>
                                                </li>

                                            </ul>
                                        </div>
                                        <div id="divAccountGroup" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label14" runat="server" Text="Account Group" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlAccountGroup" runat="server" CssClass="dropdown" TabIndex="2" onblur="BlurGroup();">
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                        </div>
                                        <div id="divAccountsubGroup" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label15" runat="server" Text="Account Sub Group" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlAccountSubGroup" runat="server" CssClass="dropdown" TabIndex="3">
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                        </div>
                                        <div id="divStore" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label6" runat="server" Text="Store" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropdown" TabIndex="8">
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                        </div>
                                          <div id="divStoreCash" runat="server">
                                               <ul class="contentReportul">
                                          <li>

                                                    <asp:Label ID="Label17" runat="server" Text="Store" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlstoreCashbook" runat="server" CssClass="dropdown" TabIndex="8">
                                                    </asp:DropDownList>


                                                </li>
                                                   </ul>
                                            </div>
                                        <div id="divItems" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label5" runat="server" Text="Items" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlItems" runat="server" CssClass="dropdown" TabIndex="9">
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                        </div>
                                        <div id="divIssueMode" runat="server">
                                            <ul class="contentReportul">
                                                <li>
                                                    <asp:Label ID="Label9" runat="server" Text="Issue Mode" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlIssueMode" CssClass="dropdown" runat="server" TabIndex="10" AutoPostBack="True" ToolTip="Issue Mode">
                                                        <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                        <asp:ListItem Value="1">To Store</asp:ListItem>
                                                        <asp:ListItem Value="2">To Vendor Return</asp:ListItem>
                                                        <asp:ListItem Value="3">Issue Out</asp:ListItem>
                                                        <asp:ListItem Value="4">Wastage</asp:ListItem>
                                                        <asp:ListItem Value="5">Damage</asp:ListItem>
                                                        <asp:ListItem Value="6">Missing</asp:ListItem>
                                                    </asp:DropDownList>
                                                </li>

                                            </ul>
                                        </div>
                                        <div id="divStoreFromTo" runat="server">

                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label4" runat="server" Text="From Store" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlFromStore" runat="server" CssClass="dropdown" TabIndex="11">
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label7" runat="server" Text="To Store" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlToStore" runat="server" CssClass="dropdown" TabIndex="12">
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                        </div>

                                         <div id="DivReportType" runat="server">
                                            <ul class="contentReportul">
                                                <li>

                                                    <asp:Label ID="Label24" runat="server" Text="Report Type" CssClass="label"></asp:Label>
                                                    <asp:DropDownList ID="ddlReportType" runat="server" CssClass="dropdown" TabIndex="12">
                                                        <asp:ListItem Selected="True" Value="0">PortableDocFormat</asp:ListItem>
                                                        <asp:ListItem Value="1">CrystalReport</asp:ListItem>
                                                        <asp:ListItem Value="2">Excel</asp:ListItem>
                                                          <asp:ListItem Value="3">ExcelRecord</asp:ListItem>
                                                         <asp:ListItem Value="4">ExcelWorkbook</asp:ListItem>
                                                      
                                                    </asp:DropDownList>


                                                </li>
                                            </ul>
                                        </div>


                                        <br />
                                        <div id="divButtons" runat="server">
                                            <ul class="contentReportul">
                                                <li class="btnMargin">
                                                    <asp:HiddenField ID="hfINVSubModule" runat="server" />
                                                    <asp:HiddenField ID="hfdate" runat="server" ClientIDMode="Static" />
                                                    <asp:HiddenField ID="hfUser" runat="server" />
                                                    <asp:HiddenField ID="hfClient" runat="server" />
                                                    <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="13" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" CausesValidation="false" meta:resourcekey="btnClear" OnClick="btnClear_Click" TabIndex="14" />

                                                </li>
                                            </ul>
                                        </div>

                                        <div style="float: right; direction: rtl; width: 100%;">
                                        </div>


                                        <li class="btnMargin">

                                            <asp:HiddenField ID="hfItemsId" runat="server" />

                                            <asp:HiddenField ID="hfUrl" runat="server" />
                                            <asp:HiddenField ID="hfUpdatePermission" runat="server" />
                                            <asp:HiddenField ID="hfAddPermission" runat="server" />
                                            <asp:Button ID="btnSubHeadBlurTrigger" Text="text" runat="server" Style="display: none;" OnClick="btnSubHeadBlurTrigger_Click" />
                                            <asp:Button ID="btnShiftBlurTrigger" Text="text" runat="server" Style="display: none;" OnClick="btnShiftBlurTrigger_Click" />    
                                            <asp:Button ID="btnGroupBlurTrigger" Text="text" runat="server" Style="display: none;" OnClick="btnGroupBlurTrigger_Click" />                                    

                                        </li>

                                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                                        <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>
                                        <li style="width: 450px; float: left;"></li>
                                </ul>
                            </div>
                        </div>
                             </div>
                    </div>


                </div>
                <%--<asp:HiddenField ID="hfUser" runat="server" />
                <asp:HiddenField ID="hfClient" runat="server" />--%>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>

</html>
