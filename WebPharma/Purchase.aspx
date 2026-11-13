  <%@ Page Language="C#" AutoEventWireup="true" CodeFile="Purchase.aspx.cs" Inherits="Purchase" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Purchase::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script src="js/Navigation.js"></script>
    <script type="text/javascript">

        function ClearAdd() {
            submit = 0;
            // $("[id*='txt']").not("txtDate").val('');
            $("#txtRefNo").val('');

            $("#ddlMaterialReceipt").val('0');
            $("#txtRefNo").val('');
            $("#ddlSupplier").val('0');
            $("#ddlStore").val('0');
            $("#txtDate").val('');

            var d = new Date();
            $("#txtDate").val(d.format('dd/MM/yyyy'));
            $("#txtInvoiceAmount").val('');
            $("#txtExpense").val('');
            $("#txtCost").val('');
            $("#txtRemark").val("");
            $("#cbxIsCredit").prop("checked", false);
            $("#hfPurchse").val('');
            $("#txtFromDate,#txtTodate").val(d.format('dd/MM/yyyy'));
            $("#txtExpiryDate").val(d.format("dd/MM/yyyy"));
            $("[id*='ddl']").val('0');
            $("#ddlPurchaseOrder").val('select');
            var dtnow = new Date();
            $("#hfDate").val(dtnow.format("dd/MM/yyyy"));
            var dt = new Date();
            $("#hdfCurrentDate").val(dt);
            ItemClear();
            ShowPurchaseItems();
            DisableTextBox();
            //  return false;
        }
        //$("#txtInvoiceAmount,#txtExpense,#txtCost").val('0');
        function ItemClear() {

            $("#ddlItem").val("0");
            $("#txtItemShortName").val("");
            $("#ddlUnit").val("0");
            $("#txtBatch").val("");
            $("#txtWholeSale").val("0");
            $("#txtQuantity").val("0");
            $("#txtPurchaseRate").val("0");
            $("#txtActualCost").val("0");
            $("#txtSalesPrice").val("0");
            $("#txtOfferPrice").val("0");
            var toDate = new Date();
            $("#txtExpiryDate").val(toDate.format("dd/MM/yyyy"));


        }




        var submit = 0;
        function disablebutn() {
            try {

                window.setTimeout('delaysubmit()', 50);
            }
            catch (e) {
                alert(e);
            }
        }

        function releasebtn() {


            $("#btnSave").prop('disabled', false);

        }
        function delaysubmit() {
            try {
                if (submit == 1) {
                    return false;

                }

                $("#btnSave").prop('disabled', true);

                window.setTimeout('releasebtn()', 3000);
                submit = 1;
            }
            catch (e) {
                alert(e);
            }
        }

        function CheckKeys() {
            alert(e.keyCode);
        }

        function ClearSearch() {
            $("#ddlSearchSupplier ").val("0");
            $("#ddlSearchStore").val("0");
            var d = new Date();
            $("#txtFromDate,#txtTodate").val(d.format('dd/MM/yyyy'));

        }
        function ShowAddDiv() {
            //alert('Show Add')
            //$("#divView").hide();
            //$("#divAdd").show();

        }
        function ShowPurchaseItems() {
            try {
                $("#divMaterialPurchase").hide();
                $("#divItemPurchase").show();

                //CalculateTotalItems();
            }
            catch (e) {
                alert(e);
            }
        }
        function ShowPurchaseOrderItems() {
            try {

                $("#divMaterialPurchase").show();
                $("#divItemPurchase").hide();
                //CalculateTotal();
            }
            catch (e) {
                alert(e);
            }
        }
        function btnClick() {
            $("#btnHidden2").click();
            return false;
        }

        function buttonClick() {
            $("#btnHidden").click();
            return false;
        }

        ////function pageLoad() {
        ////    if ($("#gvMaterialItems tr td").length > 1) {
        ////        $("#btnAdd").show();
        ////    }
        ////    else {
        ////        $("#btnAdd").hide();
        ////    }

        // }
        function pageLoad() {
            AmountAlert();
            var ctrContainer = "form";
            setKeynavigation(ctrContainer);
            UpdateValidationGroups();
            watchDateField();

        }
        function KEyCode() {
            if (e.keyCode == 9 || e.keyCode == 13)
                return false;
            else
                return true;
        }
        function CalculateGrand() {
            CalculateTotal();
            CalculateTotalItems();
        }
        function CalculateTotal() {
            try {
                var sum = 0;
                $("#gvMatrerialReceipt tr").has('td').each(function () {

                    var s = $(this).find("td:eq(2)").html();

                    s = (s == '' || s == null ? 0 : s);
                    sum = parseFloat(sum) + parseFloat(s);
                    sum = Math.round(sum * 1000) / 1000;
                });

                $("#txtInvoiceAmount").val(sum);
            }
            catch (e) {
                alert(e);
            }
        }
        function CalculateTotalItems() {
            try {
                var sumi = 0;
                $("#gvItems tr").has('td').each(function () {

                    var a = $(this).find("td:eq(12)").html();

                    alert(a);
                    a = ((a == '' || a == null) ? 0 : a);
                    alert(a);
                    sumi = parseFloat(sumi) + parseFloat(a);
                    sumi = Math.round(sumi * 1000) / 1000;

                });

                $("#txtInvoiceAmount").val(sumi);
            }
            catch (e) {
                alert(e);
            }
        }

        function blockPostOnTab() {
            if (e.keyCode == 9)
                return false;

        }

        function AmountAlert() {
            if ($("#txtWholeSale").val() != "" && $("#txtPurchaseRate").val() != "") {

                var sp2 = $("#txtWholeSale").val() == "" ? "0" : $("#txtWholeSale").val();
                var SP1 = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();
                var PR = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                if (parseFloat(sp2) < parseFloat(PR) && parseFloat(SP1) < parseFloat(PR)) {
                    alertify.alert("Purchase Rate is greater than Sales Price !");
                }
            }
        }
        function FillterPurchaseorder() {
            $("#btnHiddenSupplierFilter").click();

        }
        function PopUp() {
            //alert($("#hdfSearchCriteria").val());
            var ss = "ExportToExcel.aspx?ExportType=PurchaseExport" + $("#hdfSearch").val();
            // alert(ss);
            // window.location = ss;
            window.open(ss);
            window.close();
        }
        function DisableTextBox() {

        }
        function EnableExport() {



        }
        function ShowipmportVeiw() {
            $("#divAdd").hide();
            $("#divView").hide();
            $("#divImport").show();
            DisableTextBox();
        }

        function uploadStartedabdc(sender, args) {

            var filename = args.get_fileName();
            var filext = filename.substring(filename.lastIndexOf(".") + 1).toLowerCase();

            if (filext == "xlsx" || filext == "xls") {
                return true;
            }
            else {
                var err = new Error();
                err.name = 'File upload error';
                err.message = 'Invalid file format selected';
                throw (err);
            }
        }

        function UploadCompleteadbc(sender, args) {
            var dt = new Date();
            $("#hdfCurrentDate").val(dt);
            $("#btnShowData").click();
        }



        function popUpStockSelect() {

            try {
                ///alert('a');
                var ItemName = $("#txtItemShortName").val();
                if (ItemName.length > 0) {
                    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName;

                    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                    ",statusbar=no,menubar=no,resizable=0,width=1000," +
                    "height=600,left = 200,top = 100");
                    popUpObj.focus();

                }




            }
            catch (e) {
                alert(e);
            }

        }
        function popUpStockSelectClick() {

            try {
                ///alert('a');
                var ItemName = $("#txtItemShortName").val();

                url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName;

                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                ",statusbar=no,menubar=no,resizable=0,width=1000," +
                "height=600,left = 200,top = 100");
                popUpObj.focus();






            }
            catch (e) {
                alert(e);
            }

        }
        // onkeydown = "javascript:EnterKeyFilter();"

        function enterpressalert(e, textarea) {
            //var code = (e.keyCode ? e.keyCode : e.which);

            //if (code == 13) { 
            //   alert(e.keyCode);

            //    return false;
            //}
        }
        // onKeyPress = "enterpressalert(event, this)"


        // =========================
        // DATE HELPERS
        // =========================

        function parseDate_DMY(str) {
            if (!str || str.trim() === '') return null;
            var parts = str.trim().split('/');
            if (parts.length !== 3) return null;
            var d = parseInt(parts[0], 10);
            var m = parseInt(parts[1], 10) - 1;
            var y = parseInt(parts[2], 10);
            if (isNaN(d) || isNaN(m) || isNaN(y)) return null;
            var dt = new Date(y, m, d, 0, 0, 0, 0);
            if (dt.getFullYear() !== y || dt.getMonth() !== m || dt.getDate() !== d)
                return null;
            return dt;
        }


        function getToday() {
            var t = new Date();
            return new Date(t.getFullYear(), t.getMonth(), t.getDate(), 0, 0, 0, 0);
        }

        function getFinYearStart() {
            var today = getToday();
            var yr = today.getFullYear();
            var finStart = new Date(yr, 3, 1, 0, 0, 0, 0); // April 1
            if (today < finStart) {
                finStart = new Date(yr - 1, 3, 1, 0, 0, 0, 0);
            }
            return finStart;
        }


        // =========================
        // DATE VALIDATION
        // =========================

        function ValidatePurchaseDate() {
            var dateStr = $("#txtDate").val();

            if (!dateStr || dateStr.trim() === '') {
                alertify.alert("Please enter a Purchase Date.");
                $("#txtDate").focus();
                return false;
            }

            var selectedDate = parseDate_DMY(dateStr);
            if (!selectedDate) {
                alertify.alert("Invalid date format. Please enter date as dd/MM/yyyy.");
                $("#txtDate").focus();
                return false;
            }

            var today = getToday();
            var finStart = getFinYearStart();

            // Rule 1 — No future dates
            if (selectedDate > today) {
                alertify.alert("Purchase date cannot be a future date.");
                $("#txtDate").val('');
                $("#txtDate").focus();
                return false;
            }

            // Rule 2 — No dates before current financial year
            if (selectedDate < finStart) {
                var fs = ('0' + finStart.getDate()).slice(-2) + '/'
                       + ('0' + (finStart.getMonth() + 1)).slice(-2) + '/'
                       + finStart.getFullYear();
                alertify.alert(
                    "Purchase date cannot be from a previous financial year.\n" +
                    "Earliest allowed date is " + fs + "."
                );
                $("#txtDate").val('');
                $("#txtDate").focus();
                return false;
            }

            return true;
        }

        // =========================
        // MAIN SUBMIT VALIDATION
        // =========================

        function ValidateBeforeSubmit() {

            // 1. Date validation — MUST be first
            if (!ValidatePurchaseDate()) {
                return false;
            }

            // 2. Item-wise grid check
            if ($('#<%= rdItemPurchase.ClientID %>').is(':checked')) {
        if ($("table[id*='gvItems'] tr td").length === 0) {
            alertify.alert('Please add at least one item before submitting.');
            return false;
        }
    }

        // 3. Material-wise grid check
    if ($('#<%= rdMaterialPurchase.ClientID %>').is(':checked')) {
            if ($("table[id*='gvMatrerialReceipt'] tr td").length === 0) {
                alertify.alert('Please add at least one Material Receipt before submitting.');
                return false;
            }
        }

        return true;
    }


    // =========================
    // OPTIONAL LOADER WRAPPER
    // =========================

        function ShowProgress() {
            if (!ValidateBeforeSubmit()) {
                return false;
            }
            if (Page_ClientValidate("submit")) {
                // loader logic if needed
            }
            return true;
        }
    // =========================
    // VALIDATION GROUP CONTROL
    // =========================


        function UpdateValidationGroups() {
            var isItemWise = $('#<%= rdItemPurchase.ClientID %>').is(':checked');

    var suppValidator = document.getElementById('<%= reqvalSupplier.ClientID %>');
    if (suppValidator) {
        ValidatorEnable(suppValidator, isItemWise);
    }
}

    // =========================
    // EVENTS (UpdatePanel safe)
    // =========================

        $(document).ready(function () {

            // Date validation on blur
            $(document).on('change', '[id$="txtDate"]', function () {
                if ($(this).val() !== '') {
                    ValidatePurchaseDate();
                }
            });

            $(document).on('input propertychange', '[id$="txtDate"]', function () {
                if ($(this).val() !== '') {
                    ValidatePurchaseDate();
                }
    });
});

    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
        UpdateValidationGroups();
    });

    function watchDateField() {
        var dateField = document.getElementById('<%= txtDate.ClientID %>');
            if (!dateField) return;

            var lastVal = dateField.value;

            // Check every 300ms if the value changed
            setInterval(function () {
                if (dateField.value !== lastVal) {
                    lastVal = dateField.value;
                    if (dateField.value !== '') {
                        ValidatePurchaseDate();
                    }
                }
            }, 300);
        }
    </script>


</head>
<body>
    <form id="frmpurchase" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>



        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>

                <div class="icon_bg">
                    <div class="search" style="width:670px; " >Purichase</div>
                    <div class="add">Add</div>
                    <div class="add_icon">
                        <asp:ImageButton ID="ImageButton1" ImageUrl="images/add1.png" Width="37" Height="38" runat="server" TabIndex="29" CausesValidation="false" OnClick="ImageButton1_Click" OnClientClick=" releasebtn();" />
                    </div>

                    <div class="search">View</div>
                    <div class="search_icon">
                        <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" CausesValidation="false" Width="37" Height="40" TabIndex="40" runat="server" OnClick="imgbtnView_Click" />
                    </div>

                    <div class="search">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Export</div>
                    <div class="search_icon">
                        <asp:ImageButton ID="imgbtnExport" ImageUrl="~/images/Export.png" ToolTip="Export" Width="37" CausesValidation="false" Height="38" runat="server" OnClientClick="PopUp();return false;" TabIndex="61" />
                        <asp:HiddenField ID="hdfSearch" runat="server" />
                    </div>
                    <div class="search">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Import</div>
                    <div class="search_icon">
                        <asp:ImageButton ID="imbtnImport" ImageUrl="~/images/IMPORTING.png" ToolTip="Import" CausesValidation="false" Width="37" Height="38" runat="server" OnClientClick="ShowipmportVeiw();" OnClick="imgbtnImport_Click" TabIndex="60" />
                    </div>
                </div>
                <%--  <div class="user_bg">
                    <div class="user1">Purchase</div>
                </div>--%>
                <div class="clear"></div>
                <div class="group_bg2" style="width: 980px;">
                    <div id="divAdd" runat="server" style="">
                        display: none;

                        <asp:HiddenField ID="hdfCurrentDate" runat="server" />
                        <asp:HiddenField ID="hfDate" runat="server" />
                        <%--  style="display:none;"--%>

                        <ul class="contentul" id="Container">
                            <div class="divL">

                                <li>
                                    <label class="label">Date</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtDate" TabIndex="1" AutoComplete="off" />
                                    <%--<cc:CalendarExtender ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>--%>
                                    <asp:RequiredFieldValidator ID="reqvalDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="reqvalDate">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <br class="clear" />
                                <li>
                                    <asp:Label ID="Label186" runat="server" CssClass="label" Text="Purchase Method"></asp:Label>
                                    <asp:RadioButton ID="rdItemPurchase" runat="server" Text="Item Wise " GroupName="a" AutoPostBack="true" TabIndex="2" OnCheckedChanged="rdItemPurchase_CheckedChanged" />
                                    <asp:RadioButton ID="rdMaterialPurchase" runat="server" Text="MaterialReceipt Wise" AutoPostBack="true" GroupName="a" TabIndex="3" OnCheckedChanged="rdMaterialPurchase_CheckedChanged" />
                                </li>
                                <%-- <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate> style="display:none;"  --%>
                                <li class="clear"></li>
                                <br class="clear" />

                                <li id="liPO" runat="server">
                                    <label class="label">Purchase Order</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlPurchaseOrder" TabIndex="4" AppendDataBoundItems="True" AutoPostBack="True" OnSelectedIndexChanged="ddlPurchaseOrder_SelectedIndexChanged1">
                                    </asp:DropDownList>
                                    <%-- onBlur="return btnClick();"--%>
                                    <asp:Button ID="btnHidden2" runat="server" Text="Button" CausesValidation="false" OnClick="btnHidden2_Click" Style="display: none;" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="Purchase order required" ControlToValidate="ddlPurchaseOrder" Display="None" SetFocusOnError="True" InitialValue="select" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                                <li id="liMR" runat="server">
                                    <label class="label">Material Receipt</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlMaterialReceipt" TabIndex="5" AutoPostBack="True" OnSelectedIndexChanged="ddlMaterialReceipt_SelectedIndexChanged1" onkeydown="return KEyCode();">
                                    </asp:DropDownList>
                                    <%-- onBlur="return buttonClick();" onkeydown="return KEyCode();" --%>
                                    <%--   <asp:RequiredFieldValidator ID="rfvMaterialReceipt" runat="server" ErrorMessage="Material Receipt required" ControlToValidate="ddlMaterialReceipt" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                     <cc:ValidatorCalloutExtender ID="vceMaterialReceipt" runat="server" Enabled="True" TargetControlID="rfvMaterialReceipt">
                                        </cc:ValidatorCalloutExtender>--%>
                                    <asp:Button ID="btnHidden" runat="server" Text="Hidden" OnClick="btnHidden_Click" CausesValidation="false" Style="display: none;" />
                                </li>


                            </div>
                            <div class="divR">
                                <li>
                                    <label class="label">Ref No</label>
                                    <asp:TextBox ID="txtRefNo" runat="server" CssClass="textboxMandatory" TabIndex="6" AutoComplete="off"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ref No required" ControlToValidate="txtRefNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                  
                                <br class="clear" />
                                <li>
                                    <label class="label">Supplier</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlSupplier" TabIndex="7" onBlur="FillterPurchaseorder();">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalSupplier" runat="server" ErrorMessage="Supplier required" ControlToValidate="ddlSupplier" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="reqvalSupplier">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:Button ID="btnHiddenSupplierFilter" runat="server" CausesValidation="false" Text="Hide" Style="display: none;" OnClick="btnHiddenSupplierFilter_Click" />
                                </li>
                                <br class="clear" />

                                <li>
                                    <label class="label">Store</label>
                                    <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="8">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalStore">
                                    </cc:ValidatorCalloutExtender>
                                </li>
                                <br class="clear" />
                                <li class="btnMargin">
                                    <asp:CheckBox ID="cbxIsCredit" runat="server" Text="Credit" TabIndex="9" />
                                </li>



                            </div>
                            <li class="clear"></li>
                            <div class="divSeparation"></div>
                            <%--direct Items--%>
                            <div id="divItemPurchase" runat="server" style="display: none;">
                                <div class="divL">

                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item" Style="padding-top: 7px;"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtItemShortName" MaxLength="20" AutoComplete="off" TabIndex="10" onblur="popUpStockSelect();" />
                                        <asp:Button ID="btnItem" runat="server" Text="Item" OnClick="btnItem_Click" Style="display: none;" />
                                        <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return popUpStockSelectClick();" ImageUrl="~/images/POS/icons_bg.jpg" Style="padding-left: 2px; margin-top: 0px; width: 30px; height: 25px;" />
                                    </li>
                                    <li style="display: none;">
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:DropDownList ID="ddlItem" runat="server" CssClass="dropdownMandatory" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label136" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                        <asp:DropDownList ID="ddlUnit" runat="server" CssClass="dropdownMandatory" TabIndex="11"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator36" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender152" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator36">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Batch"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtBatch" TabIndex="12" MaxLength="20" AutoComplete="off" />

                                    </li>
                                    <li style="display: none;">
                                        <asp:Label ID="Label15" runat="server" CssClass="label" Text="Offer Price"></asp:Label>
                                        <asp:TextBox ID="txtOfferPrice" CssClass="textbox" runat="server" TabIndex="-1" MaxLength="14" AutoComplete="off" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>

                                    <li>
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="Expiry date"></asp:Label>
                                        <asp:TextBox ID="txtExpiryDate" CssClass="textbox" TabIndex="14" runat="server" AutoComplete="off"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtDob_CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtExpiryDate">
                                        </cc:CalendarExtender>
                                    </li>


                                </div>
                                <div class="divR">
                                    <li>
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Text="Quantity"></asp:Label>
                                        <asp:TextBox ID="txtQuantity" CssClass="textbox" runat="server" TabIndex="15" MaxLength="10" Style="text-align: right;" AutoComplete="off"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Item required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" SetFocusOnError="true" ValidationGroup="add" MinimumValue="1" Type="Double"></asp:RangeValidator>

                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label22" runat="server" CssClass="label" Text="Purchase Rate"></asp:Label>
                                        <asp:TextBox ID="txtPurchaseRate" CssClass="textbox" Style="text-align: right;" runat="server" TabIndex="16" MaxLength="10" onchange="AmountAlert();" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>

                                        <asp:RangeValidator ID="RangeValidator5" runat="server" ControlToValidate="txtPurchaseRate" SetFocusOnError="true" ErrorMessage="Enter a valid PurchaseRate" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RangeValidator5">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="PurchaseRate required" ControlToValidate="txtPurchaseRate" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="display: none;">
                                        <asp:Label ID="Label10" runat="server" CssClass="label" Text="Actual Cost"></asp:Label>
                                        <asp:TextBox ID="txtActualCost" Style="text-align: right;" CssClass="textbox" runat="server" TabIndex="17" MaxLength="14" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" Text="Sales Price 1"></asp:Label>
                                        <asp:TextBox ID="txtSalesPrice" Style="text-align: right;" CssClass="textbox" runat="server" TabIndex="18" MaxLength="14" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off"></asp:TextBox>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Sales Price 2"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textbox" ID="txtWholeSale" Style="text-align: right;" TabIndex="19" MaxLength="12" onchange="AmountAlert();" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />

                                    </li>



                                </div>
                                <li class="clear"></li>
                                <li class="btnMargin" style="padding-left: 135px;">
                                    <asp:Button Text="Add" ID="btnAddItems" runat="server" OnClick="btnAddItems_Click" TabIndex="20" ValidationGroup="add" CssClass="button" />
                                    <asp:Button Text="Clear" ID="btnClearItems" runat="server" CssClass="button" TabIndex="21" OnClientClick="ItemClear();return false;" />
                                </li>
                                <div class="clear"></div>
                                <li>
                                    <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="940px"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items" OnRowDeleting="gvItems_RowDeleting" PageSize="100">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />

                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Item" DataField="Item" />
                                            <asp:BoundField HeaderText="Fk_Item" DataField="Fk_Items" Visible="false" />
                                            <asp:BoundField HeaderText="Unit" DataField="Units" />
                                            <asp:BoundField HeaderText="Fk_Unit" DataField="Fk_Unit" Visible="false" />
                                            <asp:BoundField HeaderText="Batch No" DataField="BatchNo" />
                                            <asp:BoundField HeaderText="Expiry Date" DataField="ExpiryDate" />
                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Right" />

                                            <asp:BoundField HeaderText="Purchase Rate" DataField="PurchaseRate" ItemStyle-HorizontalAlign="Right" />
                                            <%-- <asp:BoundField HeaderText="Actual Cost" DataField="ActualCost" ItemStyle-HorizontalAlign="Right"  />--%>

                                            <%-- <asp:BoundField HeaderText="Offer Price" DataField="OfferPrice"  />--%>
                                            <asp:BoundField HeaderText="Sales Price 1" DataField="SalesPrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Sale Price 2" DataField="WholeSalePrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Total Amount" DataField="TotalAmount" ItemStyle-HorizontalAlign="Right" />

                                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                              
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton255" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                        <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />

                                    </asp:GridView>
                                </li>
                            </div>
                            <%--END direct Items--%>

                            <div class="clear"></div>

                            <li class="clear"></li>
                            <%--<li class="btnMargin">
                                    <asp:Button Text="Add" ID="btnAddMaterials" runat="server" TabIndex="18" ValidationGroup="add" CssClass="button" OnClick="btnAddMaterials_Click" />
                                    <asp:Button Text="Clear" ID="btnClearMaterials" runat="server" CssClass="button" TabIndex="19" OnClientClick="ItemClear();" />
                                </li>--%>


                            <div id="divMaterialPurchase" runat="server" style="display: none;">
                                <li class="clear"></li>




                                <li style="width: 1050px; height: auto; max-height: 140px; overflow-y: auto;">

                                    <asp:GridView ID="gvMaterialItems" runat="server" CssClass="grid" DataKeyNames="FK_Items"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                            <asp:BoundField HeaderText="Item" DataField="Item" />

                                            <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Batch No" DataField="BatchNo" />
                                            <asp:BoundField HeaderText="Purchase Rate" DataField="PurchaseRate" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField DataField="TotalAmt" HeaderText="Total Amount" ItemStyle-HorizontalAlign="Right" />
                                            <%--<asp:BoundField HeaderText="Actual Cost" DataField="ActualCost" ItemStyle-HorizontalAlign="Right" />--%>
                                            <asp:BoundField HeaderText="Sales Price 1" DataField="SalesPrice" ItemStyle-HorizontalAlign="Right" />
                                            <%--<asp:BoundField HeaderText="Offer Price" DataField="OfferPrice" />--%>
                                            <asp:BoundField HeaderText="Sale Price 2" DataField="WholeSalePrice" ItemStyle-HorizontalAlign="Right" />
                                            <%--   <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>--%>
                                        </Columns>
                                        <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                        <FooterStyle BackColor="#CCCCCC" />
                                        <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                        <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                        <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                        <SortedAscendingHeaderStyle BackColor="#808080" />
                                        <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                        <SortedDescendingHeaderStyle BackColor="#383838" />
                                    </asp:GridView>
                                </li>

                                <li class="clear"></li>
                                <br class="clear" />
                                <%--  </ContentTemplate>
                                        
                                    </asp:UpdatePanel>--%>


                                <li class="btnMargin">
                                    <asp:Button Text="Add" ID="btnAdd" runat="server" CssClass="button" OnClick="btnAdd_Click" ValidationGroup="Mate" TabIndex="22" />
                                    <%--<asp:Button Text="Clear" ID="btnClearAdd" runat="server" CssClass="button" TabIndex="9" CausesValidation="false" />--%>

                                    <asp:HiddenField ID="hdfPO" runat="server" />
                                    <asp:HiddenField ID="hdfMaterial" runat="server" />
                                </li>
                                <li class="clear"></li>


                                <asp:GridView ID="gvMatrerialReceipt" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" OnRowDeleting="gvMatrerialReceipt_RowDeleting">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" />


                                        <asp:BoundField HeaderText="Material Receipt No" DataField="MrNo" />


                                        <asp:BoundField DataField="PurchaseRate" HeaderText="Total Purchase Rate" ItemStyle-HorizontalAlign="Right" />


                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                            </div>

                            <div class="divL">

                                <label class="label">Remark</label>
                                <asp:TextBox runat="server" ID="txtRemark" TextMode="MultiLine" CssClass="multiline" MaxLength="500" TabIndex="23" AutoComplete="off" />

                                <li id="cbx" style="display: none;">
                                    <label class="label">Close Remark</label>
                                    <asp:TextBox ID="txtClosedRemark" runat="server" TextMode="MultiLine" MaxLength="500" TabIndex="1423" AutoComplete="off" />
                                </li>

                            </div>
                            <div class="divR">

                                <li class="clear"></li>
                                <li class="clear"></li>
                                <li>
                                    <label class="label">Invoice Amount</label>
                                    <asp:TextBox runat="server" CssClass="textbox" ID="txtInvoiceAmount" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" TabIndex="128" AutoComplete="off" />

                                    <cc:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server" Enabled="True" TargetControlID="txtInvoiceAmount" FilterType="Custom , Numbers" ValidChars=".">
                                    </cc:FilteredTextBoxExtender>
                                    <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtInvoiceAmount" ErrorMessage="Enter a valid Amount" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Invoice Amount required" ControlToValidate="txtInvoiceAmount" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                                <li style="display: none;">
                                    <label class="label">Expense</label>
                                    <asp:TextBox runat="server" CssClass="textbox" Style="text-align: right;" ID="txtExpense" TabIndex="24" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />

                                    <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtExpense" ErrorMessage="Enter a valid Expense" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                </li>
                                <li style="display: none;">
                                    <label class="label">Cost</label>
                                    <asp:TextBox runat="server" CssClass="textbox" Style="text-align: right;" ID="txtCost" TabIndex="25" onKeypress="CheckNumWithDecimal(event);" AutoComplete="off" />

                                    <asp:RangeValidator ID="RangeValidator4" runat="server" ControlToValidate="txtCost" ErrorMessage="Enter a valid Cost" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                </li>
                                <li class="clear"></li>
                                <li class="clear"></li>
                                <li class="clear"></li>
                                <li class="clear"></li>
                                <li class="btnMargin">
                                    <asp:HiddenField ID="hfPurchse" runat="server" />
                                    <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave" ValidationGroup="submit" OnClick="btnSave_Click" TabIndex="26"  OnClientClick="return ValidateBeforeSubmit();" />
                                    <asp:Button ID="btnClear" Text="Clear" runat="server" CssClass="button" OnClientClick="ClearAdd();" CausesValidation="false" TabIndex="27" OnClick="btnClear_Click1" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                </li>
                                <br class="clear" />
                                <br class="clear" />
                            </div>
                            <br class="clear" />
                        </ul>
                    </div>
                    <div class="clear"></div>
                    <div id="divView" runat="server" style="display: none;">



                        <ul class="contentul">
                            <div class="divL">
                                <li>
                                    <label class="label">Supplier</label>
                                    <asp:DropDownList ID="ddlSearchSupplier" runat="server" CssClass="dropdown" TabIndex="30"></asp:DropDownList>
                                </li>

                                <li>
                                    <label class="label">Store</label>
                                    <asp:DropDownList ID="ddlSearchStore" runat="server" CssClass="dropdown" TabIndex="31"></asp:DropDownList>

                                    <asp:HiddenField ID="hdfSearchCriteria" runat="server" />
                                </li>


                                <br class="clear" />
                                <li class="btnMargin">
                                    <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="34" />
                                    <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" TabIndex="35" />
                                </li>




                            </div>
                            <div class="divR">

                                <li>
                                    <label class="label">From Date</label>
                                    <asp:TextBox ID="txtFromDate" CssClass="textbox" runat="server" TabIndex="32" AutoComplete="off"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" TargetControlID="txtFromDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="From date should be less than to date" ControlToCompare="txtToDate" ControlToValidate="txtFromDate" Display="None" Font-Bold="True" Font-Size="Large" ForeColor="Red" Operator="LessThanEqual" SetFocusOnError="True" Type="Date" ValidationGroup="submit"></asp:CompareValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li>
                                    <label class="label">To Date</label>
                                    <asp:TextBox ID="txtTodate" CssClass="textbox" runat="server" TabIndex="33" AutoComplete="off"></asp:TextBox>
                                    <cc:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" TargetControlID="txtTodate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy">
                                    </cc:CalendarExtender>
                                </li>


                            </div>

                        </ul>
                        <br class="clear" />
                        <ul class="contentul">
                            <asp:GridView ID="gvPurchase" runat="server" CssClass="grid"
                                EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_Purchase" OnSelectedIndexChanging="gvPurchase_SelectedIndexChanging" OnRowDeleting="gvPurchase_RowDeleting">
                                <AlternatingRowStyle BackColor="#CCE0EB" />
                                <Columns>
                                    <asp:BoundField DataField="SLNO" HeaderText="SLNO" />
                                    <asp:BoundField DataField="PurBillNo" HeaderText="Bill No" />
                                    <asp:BoundField DataField="dates" HeaderText="Date" />
                                    <asp:BoundField DataField="RefNo" HeaderText="Ref No" />
                                    <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                    <asp:BoundField DataField="Store" HeaderText="Store" />


                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                        <ItemTemplate>
                                            <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Delete" ToolTip="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <FooterStyle BackColor="#CCCCCC" />
                                <HeaderStyle BackColor="#0C3A5E" Font-Bold="True" ForeColor="White" Height="30px" />
                                <PagerStyle BackColor="#249BB1" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                <SortedAscendingHeaderStyle BackColor="#808080" />
                                <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                <SortedDescendingHeaderStyle BackColor="#383838" />
                            </asp:GridView>
                            <div class="Pager">
                                <asp:Repeater ID="rptPager" runat="server">
                                    <ItemTemplate>

                                        <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" OnClick="Page_Changed" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </div>
                        </ul>
                    </div>
                    <div class="clear"></div>
                    <div id="divImport" runat="server" style="display: none;">
                        <br class="clear" />
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                            <ContentTemplate>

                                <cc:AsyncFileUpload ID="FileUploadExcel" runat="server" CssClass="imageUploaderField" OnClientUploadComplete="UploadCompleteadbc" OnClientUploadStarted="uploadStartedabdc" OnUploadedComplete="FileUploadExcel_UploadedComplete" />
                                <asp:Button ID="btnShowData" runat="server" OnClick="btnShowData_Click" CausesValidation="false"
                                    SkinID="button-process" Text="Show Data" UseSubmitBehavior="False" Style="display: none;"
                                    Width="70px" />
                                <div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                        <div align="center" style="position: absolute; z-index: auto; top: 574px; left: 370px; width: 254px;">
                            <%--     <asp:UpdateProgress ID="UpdateProgress1" AssociatedUpdatePanelID="UpdatePanel2" runat="server" DisplayAfter="20">
                                    <ProgressTemplate>
                                        <img alt="Please wait.." src="../../Images/ProgressBar/ProgressScore2.gif" style="width: 64px; height: 56px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>--%>
                        </div>
                    </div>
                    <div class="clear"></div>
                   <%-- <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                    <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>--%>


                </div>


            </ContentTemplate>
        </asp:UpdatePanel>






    </form>
    <p>
        &nbsp;
    </p>
</body>
</html>
