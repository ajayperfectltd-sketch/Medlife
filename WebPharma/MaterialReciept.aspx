<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MaterialReciept.aspx.cs" Inherits="MaterialReciept" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>::Material Reciept::</title>
    <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/toastr.js"></script>
    <script src="js/alertify.min.js"></script>
    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />
    <script type="text/javascript">




        function ClearAdd() {

            var dt = new Date();
            $("#hfDate").val(dt.format("dd/MM/yyyy"));
            $("#ddlPurchaseOrder").val("0");
            $("#ddlSupplier").val("0");
            $("#ddlStore").val("0");

            //  $("#txtRefNo").val("");
            //$("#hfMaterialRecieptId").val("0");
            //var toDate = new Date();
            //$("#txtDate").val(toDate.format("dd/MM/yyyy"));       
            //  

            //  $('#ChkCredit').prop('checked', false);




            $("#txtBillNetAmount").val("0");


            ItemClear();

        }




        function ClearSearch() {
            $("#ddlSearchSupplier ").val("0");
            $("#ddlSearchStore").val("0");

        }


        function ItemClear() {
        
            $("#ddlItem").val("0");
            $("#ddlUnit").val("0");
            $("#txtBatch").val("");


           // $("#txtItemShortName").val("");
            $("#txtQuantity").val("0");
            $("#txtMRP").val("0");

            $("#txtPurchaseVat").val("0");
            $("#txtDiscountPer").val("0");
            $("#txtProfitPer").val("0");
            $("#txtRetailPrice").val("0");

            $("#txtWholeSalePrice").val("0");
            $("#txtPrivilagePrice").val("0");
            $("#txtVat").val("0");
            $("#txtActqty").val("0");
            $("#txtFree").val("0");
            $("#txtDiscountAmt").val("0");

            $("#txtPurchaseRate").val("0");



            var toDate = new Date();
            $("#txtExpiryDate").val(toDate.format("dd/MM/yyyy"));


        }
        function ShowAddDiv() {
            //ClearAdd();
            try {

                $("#divAdd").show();
                $("#divView").hide();

            }
            catch (e) {
                alert(e);
            }

        }
        function BlurPurchaseOrder() {
            $('#btnPurchaseOrderBlurTrigger').click();
        }
        function pageLoad() {

            var toDate = new Date();
            $("#txtBillDate").val(toDate.format("dd/MM/yyyy"));
            $("#txtInvoiceDate").val(toDate.format("dd/MM/yyyy"));

            alert( $("#txtItemShortName").val());

            CalculateTotal();
        }


        function popUpStockSelect() {

            try {
                ///alert('a');
                var ItemName = $("#txtItemShortName").val();
                if (ItemName.length > 0) {
                    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val();

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
                url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName + '&ctgry=' + $("#ddlCategory").val();


                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=yes,location=no" +
                                ",statusbar=no,menubar=no,resizable=0,width=1015," +
                                "height=650,left = 200,top = 100");

             
                popUpObj.focus();






            }
            catch (e) {
                alert(e);
            }

        }

        function SelectName() {
            var popup;
            popup = window.open("ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&ctgry=' + $("#ddlCategory").val(), "Popup", "width=1000,height=800");
            popup.focus();

            return false;
        }
        function AmountAlert() {
            if ($("#txtSalesPrice").val() != "" && $("#txtPurchaseRate").val() != "") {

                var sp2 = $("#txtWholeSale").val() == "" ? "0" : $("#txtWholeSale").val();

                var SP1 = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();

                var PR = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();
                if (parseFloat(sp2) < parseFloat(PR) && parseFloat(SP1) < parseFloat(PR)) {
                    alertify.alert("Purchase Rate is greater than Sales Price !");
                }
            }
        }

        function CalculateProfitPercentege() {

            if ($("#txtMRP").val() != "" && $("#txtDiscountAmt").val() != "" && $("#txtPurchaseRate").val() != "") {
                var mrp = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var disam = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                var purrate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();

                sum = parseFloat(sum) + parseFloat(s);

            }

        }
        function CalculateRetailPrice() {

            if ($("#txtMRP").val() != "" && $("#txtDiscountAmt").val() != "" && $("#txtPurchaseRate").val() != "") {
                var mrp = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
                var disam = $("#txtDiscountAmt").val() == "" ? "0" : $("#txtDiscountAmt").val();
                var purrate = $("#txtPurchaseRate").val() == "" ? "0" : $("#txtPurchaseRate").val();



            }

        }


        //function CalculateTotal() {
        //    try {
        //        var sum = 0;
        //        $("#gvItems tr").has('td').each(function () {

        //            var s = $(this).find("td:eq(7)").html();

        //            s = (s == '' || s == null ? 0 : s);
        //            sum = parseFloat(sum) + parseFloat(s);
        //           // sum = Math.round(sum * 1000) / 1000;
        //        });

        //        $("#txtInvoiceAmount").val(sum.toFixed(3));
        //    }
        //    catch (e) {
        //        alert(e);
        //    }
        //}
        //function CalculateAmt(event) {
        //    var e = (window.event) ? window.event : event;
        //    var id = e.target.id;

        //    var Pr = id.replace('gvItems_lblgvQuantity_', 'gvItems_Label26_');
        //    var totalAmt = (parseFloat($("#" + Pr).text() == "" || $("#" + Pr).text() == null ? "0" : $("#" + Pr).text()))*(parseFloat($("#" + id).val() == "" ? "0" : $("#" + id).val()))
        //    $("#" + id).parent().parent().find('td:eq(7)').html(totalAmt.toFixed(3));
        //    CalculateTotal();

        //}



    </script>
</head>
<body onkeydown="javascript:EnterKeyFilter();">
    <form id="frmMaterialReciept" runat="server" autocomplete="off">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                   <div class="btn_bg5"> 
                         <div class="heading2">
                    <div class="user">Material Reciept</div>
                             <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top: -10px">
                        <asp:ImageButton ID="ImageButton12" ImageUrl="images/ADD.png" Width="38"     Height="38" runat="server" OnClientClick=" ShowAddDiv(); return false; " />
                      <asp:ImageButton ID="imgbtnView" ImageUrl="images/view.png" Width="37"    Height="40" runat="server" OnClick="imgbtnView_Click" />    
                    </div>
                                 </asp:Panel>
                             </div>
                       

                 

                    <div class="clear"></div>
                    <div class="group_bg2" style="width: 980px;">
                          <div class="main_bg">  
                        <div id="divAdd" runat="server">
                            <ul class="contentul" id="Container">
                                <div class="divL" style="width: 325px;">

                                    <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                        <asp:DropDownList ID="ddlSupplier" runat="server" TabIndex="4" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Select a Supplier" ControlToValidate="ddlSupplier" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="RequiredFieldValidator3_ValidatorCalloutExtender" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator3">
                                        </cc:ValidatorCalloutExtender>

                                    </li>

                                    <li id="liPO" runat="server">
                                        <asp:Label ID="Label17" runat="server" CssClass="label" Text="Purchase order"></asp:Label>
                                        <asp:DropDownList ID="ddlPurchaseOrder" TabIndex="3" runat="server" onblur="BlurPurchaseOrder();" CssClass="dropdown"></asp:DropDownList>
                                    </li>

                                </div>

                                <div class="divL" style="width: 325px;">

                                    <li>
                                        <asp:Label ID="Label18" runat="server" CssClass="label" Text="Invoice No"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtInvoiceNo" TabIndex="1" MaxLength="10" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ErrorMessage="Required Invoice No" ControlToValidate="txtInvoiceNo" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator6">
                                        </cc:ValidatorCalloutExtender>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label19" runat="server" CssClass="label" Text="Invoice Date"></asp:Label>
                                        <asp:TextBox ID="txtInvoiceDate" CssClass="textboxMandatory" TabIndex="2" runat="server"></asp:TextBox>
                                        <cc:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtInvoiceDate">
                                        </cc:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ErrorMessage="Invoice Date required" ControlToValidate="txtInvoiceDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator7">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="display: none;">
                                        <asp:Label ID="Label20" runat="server" CssClass="label" Text="BillType"></asp:Label>
                                        <asp:DropDownList ID="ddlBillType" runat="server" TabIndex="5" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ErrorMessage="Select a BillType" ControlToValidate="ddlBillType" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator8">
                                        </cc:ValidatorCalloutExtender>
                                    </li>


                                    <li class="clear"></li>
                                    <li class="clear"></li>


                                </div>

                                <div style="float: left; width: 300px;">


                                    <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Width="100" Text="Store"></asp:Label>
                                        <asp:DropDownList ID="ddlStore" runat="server" TabIndex="5" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label23" runat="server" CssClass="label" Width="100" Text="Category"></asp:Label>
                                        <asp:DropDownList ID="ddlCategory" runat="server" TabIndex="5" CssClass="dropdownMandatory" AutoPostBack="true" OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select a Category" ControlToValidate="ddlCategory" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                        </cc:ValidatorCalloutExtender>--%>
                                    </li>
                                </div>
                                <li class="clear"></li>
                                <div class="divSeparation"></div>
                                <li class="clear"></li>

                                <div class="divL" style="width: 325px;">

                                    <li>
                                        <asp:Label ID="Label32" runat="server" CssClass="label" Text="Reciept No"></asp:Label>
                                        <asp:DropDownList ID="ddlRecieptNo" TabIndex="3" runat="server" CssClass="dropdown"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ErrorMessage="Select a RecieptNo" ControlToValidate="ddlRecieptNo" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="deletercpt"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator9">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label1" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtItemShortName" Width="130px" MaxLength="150" TabIndex="10"  />
                                        <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
                                        <asp:ImageButton ID="imgPopUp" runat="server" OnClientClick="return  popUpStockSelectClick();" ImageUrl="~/images/Search_1.png" Style="padding-left: 2px; margin-top: 0px; width: 30px; height: 19px;" />
                                        <%-- <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Item Required " ControlToValidate="txtItemShortName" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                        </cc:ValidatorCalloutExtender>--%>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label16" runat="server" CssClass="label" Text="Unit"></asp:Label>
                                        <asp:DropDownList ID="ddlUnit" runat="server" TabIndex="7" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator61" runat="server" ErrorMessage="Select a Unit" ControlToValidate="ddlUnit" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender61" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator61">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Text="Batch"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textbox" TabIndex="8" ID="txtBatch" MaxLength="20" />

                                    </li>
                                    <li>
                                        <asp:Label ID="Label11" runat="server" CssClass="label" Text="Expiry date"></asp:Label>
                                        <asp:TextBox ID="txtExpiryDate" CssClass="textbox" TabIndex="10" runat="server"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtDob_CalendarExtender" runat="server" Enabled="True" Format="dd/MM/yyyy" TargetControlID="txtExpiryDate">
                                        </cc:CalendarExtender>
                                    </li>
                                  
                                      
                                    <li style="">display:none;
                                <asp:Label ID="Label6" runat="server" CssClass="label" Text="Item"></asp:Label>
                                        <asp:DropDownList ID="ddlItem" runat="server" TabIndex="6" CssClass="dropdownMandatory" AutoPostBack="true" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                      
                                  


                                    <li>
                                        <asp:Label ID="Label22" runat="server" CssClass="label" Text="Purchase Rate"></asp:Label>
                                        <asp:TextBox ID="txtPurchaseRate" AutoComplete="off" CssClass="textbox" runat="server" TabIndex="12" MaxLength="10" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPurchaseRate" ErrorMessage="Enter a valid PurchaseRate" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="1" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPurchaseRate" SetFocusOnError="true" ControlToCompare="txtMRP" ValidationGroup="add" Display="None" ErrorMessage="Purchase rate Should be less than MRP" Operator="LessThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ErrorMessage="PurchaseRate required" ControlToValidate="txtPurchaseRate" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator5">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label15" runat="server" CssClass="label" Text="MRP"></asp:Label>
                                        <asp:TextBox ID="txtMRP" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>







                                </div>
                                <div class="divL" style="width: 325px;">

                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Text="Purchase VAT"></asp:Label>
                                        <asp:TextBox ID="txtPurchaseVat" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label12" runat="server" CssClass="label" Text="Discount %"></asp:Label>
                                        <asp:TextBox ID="txtDiscountPer" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label24" runat="server" CssClass="label" Text="Discount Amount"></asp:Label>
                                        <asp:TextBox ID="txtDiscountAmt" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label25" runat="server" CssClass="label" Text="Profit %"></asp:Label>
                                        <asp:TextBox ID="txtProfitPer" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label27" runat="server" CssClass="label" Text="Retail Price"></asp:Label>
                                        <asp:TextBox ID="txtRetailPrice" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtRetailPrice" SetFocusOnError="true" ControlToCompare="txtPurchaseRate" ValidationGroup="add" Display="None" ErrorMessage="Retail Price rate Should be greater than or equal to Purchase Rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" runat="server" Enabled="True" TargetControlID="CompareValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label10" runat="server" CssClass="label" Text="Whole Sale Price"></asp:Label>
                                        <asp:TextBox ID="txtWholeSalePrice" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="txtWholeSalePrice" SetFocusOnError="true" ControlToCompare="txtPurchaseRate" ValidationGroup="add" Display="None" ErrorMessage="Whole Sale Price rate Should be greater than or equal to  Purchase Rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" runat="server" Enabled="True" TargetControlID="CompareValidator3">
                                        </cc:ValidatorCalloutExtender>
                                    </li>


                                </div>
                                <div style="float: left; width: 300px;">

                                    <li style="padding-top: 10px;">
                                        <asp:Label ID="Label28" runat="server" CssClass="label" Width="100" Text="Privilage Price"></asp:Label>
                                        <asp:TextBox ID="txtPrivilagePrice" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtPrivilagePrice" SetFocusOnError="true" ControlToCompare="txtPurchaseRate" ValidationGroup="add" Display="None" ErrorMessage="Privilage Price rate Should be greater than or equal to  Purchase Rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" runat="server" Enabled="True" TargetControlID="CompareValidator4">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="padding-top: 15px;">
                                        <asp:Label ID="Label31" runat="server" CssClass="label" Width="100" Text="VAT"></asp:Label>
                                        <asp:TextBox ID="txtVat" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>
                                    <li style="padding-top: 10px;">
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Width="100" Text="Quantity"></asp:Label>
                                        <asp:TextBox ID="txtQuantity" CssClass="textbox" runat="server" TabIndex="11" MaxLength="10" onKeypress="CheckNumWithDecimal(event);" Style="text-align: right;"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="1" Type="Double"></asp:RangeValidator>

                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label29" runat="server" CssClass="label" Width="100" Text="Actual Qty"></asp:Label>
                                        <asp:TextBox ID="txtActqty" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                        <asp:RangeValidator ID="RangeValidator3" runat="server" ControlToValidate="txtActqty" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="1" Type="Double"></asp:RangeValidator>

                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" runat="server" Enabled="True" TargetControlID="RangeValidator3">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li>
                                        <asp:Label ID="Label30" runat="server" CssClass="label" Width="100" Text="Free"></asp:Label>
                                        <asp:TextBox ID="txtFree" CssClass="textbox" runat="server" MaxLength="11" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>




                                </div>
                                <li class="clear"></li>
                                <li class="btnMargin">
                                    <asp:Button Text="Insert" ID="btnAdd" runat="server" OnClick="btnAdd_Click" TabIndex="16" ValidationGroup="add" CssClass="button" />
                                    <asp:Button Text="Clear" ID="btnClearAdd" runat="server" CssClass="button" TabIndex="17" OnClientClick="ItemClear();return false;" />
                                    <asp:Button Text="Delete" ID="btnMRDelete" runat="server" CssClass="button" TabIndex="17" OnClick="btnMRDelete_Click" ValidationGroup="deletercpt" />

                                </li>

                                <li class="clear"></li>

                                 <div style=" width:950px; overflow:auto;">
                                <li>
                                    <asp:GridView ID="gvItems" runat="server" CssClass="grid"
                                        EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="940px"
                                        BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items" OnRowDeleting="gvItems_RowDeleting"
                                        OnSelectedIndexChanging="gvItems_SelectedIndexChanging" PageSize="100">
                                        <AlternatingRowStyle BackColor="#CCE0EB" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBoxa1" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton1q2" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Edit_bg1.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox31" runat="server"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="ImageButton232" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="SLNO" ItemStyle-HorizontalAlign="Center">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox21" runat="server" Text='<%# Bind("SLNO") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvSLNO" runat="server" Text='<%# Bind("SLNO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Item">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox41" runat="server" Text='<%# Bind("Item") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <div style="overflow: auto; width: 250px;">
                                                        <asp:Label ID="lblgvItem" runat="server" Text='<%# Bind("Item") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FK_Item" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox4322" runat="server" Text='<%# Bind("FK_Items") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvFK_Items" runat="server" Text='<%# Bind("FK_Items") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Unit">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox45513" runat="server" Text='<%# Bind("Units") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <div style="overflow: auto; width: 60px;">
                                                        <asp:Label ID="lblgvUnits" runat="server" Text='<%# Bind("Units") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="FK_Unit" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox43229" runat="server" Text='<%# Bind("FK_Units") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvFK_Units" runat="server" Text='<%# Bind("FK_Units") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                            <asp:TemplateField HeaderText="Batch">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox493" runat="server" Text='<%# Bind("BatchNo") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <div style="overflow: auto; width: 120px;">
                                                        <asp:Label ID="lblgvBatchNo" runat="server" Text='<%# Bind("BatchNo") %>'></asp:Label>
                                                    </div>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Expiry Date">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox66" runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvExpiryDate" runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase Rate">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox68" runat="server" Text='<%# Bind("PurchaseRate") %>' Style="text-align: right;"></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPurchaseRate" runat="server" Text='<%# Bind("PurchaseRate") %>' Style="text-align: right; float: right;"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="MRP">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvMRP" runat="server" Text='<%# Bind("MRP") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvMRP" runat="server" Text='<%# Bind("MRP") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Purchase VAT">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvPurchaseVAT" runat="server" Text='<%# Bind("PurchaseVAT") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPurchaseVAT" runat="server" Text='<%# Bind("PurchaseVAT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount %">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvDicountPer" runat="server" Text='<%# Bind("DicountPer") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDicountPer" runat="server" Text='<%# Bind("DicountPer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Discount Amount">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvDiscountAmt" runat="server" Text='<%# Bind("DiscountAmt") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvDiscountAmt" runat="server" Text='<%# Bind("DiscountAmt") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Profit % ">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvProfitPer" runat="server" Text='<%# Bind("ProfitPer") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvProfitPer" runat="server" Text='<%# Bind("ProfitPer") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Retail Price">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvRetailPrice" runat="server" Text='<%# Bind("RetailPrice") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvRetailPrice" runat="server" Text='<%# Bind("RetailPrice") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="WholeSalePrice">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvWholeSalePrice" runat="server" Text='<%# Bind("WholeSalePrice") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvWholeSalePrice" runat="server" Text='<%# Bind("WholeSalePrice") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Privilage Price">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvPrivilagePrice" runat="server" Text='<%# Bind("PrivilagePrice") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvPrivilagePrice" runat="server" Text='<%# Bind("PrivilagePrice") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="VAT ">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvVAT" runat="server" Text='<%# Bind("VAT") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvVAT" runat="server" Text='<%# Bind("VAT") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>





                                            <asp:TemplateField HeaderText="Quantity Purchase" Visible="false">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtqtyhidden" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvquty" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Quantity">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="TextBox67" Style="width: 20px; text-align: right;" runat="server" Text='<%# Bind("Quantity") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>

                                                    <asp:Label ID="Label2522" runat="server" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Act Quantity">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvactqty" runat="server" Text='<%# Bind("ActQty") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvActQty" runat="server" Text='<%# Bind("ActQty") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Free">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvfree" runat="server" Text='<%# Bind("Free") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvFree" runat="server" Text='<%# Bind("Free") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>




                                            <asp:BoundField HeaderText="Net Amount" DataField="NetAmount" ItemStyle-HorizontalAlign="Right" />



                                            <asp:TemplateField HeaderText="MR">
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtgvFK_MaterialReciept" runat="server" Text='<%# Bind("FK_MaterialReciept") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblgvFK_MaterialReciept" runat="server" Text='<%# Bind("FK_MaterialReciept") %>'></asp:Label>
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
                                <li class="clear"></li>

                                <div class="divSeparation"></div>
                                <li class="clear"></li>
                                <div class="divL" style="width: 325px;">

                                    <li>
                                        <asp:Label ID="Label14" runat="server" CssClass="label" Text="Tot NetPur"></asp:Label>
                                        <asp:TextBox ID="TextBox2" CssClass="textbox" runat="server" MaxLength="10" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                    </li>



                                </div>
                                <div class="divL" style="width: 325px;">

                                    <li>
                                        <asp:Label ID="Label13" runat="server" CssClass="label" Text="Vat.Total"></asp:Label>
                                        <asp:TextBox ID="TextBox1" CssClass="textbox" runat="server" MaxLength="10" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                    </li>



                                </div>
                                <div class="divL" style="width: 325px;">

                                    <li>
                                        <asp:Label ID="Label33" runat="server" CssClass="label" Text="Fright"></asp:Label>
                                        <asp:TextBox ID="txtFright" CssClass="textbox" runat="server" MaxLength="10" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                    </li>



                                </div>
                                <div class="divL" style="width: 325px;">
                                    <li>
                                        <asp:Label ID="Label37" runat="server" CssClass="label" Text="Net Amount"></asp:Label>
                                        <asp:TextBox ID="txtBillNetAmount" CssClass="textbox" runat="server" MaxLength="10" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                    </li>


                                </div>


                                <li class="clear"></li>
                                <li class="btnMargin">
                                    <asp:HiddenField ID="hfMaterialRecieptId" runat="server" />
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfDate" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    <%-- <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave"  ValidationGroup="submit" OnClick="btnSave_Click" />--%>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="19" Text="Submit" ValidationGroup="submit" OnClick="btnSubmit_Click" />
                                    <asp:Button ID="btnClear" Text="Clear" runat="server" TabIndex="20" CausesValidation="false" CssClass="button" OnClick="btnClear_Click" />

                                </li>


                            </ul>

                        </div>
                              </div>
                           
                        <div class="clear"></div>
                             <div class="main_bg">  
                        <div id="divView" runat="server" style="display: none;">
                            <ul class="contentul">
                                <li>
                                    <li>
                                        <asp:Label ID="Label4" runat="server" CssClass="label" Text="Supplier"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchSupplier" runat="server" TabIndex="39" CssClass="dropdown"></asp:DropDownList>

                                    </li>
                                    <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Text="Store"></asp:Label>
                                        <asp:DropDownList ID="ddlSearchStore" runat="server" TabIndex="40" CssClass="dropdown"></asp:DropDownList>

                                    </li>

                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" TabIndex="41" runat="server" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" TabIndex="42" CssClass="button" Text="Clear" OnClientClick="ClearSearch(); return false;" />
                                    </li>
                                </li>

                            </ul>
                            <ul class="contentul">
                                <asp:GridView ID="gvMaterialReciept" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" PageSize="100" DataKeyNames="ID_MaterialReciept"
                                    OnRowDeleting="gvMaterialReciept_RowDeleting" OnSelectedIndexChanging="gvMaterialReciept_SelectedIndexChanging">
                                    <AlternatingRowStyle BackColor="#CCE0EB" />
                                    <Columns>
                                        <asp:BoundField DataField="SLNO" HeaderText="SLNO" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="MrNo" HeaderText="MrNo" />
                                        <asp:BoundField DataField="Date" HeaderText="Date" />
                                        <asp:BoundField DataField="Supplier" HeaderText="Supplier" />
                                        <asp:BoundField DataField="Store" HeaderText="Store" />
                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
      
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField  ItemStyle-HorizontalAlign="Center">
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
                                            <%--OnClick="Page_Changed" --%>
                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>' OnClick="Page_Changed" Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                                 </div>

                        <div class="clear"></div>
                        <asp:ValidationSummary ID="ValidationSummary1" CssClass="ValidationSummary" runat="server" ValidationGroup="submit" />
                     <%--   <asp:BulletedList ID="blMessage" CssClass="MessageList" runat="server"></asp:BulletedList>
                        <asp:Button ID="btnPurchaseOrderBlurTrigger" Text="text" runat="server" OnClick="btnPurchaseOrderBlurTrigger_Click" Style="display: none;" />--%>
                    </div>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
