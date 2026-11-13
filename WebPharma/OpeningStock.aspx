<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpeningStock.aspx.cs" Inherits="OpeningStock" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>::Opening Stock::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script type="text/javascript">
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


            $("#btnSubmit").prop('disabled', false);

        }
        function delaysubmit() {
            try {
                if (submit == 1) {
                    return false;

                }

                $("#btnSubmit").prop('disabled', true);

                window.setTimeout('releasebtn()', 3000);
                submit = 1;
            }
            catch (e) {
                alert(e);
            }
        }

        function ClearAdd() {
            submit = 0;
            $("#<%#txtItemShortName.ClientID%>").val('');
            $("#<%#ddlItem.ClientID%>").val('0');
            $("#<%#ddlStore.ClientID%>").val('0');
            $("#<%#txtBatch.ClientID%>").val('');
            $("#<%#txtExpiryDate.ClientID%>").val('');
            $("#<%#txtBarCode.ClientID%>").val('');
            $("#<%#hdfOpenStockId.ClientID%>").val('');
            $("#<%#txtPurchaseRate.ClientID%>").val('0');
            $("#<%#txtPrivilagePrice.ClientID%>").val('0');
            $("#<%#txtMRP.ClientID%>").val('0');
            $("#<%#txtWholeSalePrice.ClientID%>").val('0');
            $("#<%#txtRetailPrice.ClientID%>").val('0');
            $("#<%#txtVat.ClientID%>").val('0');
            $("#<%#txtOpeningStock.ClientID%>").val('0');
            $('#GvPreviousPurchase').remove();
            var toDate = new Date();
            $("#txtExpiryDate").val(toDate.format("dd/MM/yyyy"));
        }
        function ClearSearch() {
            $("#<%#txtSearchItemName.ClientID%>").val('');
            $("#<%#txtSearchItemName.ClientID%>").focus();
        }
        function ShowAddDiv() {

            $("#<%#divAdd.ClientID%>").show();
            $("#<%#divView.ClientID%>").hide();
            $("#<%#imgPopUp.ClientID%>").focus();
        }

        //function ChangeValue() {
        //    if ($("#cbxIsBaseUnit").prop("checked")) {

        //        if ($("#ddlCategoryFirst :selected").val() != '0') {
        //            GetDefaultunit();
        //        }

        //    }

        //}

        //function GetDefaultunit() {
        //    var FK_CategoryFirst = $("#ddlCategoryFirst :selected").val();

        //    try {
        //        $.ajax({
        //            type: "POST",
        //            url: 'Textile.asmx/GetCategoryBaseUnit',
        //            data: "{FK_CategoryFirst :'" + FK_CategoryFirst + "'}",
        //            contentType: "application/json; charset=utf-8",
        //            dataType: "json",
        //            success: function (data) {
        //                if (data.d != '') {

        //                    var arr = (data.d).split('-');
        //                    if ($("#hdfOpenStockId").val() != '') {
        //                        if ($("#txtName").val() != arr[0]) {
        //                            if (alertify.confirm(data.d + "  has already been set  as  base unit under the category do you want to change it?")) {
        //                                $("#cbxIsBaseUnit").attr("checked", "checked");
        //                            }
        //                            else {
        //                                $("#cbxIsBaseUnit").prop("checked", false);
        //                            }
        //                        }
        //                    }
        //                    else {
        //                        if (alertify.confirm(data.d + "  has already been set  as  base unit under the category do you want to change it?")) {
        //                            $("#cbxIsBaseUnit").attr("checked", "checked");
        //                        }
        //                        else {
        //                            $("#cbxIsBaseUnit").prop("checked", false);
        //                        }
        //                    }

        //                }

        //            },
        //            error: function (error) {

        //            }
        //        });
        //    }
        //    catch (e) {
        //        alert(e);
        //    }
        //}
        function popUpStockSelectClick() {

            try {
                var ItemName = $("#txtItemShortName").val();
                url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val();
                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                ",statusbar=no,menubar=no,resizable=0,width=1015," +
                "height=650,left = 200,top = 100");
                popUpObj.focus();
            }
            catch (e) {
                alert(e);
            }

        }
        function popUpStockSelect() {

            try {
                $("#ddlItem").val('0');
                var Cat = $("#ddlCategory").val();

                var ItemName = $("#txtItemShortName").val();
                if (ItemName.length > 0) {
                    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val();

                    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                    ",statusbar=no,menubar=no,resizable=0,width=1015," +
                    "height=650,left = 200,top = 100");
                    popUpObj.focus();
                }
                else {


                }
            }
            catch (e) {
                alert(e);
            }

        }

    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmUnit" runat="server" autocomplete="off">

        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <div>
                    <div class="icon_bg">
                        <div class="search" style="width: 670px;">Opening Stock</div>
                        <div class="add">Add</div>
                        <div class="add_icon">
                            <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/add1.png" Width="37" Height="38" TabIndex="22" runat="server" OnClick="imgbtnAdd_Click" />
                        </div>
                        <div class="search">View</div>
                        <div class="search_icon">
                            <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37" Height="40" TabIndex="21" runat="server" OnClick="imgbtnView_Click" />
                        </div>
                    </div>
                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div id="divAdd" runat="server" style="display: none;">
                            <div class="divL">
                                <ul class="contentul">
                                    <li >
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory"  ID="txtItemShortName" onBlur="popUpStockSelect();" Width="130px" MaxLength="150" TabIndex="1"  /> 
                                        <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
                                        <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return  popUpStockSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding: 0px; margin: 0px 0px 0px 6px;position:absolute " />
                                         <asp:RequiredFieldValidator ID="rfvitem" runat="server" ErrorMessage="Select an Item" ControlToValidate="txtItemShortName" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="rfvitem"></cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="display:none;">
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:DropDownList ID="ddlItem" runat="server" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true"  CssClass="dropdownMandatory" ></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2"></cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="display:none;">
                                        <asp:Label ID="Label16" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                        <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="2" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender61" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator61"></cc:ValidatorCalloutExtender>
                                    </li>
                                    <li >
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Opening Stock"></asp:Label>
                                        <asp:TextBox ID="txtOpeningStock" CssClass="textboxMandatory" runat="server" Width="165px" TabIndex="3" MaxLength="24" Style="text-align: right; font-weight:bold;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                        <asp:RangeValidator ID="rvStock" runat="server" ControlToValidate="txtOpeningStock" ErrorMessage="Enter a valid Stock" Display="None" MaximumValue="1000000" ValidationGroup="submit" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="rvStock"></cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="rfvStock" runat="server" ErrorMessage="Opening Stock required" ControlToValidate="txtOpeningStock" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="rfvStock"></cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <label class="label">Store</label>
                                        <asp:DropDownList runat="server" CssClass="dropdownMandatory" ID="ddlStore" TabIndex="4">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="reqvalStore" runat="server" ErrorMessage="Store required" ControlToValidate="ddlStore" Display="None" SetFocusOnError="True" InitialValue="0" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="reqvalStore"></cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label2" runat="server"  CssClass="label" Text="Batch & Exp" Style=" font-weight:bold;"  ></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textbox"  TabIndex="5"  Width="75px"  ID="txtBatch" MaxLength="20" />
                                         <asp:TextBox ID="txtExpiryDate" CssClass="textbox"   Width="75px"  TabIndex="6" Style=" font-weight:bold;" runat="server"></asp:TextBox>
                                         <cc:MaskedEditExtender ID="MaskedEditExtender2" TargetControlID="txtExpiryDate" 
                                            Mask="99/99/9999"
                                            MessageValidatorTip="true" 
                                            OnFocusCssClass="MaskedEditFocus" 
                                            OnInvalidCssClass="MaskedEditError"
                                            MaskType="Date" 
                                            InputDirection="RightToLeft" 
                                            AcceptNegative="None" 
                                            DisplayMoney="None"
                                            AutoComplete="true"
                                            AutoCompleteValue="Date" 
                                           runat="server"></cc:MaskedEditExtender>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator178" runat="server" ValidationGroup="submit" Display="None" SetFocusOnError="True"
                                            ErrorMessage="Please enter date in dd/mm/yyyy format"   ForeColor="Red" Font-Bold="true"   ControlToValidate="txtExpiryDate"
                                            ValidationExpression="(0[1-9]|[12][0-9]|3[01])/(0[1-9]|1[012])/\d{4}"></asp:RegularExpressionValidator> 
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender58" runat="server" Enabled="True" TargetControlID="RegularExpressionValidator178">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                     <li>
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="BarCode"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textbox"  TabIndex="7"  Width="165px"  ID="txtBarCode" MaxLength="14" />
                                    </li>
                                    <li>
                                        <asp:Label ID="Label22" runat="server" CssClass="label" Text="Pur Rate"></asp:Label>
                                        <asp:TextBox ID="txtPurchaseRate" AutoComplete="off"   Width="165px" CssClass="textboxMandatory" runat="server" TabIndex="8" MaxLength="10" Style="text-align: right; font-weight:bold;" onKeypress="CheckNumWithDecimal(event);" onkeyup=" CalculateNetamountofItems(); CalculateProfitPercentage(); calculateVatAmnt();  "></asp:TextBox>
                                         <asp:RequiredFieldValidator ID="rfvpurRate" runat="server" ErrorMessage="Select an Item" ControlToValidate="txtPurchaseRate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="rfvpurRate"></cc:ValidatorCalloutExtender>

                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPurchaseRate" ErrorMessage="Enter a valid PurchaseRate" Display="None" MaximumValue="999999999999" ValidationGroup="submit" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                        </cc:ValidatorCalloutExtender>
                                       
                                    </li>
                                    
                                    
                                </ul>
                            </div>
                            <div class="divL">
                                <ul class="contentul">
                                    <li>
                                        <asp:Label ID="Label15" runat="server" CssClass="label" Text="MRP"></asp:Label>
                                        <asp:TextBox ID="txtMRP" CssClass="textboxMandatory" TabIndex="9" Width="165px" runat="server" MaxLength="11" Style="text-align: right; font-weight:bold;" onKeypress="CheckNumWithDecimal(event);" OnkeyUp="CalculaterRetailPrice(); CalculateProfitPercentage();   " ></asp:TextBox>
                                           <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="MRP required" ControlToValidate="txtMRP" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                        </cc:ValidatorCalloutExtender>
                                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtMRP" SetFocusOnError="true" ControlToCompare="txtPurchaseRate" Type="Double"  ValidationGroup="submit" Display="None" ErrorMessage=" MRP Should be Greater than Purchase rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label27" runat="server" CssClass="label" Text="Retail Price"></asp:Label>
                                        <asp:TextBox ID="txtRetailPrice" CssClass="textboxMandatory" Width="165px"  TabIndex="10" runat="server" MaxLength="11" Style="text-align: right; font-weight:bold;" onkeyup="CalculateProfitPerwhileRetail(); CalculateNetamountofItems();" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtRetailPrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtPurchaseRate" ValidationGroup="submit" Display="None" ErrorMessage="Retail Price rate Should be greater than or equal to Purchase Rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                        </cc:ValidatorCalloutExtender>
                                         <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtRetailPrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtMRP" ValidationGroup="submit" Display="None" ErrorMessage="Retail Price rate Should not  be greater than MRP" Operator="LessThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="CompareValidator5">
                                        </cc:ValidatorCalloutExtender>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ErrorMessage="Retail Price required" ControlToValidate="txtRetailPrice" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender32" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator19">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li >
                                        <asp:Label ID="Label10" runat="server" CssClass="label" Text="WholeSale Price"></asp:Label>
                                        <asp:TextBox ID="txtWholeSalePrice" CssClass="textboxMandatory" Width="165px"  TabIndex="11" runat="server" MaxLength="11" Style="text-align: right; font-weight:bold;" onkeyup="CalculatePercentagefromWholeSalePrice();" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtWholeSalePrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtPurchaseRate" ValidationGroup="submit" Display="None" ErrorMessage="Whole Sale Price  Should be greater than or equal to  Purchase Rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="CompareValidator3">
                                        </cc:ValidatorCalloutExtender>
                                         <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtWholeSalePrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtMRP"  ValidationGroup="submit" Display="None" ErrorMessage="Whole Sale Price  Should not  be greater than MRP" Operator="LessThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="CompareValidator6">
                                        </cc:ValidatorCalloutExtender>
                                      <asp:CompareValidator ID="CompareValidator9" runat="server" ControlToValidate="txtWholeSalePrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtRetailPrice"  ValidationGroup="submit" Display="None" ErrorMessage="Whole Sale Price  Should not  be less than Retail Price" Operator="LessThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender35" runat="server" Enabled="True" TargetControlID="CompareValidator9">
                                        </cc:ValidatorCalloutExtender>
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Whole Sale Price required" ControlToValidate="txtWholeSalePrice" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender31" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator18">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li >
                                        <asp:Label ID="Label28" runat="server" CssClass="label" Text="Card Price"></asp:Label>
                                        <asp:TextBox ID="txtPrivilagePrice" CssClass="textboxMandatory"  Width="165px" runat="server" TabIndex="12" MaxLength="11" Style="text-align: right; font-weight:bold;" onkeyup="CalculatePercentagefromCardPrice();" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtPrivilagePrice" SetFocusOnError="true"  ControlToCompare="txtPurchaseRate" Type="Double" ValidationGroup="submit" Display="None" ErrorMessage="Card Price rate Should be greater than or equal to  Purchase Rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="CompareValidator4">
                                        </cc:ValidatorCalloutExtender>
                                         <asp:CompareValidator ID="CompareValidator7" runat="server" ControlToValidate="txtPrivilagePrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtMRP"  ValidationGroup="submit" Display="None" ErrorMessage="Card Price rate Should not  be greater than MRP" Operator="LessThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender19" runat="server" Enabled="True" TargetControlID="CompareValidator7">
                                        </cc:ValidatorCalloutExtender>
                                           <asp:CompareValidator ID="CompareValidator10" runat="server" ControlToValidate="txtPrivilagePrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtRetailPrice"  ValidationGroup="submit" Display="None" ErrorMessage="Card Sale Price rate Should not  be less than Retail Price" Operator="LessThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender36" runat="server" Enabled="True" TargetControlID="CompareValidator10">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ErrorMessage="Privilage Price required" ControlToValidate="txtPrivilagePrice" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender30" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator17">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li >
                                        <asp:Label ID="Label31" runat="server" CssClass="label" Text="VAT %"></asp:Label>
                                        <asp:TextBox ID="txtVat" CssClass="textbox" runat="server" Width="165px" TabIndex="13" MaxLength="24" Style="text-align: right; font-weight:bold;" onKeypress="CheckNumWithDecimal(event);" Enabled="false" ></asp:TextBox>
                                        <%--<asp:RangeValidator ID="rvvat" runat="server" ControlToValidate="txtVat" ErrorMessage="Enter a valid Vat" Display="None" MaximumValue="100" ValidationGroup="submit" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="rvvat"></cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="rfvVat" runat="server" ErrorMessage="VAT % required" ControlToValidate="txtVat" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender29" runat="server" Enabled="True" TargetControlID="rfvVat"></cc:ValidatorCalloutExtender>--%>
                                    </li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSubmit" runat="server" CssClass="button" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" TabIndex="19" OnClientClick="disablebutn();" />
                                        <asp:Button ID="btnClear" runat="server" CssClass="button" Text="Clear" OnClientClick="ClearAdd(); return false;" TabIndex="20" />
                                        <asp:HiddenField ID="hdfOpenStockId" runat="server" />
                                        <asp:HiddenField ID="hfClient" runat="server" />
                                        <asp:HiddenField ID="hfUser" runat="server" />
                                        <asp:HiddenField ID="hfIsAdd" runat="server" />
                                        <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                        <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    </li>
                                </ul>
                            </div>
                            <li class="clear"></li>
                                <li class="clear"></li>
                            <div id="DivPreviousPurchase" runat="server" style="width: 940px; overflow: auto; scrollbar-face-color: blue; height: 150px;">
                                    <%--<li>Previous purchases 
                                     <asp:Label ID="lblCurrentStock" runat="server" CssClass="label" Style="float: right" Width="250px" Font-Bold="true" ForeColor="Red" Text="Current Stock=0"></asp:Label>
                                    </li>--%>


                                    <asp:GridView ID="GvPreviousPurchase" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px" Style="margin-left: 5px;"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>

                                            <asp:BoundField HeaderText="Fk_Item" DataField="Fk_Items" Visible="false" />
                                            <asp:BoundField HeaderText="BillNo" DataField="BillNo" />
                                            <asp:BoundField HeaderText="BillDate" DataField="BillDate" />

                                            <asp:BoundField HeaderText="Pur. Rate" DataField="PurchaseRate" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="MRP" DataField="MRP" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Pur.Vat" DataField="PurchaseVat" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Disc %" DataField="DiscountPer" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Disc Amt" DataField="DiscountAmount" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Profit %" DataField="ProfitPer" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="R.Price" DataField="RetailPrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="W.Price" DataField="WholeSalePrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="C.Price" DataField="CardPrice" ItemStyle-HorizontalAlign="Right" />
                                            <asp:BoundField HeaderText="Stock" DataField="Stock" ItemStyle-HorizontalAlign="left" />
                                            <asp:BoundField HeaderText="BatchNo" DataField="BatchNo" />
                                            <asp:BoundField HeaderText="ExpiryDate" DataField="ExpiryDate" Visible="false" />

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
                        </div>
                        <div id="divView" runat="server" style="display: none;">
                            <%--style="display: none;"--%>
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label6" runat="server" CssClass="label" Text="Name"></asp:Label>
                                        <asp:TextBox ID="txtSearchItemName" CssClass="textbox" runat="server" TabIndex="15"></asp:TextBox>
                                    </li>

                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" TabIndex="17" />
                                        <asp:Button ID="btnSearchClear" runat="server" CssClass="button" Text="Clear" TabIndex="18" OnClientClick="ClearSearch(); return false; " />
                                    </li>
                                </li>
                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvOpenStock" runat="server" CssClass="grid" OnSelectedIndexChanging="gvOpenStock_SelectedIndexChanging"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_OpeningStock" >
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Item" HeaderText="Item Name" />
                                        <asp:BoundField DataField="Store" HeaderText="Store Name" />
                                        <asp:BoundField DataField="BatchNo" HeaderText="Batch No" />
                                        <asp:BoundField DataField="ExpiryDate" HeaderText="Expiry Date"/>
                                        <asp:BoundField DataField="PurRate" HeaderText="Purchase Rate" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="MRP" HeaderText="MRP" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="RetailPrice" HeaderText="Retail Price" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="WholesalePrice" HeaderText="Wholesale Price" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="PrivilagePrice" HeaderText="Privilage Price" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="VAT" HeaderText="VAT"/>
                                        <asp:BoundField DataField="OpeningStock" HeaderText="Opening Stock"/>
                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select.png" Width="20px" Height="20px" />
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
                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <script src="js/toastr.js"></script>
    </form>
</body>
</html>
