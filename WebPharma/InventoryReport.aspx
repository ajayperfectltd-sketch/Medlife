<%@ Page Language="C#" AutoEventWireup="true" CodeFile="InventoryReport.aspx.cs" Inherits="InventoryReport" %>

<%@ Register Assembly="TimePicker" Namespace="MKB.TimePicker" TagPrefix="MKB" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>

    <script type="text/javascript">
        function txtCustRefno_onBlur() {
            $("#txtCustomer").val('');
        }
        function txtDoctRefno_onBlur() {
            $("#txtDoctor").val('');
        }
        function ClearCustomer() {
            $("#txtCustRefno").val('');
            $("#txtCustomer").val('');
            $("#txtItemRefno").val('');
            $("#TxtSupplierRefNo").val('');
            $("#txtSupplierName").val('');
            $("#textItemName").val('');
            $("#txtDoctRefno").val('');
            $("#txtDoctor").val('');
        }
        function txtCustRefno_onBlur() {
            $("#txtCustRefno").val('');
        }
        function txtDoctRefno_onBlur() {
            $("#txtDoctRefno").val('');
        }
        function txtItemRefno_onBlur() {
            $("#txtItemRefno").val('');
        }
        function TxtSupplierRefNo_onBlur() {
            $("#TxtSupplierRefNo").val('');
        }
        function process(date) {
            var parts = date.split("/");
            return new Date(parts[2], parts[1] - 1, parts[0]);
        }
        function ShowDate(Type) {
            var toDate = new Date();
            if (Type == '') {
                $("#txtFromDate").val(toDate.format("dd/MM/yyyy"));
                $("#txtToDate").val(toDate.format("dd/MM/yyyy"));
            }
            else {
                var year = new Date().getFullYear();
                var newyaer = "01/04/" + year;
                // alert(newyaer);
                var currentdate = toDate.format("dd/MM/yyyy");

                if (process(currentdate) > process(newyaer)) {
                    $("#txtFromDate").val(newyaer);
                }
                else {
                    year = parseInt(year) - parseInt(1);
                    var newyaer = "01/04/" + year;
                    $("#txtFromDate").val(newyaer);
                }
              
                // alert(curdate);

                $("#txtToDate").val(toDate.format("dd/MM/yyyy"));
                $("#txtAsOnDate").val(toDate.format("dd/MM/yyyy"));
                $("#txtPassDate").val(toDate.format("dd/MM/yyyy HH:mm:ss"));
                $("#txtCurrentDate").val(toDate.format("dd/MM/yyyy"));
                $('#ddlStore').val('0');

                $('#ddlCategoryFirst').val('0');
                $('#ddlItems').val('0');
                $("#txtToDate").val(toDate.format("dd/MM/yyyy"));
            }
        }


        function popUp() {
            //   alert($("#hfReportType").val());
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
        function BlurCategory() {
            $("#btnCategoryBlurTrigger").click();
            //alert('a');
        }
        function VEnable() {

            var status = $('#ddlQuantityCriteria').find("option:selected").val();
            if (parseInt(status) > 0) {
                ValidatorEnable(document.getElementById("rfvQty"), true);
                $('#txtQauntity').show();
                $('#txtQauntity').focus();
            }
            else {
                ValidatorEnable(document.getElementById("rfvQty"), false);
                $('#txtQauntity').val('');
                $('#txtQauntity').hide();
            }
        }
        function ValuesRequiredenable() {

            var status = $('#ddlValueCriteria').find("option:selected").val();
            if (parseInt(status) > 0) {
                ValidatorEnable(document.getElementById("RequiredFieldValidator1"), true);
                $('#txtValues').show();
                $('#txtValues').focus();
            }
            else {
                ValidatorEnable(document.getElementById("RequiredFieldValidator1"), false);
                $('#txtValues').val('');
                $('#txtValues').hide();
            }
        }
        function pageLoad() {
            if ($('#txtQauntity').length > 0) {
                VEnable();
            }
            if ($('#txtValues').length > 0) {
                ValuesRequiredenable();
            }
        }

        function popUpSupplierNameSelect() {

            try {

                var ItemName = $("#txtSupplierName").val();

                if (ItemName.length > 0) {
                    $("#ImageButton3").click();
                }
                else {

                }

            }
            catch (e) {
                alert(e);
            }

        }
        function popUpItemSelect() {
            try {

                var ItemName = $("#textItemName").val();

                if (ItemName.length > 0) {
                    $("#ImageItemPopup").click();
                }
                else {
                }
            }
            catch (e) {
                alert(e);
            }
        }
        function Validate() {
            try {

                if ($('#hfSubModuleName').val() == "TFRODR") {
                    var valFromStore = document.getElementById('rfvFromStore');
                    ValidatorEnable(valFromStore, false);
                    var valToStore = document.getElementById('rfvToStore');
                    ValidatorEnable(valToStore, false);
                }
                else {
                    var valFromStore = document.getElementById('rfvFromStore');
                    ValidatorEnable(valFromStore, false);
                    var valToStore = document.getElementById('rfvToStore');
                    ValidatorEnable(valToStore, false);
                }
            }
            catch (e) {
                //alert(e);
            }
        }

    </script>


</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="form1" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" AsyncPostBackTimeout="20000" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div class="btn_bg5">
                    <div class="heading2">
                        <div class="user">Reports</div>

                        <asp:Panel ID="divMenuButtons" runat="server">
                            <div style="float: right; margin-top: -10px">
                            </div>
                        </asp:Panel>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg4">
                            <div id="divAdd" runat="server">
                                <div class="divL" style="width: 250px;">
                                    <ul class="contentReportul" style="width: 250px; border-right: 1px solid #e4e4e4; margin-top: 38px; padding-bottom: 11px;">
                                        <asp:TreeView ID="tvSettings" runat="server" CollapseImageUrl="~/images/down2.png" ExpandImageUrl="~/images/side2.png" LeafNodeStyle-ImageUrl="~/images/side2.png" OnSelectedNodeChanged="tvSettings_SelectedNodeChanged" OnTreeNodeDataBound="tvSettings_TreeNodeDataBound" ExpandDepth="0">
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
                                            <br />
                                            <br />
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
                                                        <%-- <asp:CompareValidator ID="cmpCurrentDate" runat="server" ErrorMessage="Date cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtToDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>--%>
                                                        <%--   <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="cmpCurrentDate">
                                                        </cc:ValidatorCalloutExtender>--%>
                                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtToDate" Format="dd/MM/yyyy">
                                                        </cc:CalendarExtender>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divSdDate" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label25" runat="server" Text="Schedule Upto" CssClass="label"></asp:Label>
                                                        <asp:TextBox ID="txtsddate" runat="server" CssClass="textbox" TabIndex="2"></asp:TextBox>
                                                        <asp:CompareValidator ID="cmpCurrentDate1" runat="server" ErrorMessage="Date cannot be greater than current date" ControlToCompare="txtCurrentDate" ControlToValidate="txtsddate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="cmpCurrentDate1">
                                                        </cc:ValidatorCalloutExtender>
                                                        <cc:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True" TargetControlID="txtsddate" Format="dd/MM/yyyy">
                                                        </cc:CalendarExtender>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divcatgry" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label1" runat="server" Text="Category" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCategoryFirst" runat="server" CssClass="dropdown" TabIndex="3" AutoPostBack="true" OnSelectedIndexChanged="btnCategoryBlurTrigger_Click">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divBin" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label34" runat="server" Text="Bin" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlBin" runat="server" CssClass="dropdown" TabIndex="3" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divAsOnDate" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label8" runat="server" Text="As On Date" CssClass="label"></asp:Label>
                                                        <asp:TextBox ID="txtAsOnDate" runat="server" CssClass="textbox" TabIndex="4"></asp:TextBox>
                                                        <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtAsOnDate" Format="dd/MM/yyyy">
                                                        </cc:CalendarExtender>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divStore" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label6" runat="server" Text="Store" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlStore" runat="server" CssClass="dropdown" TabIndex="5" OnSelectedIndexChanged="ddlStore_SelectedIndexChanged" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divCounter" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label19" runat="server" Text="Counter" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCounter" runat="server" CssClass="dropdown" TabIndex="6" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divShift" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label20" runat="server" Text="Shift" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlShift" runat="server" CssClass="dropdown" TabIndex="7">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divItems" style="display: none">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label5" runat="server" Text="Items" CssClass="label"></asp:Label>
                                                        <asp:TextBox ID="txtItemRefno" runat="server" AutoPostBack="false" CssClass="textboxPopup" TabIndex="8" Style="display: none;" onblur="txtItemRefno_onBlur();" onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divItemsPopUp" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label26" runat="server" CssClass="label" Text="Item"></asp:Label>
                                                        <asp:TextBox ID="textItemName" runat="server" AutoPostBack="false" CssClass="textboxPopup" TabIndex="130" onchange="txtItemRefno_onBlur();" onBlur="popUpItemSelect();"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageItemPopup" runat="server" OnClick="ImageItemPopup_Click" TabIndex="1" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 3px 0px; position: absolute" />
                                                    </li>

                                                </ul>
                                            </div>
                                            <div id="divuser" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label10" runat="server" Text="User" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlUser" runat="server" CssClass="dropdown" TabIndex="7">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divSaleMode" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label21" runat="server" Text="Payment Mode" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlSaleMode" runat="server" CssClass="dropdown" TabIndex="8">
                                                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="1">Cash</asp:ListItem>
                                                            <asp:ListItem Value="6">Credit Sale</asp:ListItem>
                                                            <asp:ListItem Value="2">Cheque</asp:ListItem>
                                                            <asp:ListItem Value="3">Debit Card</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                             <div id="divReportMethod" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label36" runat="server" Text="Report Method" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlReportMethod" runat="server" CssClass="dropdown" TabIndex="8">
                                                            <asp:ListItem Selected="True" Value="0">Bill Wise</asp:ListItem>
                                                            <asp:ListItem Value="1">Date Wise</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divPaymentType" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label22" runat="server" Text="Payment Type" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlPaymentType" runat="server" CssClass="dropdown" TabIndex="8">
                                                            <asp:ListItem Selected="True" Value="0">All</asp:ListItem>
                                                            <asp:ListItem Value="1">Cash</asp:ListItem>
                                                            <asp:ListItem Value="2">Credit</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="divEmployee" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label11" runat="server" Text="Sales man" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlEmployee" runat="server" CssClass="dropdown" TabIndex="8">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divIssueMode" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label9" runat="server" Text="Issue Mode" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlIssueMode" CssClass="dropdown" runat="server" TabIndex="9" AutoPostBack="True" ToolTip="Issue Mode">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divStoreFromTo" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label4" runat="server" Text="From Store" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlFromStore" runat="server" CssClass="dropdown" TabIndex="10">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvFromStore" runat="server" Enabled="false" ErrorMessage="From Store Required" InitialValue="0" ControlToValidate="ddlFromStore" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="vceFromStore" runat="server" Enabled="True" TargetControlID="rfvFromStore">
                                                        </cc:ValidatorCalloutExtender>
                                                    </li>
                                                </ul>
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label7" runat="server" Text="To Store" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlToStore" runat="server" CssClass="dropdown" TabIndex="11">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvToStore" runat="server" Enabled="false" ErrorMessage="To Store Required" InitialValue="0" ControlToValidate="ddlToStore" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="vceToStore" runat="server" Enabled="True" TargetControlID="rfvToStore">
                                                        </cc:ValidatorCalloutExtender>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divSupplier" style="display: none;">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label12" runat="server" Text="Supplier" CssClass="label"></asp:Label>
                                                        <asp:TextBox ID="TxtSupplierRefNo" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="8" Style="display: none;" onblur="TxtSupplierRefNo_onBlur();" onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divSupplierpopup" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label61" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                                        <asp:TextBox ID="txtSupplierName" runat="server" AutoPostBack="false" CssClass="textboxPopup" TabIndex="12" onchange="TxtSupplierRefNo_onBlur();" onBlur="popUpSupplierNameSelect();"></asp:TextBox>
                                                        <asp:ImageButton ID="ImageButton3" runat="server" OnClick="ImageButton3_Click" TabIndex="12" OnClientClick="return true;" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 3px 0px; position: absolute" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator32" runat="server" ErrorMessage="Select a Supplier" ControlToValidate="txtSupplierName" Display="None" SetFocusOnError="True" ValidationGroup="PopupSupplier"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender59" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator32">
                                                        </cc:ValidatorCalloutExtender>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divCusCombo" style="display: none;">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label13" runat="server" Text="Customer" CssClass="label"></asp:Label>
                                                        <asp:TextBox ID="txtCustRefno" runat="server" AutoPostBack="false" CssClass="textbox"
                                                            TabIndex="8" Style="display: none;" onblur="txtCustRefno_onBlur();"
                                                            onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divCustomer" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label class="label" ID="Label23" runat="server" Width="135px" Text="Customer"></asp:Label>
                                                        <asp:TextBox ID="txtCustomer" runat="server" CssClass="textboxPopup" TabIndex="130"
                                                            onchange="txtCustRefno_onBlur();"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnCustomer" runat="server" OnClick="imgbtnCustomer_Click"
                                                            CausesValidation="false" OnClientClick="return true;" TabIndex="9"
                                                            ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 3px 0px; position: absolute" />
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="divCustomer2" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label15" runat="server" Text="Customer" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCustomer2" runat="server" CssClass="dropdownMandatory" TabIndex="13">
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvCustomer" runat="server" ErrorMessage="Customer Required" ControlToValidate="ddlCustomer2" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="calloutCustomer" runat="server" Enabled="True" TargetControlID="rfvCustomer">
                                                        </cc:ValidatorCalloutExtender>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divDocRef" style="display: none;">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label33" runat="server" Text="Customer" CssClass="label"></asp:Label>
                                                        <asp:TextBox ID="txtDoctRefno" runat="server" AutoPostBack="false" CssClass="textbox" TabIndex="8" Style="display: none;" onblur="txtDoctRefno_onBlur();" onKeypress="CheckInteger(event);" Width="40px"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divDoctor" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label class="label" ID="Label32" runat="server" Width="135px" Text="Doctor"></asp:Label>
                                                        <asp:TextBox ID="txtDoctor" runat="server" CssClass="textboxPopup" TabIndex="130" onchange="txtDoctRefno_onBlur();"></asp:TextBox>
                                                        <asp:ImageButton ID="imgbtnDoctor" runat="server" OnClick="imgbtnDoctor_Click" CausesValidation="false" OnClientClick="return true;" TabIndex="9" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 4px 0px 3px 0px; position: absolute" />
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divNoSales" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label16" runat="server" Text="No Of Sales(<=)" CssClass="label"></asp:Label>
                                                        <asp:TextBox ID="txtNoSales" runat="server" CssClass="textbox" TabIndex="14"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvNoSales" runat="server" ErrorMessage="No of sales Required" ControlToValidate="txtNoSales" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="rfvNoSales">
                                                        </cc:ValidatorCalloutExtender>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="divCustomerGrp" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label17" runat="server" Text="Customer Group" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlCustomerGrp" runat="server" CssClass="dropdown" TabIndex="13">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="divQuantitySearch" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label14" runat="server" Text="Quantity Criteria" CssClass="label"></asp:Label>

                                                        <asp:DropDownList ID="ddlQuantityCriteria" onchange="VEnable();" runat="server"
                                                            CssClass="dropdown" TabIndex="13" Width="80px">
                                                        </asp:DropDownList>
                                                        <asp:TextBox ID="txtQauntity" CssClass="textboxSmall" Style="width: 100px; Background-color: #FCFDD6; display: none; text-align: right" onkeypress="CheckNumWithDecimal(event);" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="rfvQty" runat="server" Enabled="false" ErrorMessage="Quantity Required" ControlToValidate="txtQauntity" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="rfvQty">
                                                        </cc:ValidatorCalloutExtender>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divValuesSearch" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label27" runat="server" Text="Values Criteria" CssClass="label"></asp:Label>
                                                    </li>
                                                </ul>

                                                <ul style="margin-left: 0px;">
                                                    <li>
                                                        <asp:DropDownList ID="ddlValueCriteria" onchange="ValuesRequiredenable();" runat="server" CssClass="dropdown" Style="width: 100px" TabIndex="13"></asp:DropDownList>
                                                        <asp:TextBox ID="txtValues" CssClass="textboxSmall" Style="width: 100px; Background-color: #FCFDD6; display: none; text-align: right" onkeypress="CheckNumWithDecimal(event);" runat="server"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Enabled="false" ErrorMessage="Value Required" ControlToValidate="txtValues" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                                        </cc:ValidatorCalloutExtender>
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="divBillNo" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label18" runat="server" Text="Bill No" CssClass="label"></asp:Label>
                                                    </li>
                                                </ul>
                                                <ul>
                                                    <li>
                                                        <asp:TextBox ID="txtBillNo" CssClass="textbox" runat="server"></asp:TextBox>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divManufacturer" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label28" runat="server" Text="Manufacturer" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlManufacturer" runat="server" CssClass="dropdown" TabIndex="8">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divSchedule" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label29" runat="server" Text="Schedule" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlSchedule" runat="server" CssClass="dropdown" TabIndex="8">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divGeneric" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label30" runat="server" Text="Generic" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlGeneric" runat="server" CssClass="dropdown" TabIndex="8">
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divLevel" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label31" runat="server" Text="Level" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlLevel" runat="server" CssClass="dropdown" TabIndex="12">
                                                            <asp:ListItem Selected="True" Value="0">Select</asp:ListItem>
                                                            <asp:ListItem Value="1">Minimum Level</asp:ListItem>
                                                            <asp:ListItem Value="2">Maximum Level</asp:ListItem>
                                                            <asp:ListItem Value="3">Danger Level</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </li>
                                                </ul>
                                            </div>
                                            <div id="divExpiry" runat="server">
                                                <ul class="contentReportul">
                                                    <li><asp:Label ID="Label35" runat="server" Text="Without Expiry" CssClass="label"></asp:Label>
                                                        <asp:CheckBox ID="chkWithoutExpiry" runat="server"   />
                                                         
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
                                            <div id="DivApproved" runat="server">
                                                <ul class="contentReportul">
                                                    <li>
                                                        <asp:Label ID="Label37" runat="server" Text="Status" CssClass="label"></asp:Label>
                                                        <asp:DropDownList ID="ddlApproved" runat="server" CssClass="dropdown" TabIndex="12">
                                                            <asp:ListItem Selected="True" Value="1">Authorised</asp:ListItem>
                                                            <asp:ListItem  Value="0">Unauthorised</asp:ListItem>

                                                        </asp:DropDownList>
                                                         
                                                    </li>
                                                </ul>
                                            </div>

                                            <div id="divButtons" runat="server">
                                                <ul class="contentReportul">
                                                    <li class="btnMargin">
                                                        <asp:HiddenField ID="hfSubModuleName" runat="server" />
                                                        <asp:HiddenField ID="hfINVSubModule" runat="server" />
                                                        <asp:HiddenField ID="hfdate" runat="server" ClientIDMode="Static" />
                                                        <asp:HiddenField ID="hfUser" runat="server" />
                                                        <asp:HiddenField ID="hfClient" runat="server" />
                                                        <asp:Button ID="btnSubmit" runat="server" Text="View" CssClass="button" meta:resourcekey="btnSubmit" TabIndex="14" ValidationGroup="submit" OnClientClick="Validate();" OnClick="btnSubmit_Click" />
                                                        <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="button" CausesValidation="false" meta:resourcekey="btnClear" OnClientClick="ClearCustomer();" OnClick="btnClear_Click" TabIndex="15" />
                                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                                        <asp:HiddenField ID="hfIsAcessable" runat="server" />
                                                        <asp:Button ID="btnCategoryBlurTrigger" Text="text" runat="server" Style="display: none;" OnClick="btnCategoryBlurTrigger_Click" />
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
                                                <asp:HiddenField ID="hfReportName" runat="server" />
                                            </li>
                                            <li style="width: 450px; float: left;">
                                                <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>
                                            </li>
                                    </ul>
                    </div>
                    </div>
                        </div>
                    </div>
                    <asp:Button ID="btnShow" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                    <asp:Panel ID="divPopup" runat="server" Style="display: none;" CssClass="DivStyle">
                        <div id="divheader" runat="server" class="PopupHeader">
                            <asp:Label ID="lblPopupHeader" runat="server" Text="Account Head" CssClass="lblPopupHeader"></asp:Label>
                            <asp:ImageButton ID="btnClose" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                        </div>
                        <uc1:CommonPopup ID="CommonPopup1" runat="server" />
                    </asp:Panel>
                    <cc:ModalPopupExtender ID="mpCommon" runat="server" PopupControlID="divPopup" TargetControlID="btnShow"
                        CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divheader">
                    </cc:ModalPopupExtender>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

    </form>
</body>

</html>
