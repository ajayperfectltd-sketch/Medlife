<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OpeningStocks.aspx.cs" Inherits="OpeningStocks" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc" %>
<%@ Register Src="Popup/CommonPopup.ascx" TagName="CommonPopup" TagPrefix="uc1" %>
<%@ Register Src="Popup/CommonPopUpGridSelection.ascx" TagName="CommonPopupGrid" TagPrefix="uc3" %>
<%@ Register Src="Popup/CommonPopupOnDelete.ascx" TagName="CommonPopupDelete" TagPrefix="uc2" %>
<%@ Register Src="~/Popup/CommonPopupPrivilege.ascx" TagPrefix="uc1" TagName="CommonPopupPrivilege" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <link href="css/main.css" rel="stylesheet" />
    <script src="js/jquery-1.10.2.js"></script>
    <script src="js/Custom.js"></script>
    <link href="css/toastr.css" rel="stylesheet" />
    <script src="js/alertify.min.js"></script>

    <link href="css/alertify.core.css" rel="stylesheet" />
    <link href="css/alertify.default.css" rel="stylesheet" />

    <script src="js/toastr.js"></script>
    <script src="js/Navigation.js"></script>


    <style>
       .stax-row {
    display: -ms-flexbox;
    display: flex;
    -ms-flex-align: center;
    align-items: center;
}
    </style>
   
    <script src="AssetLogin/js/jquery.min.js"></script>
         <script type="text/javascript">

           function pageLoad() {
               $("#<%#ChkIncStax.ClientID%>").change(function () {
                    TaxAmount();
                });
           }
           function SearchClear() {
               $("#ddlStoreSearch").val("0");
               $("#txtItemSearch").val("");
               $("#txtSearchBatch").val("");
               
           }
           function releasebtn() {


               $("#btnSubmit").prop('disabled', false);

           }
           function EnterTab(e, Id1) {

               var intKey = window.Event ? e.which : e.KeyCode;


               if (intKey == 13) {
                   e.returnValue = false;
                   $("#" + Id1).focus();
               }
           }
           function ShowDivOnButtonClick() {
               var x = document.getElementById("divEcelFormat");
               x.style.display = 'block';
           }
           function HideDivOnButtonClick() {
               var x = document.getElementById("divEcelFormat");
               x.style.display = 'none';
           }
           function ItemClear() {

             
               $("#ddlItem").val("0");
               $("#ddlCategory").val("0");
               $("#txtBatch").val("");
               var toDate = new Date();
               $("#txtExpiryDate").val(toDate.format("dd/MM/yyyy"));
               $("#txtPurchaseRate").val("0");
               $("#txtMRP").val("0");
               $("#txtSalesPrice").val("0");
               $("#txtSTaxAmount").val("0");
               $("#txtQuantity").val("0");
               $("#ChkIncStax").prop("checked", true);
               $("#txtCurrentStock").val("0");
               $("#txtOpeningLiability").val("0");
               $("#txtCurrentLiability").val("0");
               $("#txttaxAmount").val("0");
               $("#txtItemShortName").val('');
               CalculateTotal();
           }
           function TaxAmount() {

               var MRP = $("#txtMRP").val() == "" ? "0" : $("#txtMRP").val();
               var SalesPrice = $("#txtSalesPrice").val() == "" ? "0" : $("#txtSalesPrice").val();
               var TaxPercentage = $("#hfTotTaxsalesPercentage").val() == "" ? "0" : $("#hfTotTaxsalesPercentage").val();
               if ($('#ChkIncStax').is(":checked")) {

                   var taxAmount = ((parseFloat(SalesPrice)) * (parseFloat(TaxPercentage))) / ((parseFloat(TaxPercentage) + parseFloat(100)));
                   $("#txtSTaxAmount").val(parseFloat(taxAmount).toFixed(4));
                   $("#txtSalesPrice").val(parseFloat(SalesPrice).toFixed(4));

               }
               else {

                   var taxAmount = (parseFloat(SalesPrice) * parseFloat(TaxPercentage)) / parseFloat(100);
                   $("#txtSTaxAmount").val(taxAmount.toFixed(4));
                   var Sale = parseFloat(SalesPrice) + parseFloat(taxAmount)
                   // alert(Sale);
                   $("#txtSalesPrice").val(parseFloat(Sale).toFixed(4));
                   if (parseFloat(MRP) == parseFloat(SalesPrice)) {
                       alert('Sales Price not greater than MRP');
                       return false;
                   }
               }
           }
           //function CalculateTotal() {
           //    alert('g');
           //    var sumOpeningLiability = 0;
           //    var sumCurLiability = 0;
           //    $.each($("table[id*=" + 'gvItems' + "] tr"), function (index, item) {
           //        var val = $(item).find("[id*=gvItems_lblOpening_]").text();
           //        sumOpeningLiability += parseFloat(val != null ? (val != '' ? val : '0') : '0');
           //        //var val2 = $(item).find("[id*=gvItems_lblCurrent_]").text();
           //        //sumCurLiability += parseFloat(val2 != null ? (val2 != '' ? val2 : '0') : '0');
           //        alert(sumOpeningLiability);
           //    });

           //    $("#txtOpeninLiablity").val(sumOpeningLiability.toFixed(4));
           //    //$("#txtCurLiability").val(sumCurLiability.toFixed(4));
           //}
           function CalculateTotal() {

               var sum = 0;
               var sum2 = 0;
               $.each($("table[id*=" + 'gvItems' + "] tr"), function (index, item) {
                   var val = $(item).find("[id*=gvItems_lblOpening_]").text();
                   sum += parseFloat(val != null ? (val != '' ? val : '0') : '0');
                   var val2 = $(item).find("[id*=gvItems_lblCurrent_]").text();
                   sum2 += parseFloat(val2 != null ? (val2 != '' ? val2 : '0') : '0');
                   
               });
               
               $("#txtOpeninLiablity").val(sum.toFixed(4));
               $("#txtCurLiability").val(sum2.toFixed(4));

              
           }
           function popUpStockSelect() {

               try {
                   var Cat = $("#ddlCategory").val();
                   var ItemName = $("#txtItemShortName").val();
                   var hfUser = $("#hfUser").val();
                   var hfClient = $("#hfClient").val();
                   var ddlCategory = $("#ddlCategory").val();
                   var ddlSupplier = 0;
                   var hfItemvalue = $("#hfItemvalue").val();
                   var ItemType = "2";
                   $.ajax({
                       type: "POST",
                       url: 'Textile.asmx/GetUrlEncrypt',
                       data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier  + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (MenuHtml) {

                           //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val() + '&Supplier=' + $("#ddlSupplier").val() + '&Date=' + $("#txtInvoiceDate").val()

                           url = "ItemSearch.aspx?" + MenuHtml.d;

                           popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                           ",statusbar=no,menubar=no,resizable=0,width=1015," +
                           "height=650,left = 200,top = 100");
                           popUpObj.focus();

                       },
                       error: function (MenuHtml) {

                       }
                   });
                   //if (ItemName.length > 0) {
                   //    url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val();

                   //    popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                   //    ",statusbar=no,menubar=no,resizable=0,width=1015," +
                   //    "height=650,left = 200,top = 100");
                   //    popUpObj.focus();
                   //}
                   //else {

                   //}
                  
               }
               catch (e) {
                   alert(e);
               }

           }
           //function popUpStockSelectClick() {

           //    try {
           //        var ItemName = $("#txtItemShortName").val();
           //        var ItemName = $("#txtItemShortName").val();
           //        var hfUser = $("#hfUser").val();
           //        var hfClient = $("#hfClient").val();
           //        var ddlCategory = $("#ddlCategory").val();
           //        var ddlSupplier = $("#ddlSupplier").val();
           //        var txtInvoiceDate = $("#txtInvoiceDate").val();
           //        alert('g');
           //        var hfItemvalue = $("#hfItemvalue").val();
           //        var ItemType = "2";
           //        $.ajax({
           //            type: "POST",
           //            url: 'Textile.asmx/GetUrlEncrypt',
           //            data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier + "','txtInvoiceDate':'" + txtInvoiceDate + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
           //            contentType: "application/json; charset=utf-8",
           //            dataType: "json",
           //            success: function (MenuHtml) {

           //                //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val() + '&Supplier=' + $("#ddlSupplier").val() + '&Date=' + $("#txtInvoiceDate").val()

           //                url = "ItemSearch.aspx?" + MenuHtml.d;

           //                popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
           //                ",statusbar=no,menubar=no,resizable=0,width=1015," +
           //                "height=650,left = 200,top = 100");
           //                popUpObj.focus();

           //            },
           //            error: function (MenuHtml) {

           //            }
           //        });
           //        //alert(ItemName.toString());
           //        //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val();

           //        //popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
           //        //",statusbar=no,menubar=no,resizable=0,width=1015," +
           //        //"height=650,left = 200,top = 100");
           //        //popUpObj.focus();

           //        $("#txtBatch").focus();
           //    }
           //    catch (e) {
           //        alert(e);
           //    }

           //}
           function popUpStockSelectClick() {

               try {
                   
                   var ItemName = $("#txtItemShortName").val();

                   var hfUser = $("#hfUser").val();
                   var hfClient = $("#hfClient").val();
                   var ddlCategory = $("#ddlCategory").val();
                   var ddlSupplier = 0;
                   var hfItemvalue = $("#hfItemvalue").val();
                  // var date = new Date().format("dd/MM/yyyy");
                   //$("#txtInvoiceDate").val(date);
                   var txtInvoiceDate = new Date().format("dd/MM/yyyy");
                   var ItemType = "2";
                   $.ajax({
                       type: "POST",
                       url: 'Textile.asmx/GetUrlEncrypt',
                       data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier + "','txtInvoiceDate':'" + txtInvoiceDate + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
                      // data: "{'ItemName':'" + ItemName + "','hfUser':'" + hfUser + "','ddlCategory':'" + ddlCategory + "','ddlSupplier':'" + ddlSupplier + "','hfItemvalue':'" + hfItemvalue + "','hfClient':'" + hfClient + "','ItemType':'" + ItemType + "'}",
                       contentType: "application/json; charset=utf-8",
                       dataType: "json",
                       success: function (MenuHtml) {

                           //url = "ItemSearch.aspx?ItemType=2" + '&uid=' + $("#hfUser").val() + '&clnt=' + $("#hfClient").val() + '&itn=' + ItemName.toString() + '&ctgry=' + $("#ddlCategory").val() + '&Supplier=' + $("#ddlSupplier").val() + '&Date=' + $("#txtInvoiceDate").val()
                          
                           url = "ItemSearch.aspx?" + MenuHtml.d;

                           popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                           ",statusbar=no,menubar=no,resizable=0,width=1015," +
                           "height=650,left = 200,top = 100");
                           popUpObj.focus();

                       },
                       error: function (MenuHtml) {

                       }
                   });







                   //popUpObj = window.open(url, "Popup", "toolbar=no,scrollbars=no,location=no" +
                   //",statusbar=no,menubar=no,resizable=0,width=1015," +
                   //"height=650,left = 200,top = 100");
                   //popUpObj.focus();

                   //$("#ddlUnit").focus();
               }
               catch (e) {
                   alert(e);
               }

           }
           function ClearAdd() {
               try {
                   var dt = new Date();
                   $("#hfDate").val(dt.format("dd/MM/yyyy"));
                   $("#ChkIncStax").prop("checked", true);
                   $("#hfOpningID").val("");
                   $("#txtOpeninLiablity").val(0);
                   $("#txtCurLiability").val(0);
                 
                // DiscountOnTax();

                ItemClear();
            }
            catch (e) {
                alert(e);
            }
           }
           function EnterKeyFilter() {



               $(document).keypress(
                   function (event) {
                       if (event.which == '13') {
                           //alert(8);
                           event.preventDefault();
                       }


                   });

           }
           function uploadStartedabdc(sender, args) {
               // alert('dd');
               var filename = args.get_fileName();
               var filext = filename.substring(filename.lastIndexOf(".") + 1).toLowerCase();

               if (filext == "xlsx" || filext == "xls") {
                 
                   return true;

               }
               else {
                   alert('Please upload only Excel files ')
                   $('#FileUploadExcel').val('');
               }

           }
           function ValidateBeforeSubmit() {
               if ($("#ddlStore").val() == "0") {
                   alertify.alert("Please select a Store before submitting.");
                   return false;
               }

               var hasRows = false;
               $("table[id*='gvItems'] tr").each(function () {
                   if ($(this).find("td").length > 0) {
                       hasRows = true;
                       return false; // break
                   }
               });

               if (!hasRows) {
                   alertify.alert("Please add at least one item before submitting.");
                   return false;
               }

               return true;
           }

           function updateRemarkCount() {
               // placeholder if needed
           }



       </script>
</head>
<body onkeydown="EnterTabBody(event);"> 
    <form id="form1" runat="server">
           <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
   <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                 <div class="btn_bg5"> 
                       <div class="heading2">
                    <div class="user">Opening Stock</div>
                           <asp:Panel ID="divMenuButtons" runat="server">
                    <div style="float: right; margin-top:  -10px">
                          <asp:ImageButton ID="imgbtnAdd" ImageUrl="images/ADD.png" CssClass="imagebuttonStyle" TabIndex="24"  runat="server" OnClick="imgbtnAdd_Click" OnClientClick="ClearAdd();ShowAddDiv();" />
                      <asp:ImageButton ID="ImageButton1" ImageUrl="images/view.png"  CssClass="imagebuttonStyle" TabIndex="23"   runat="server" OnClientClick="SearchClear(); " OnClick="ImageButton1_Click" />
                    
                        </div>
                               </asp:Panel>
                           </div>
                    
                          
              

                    <div class="clear"></div>
                    <div class="group_bg2">
                        <div class="main_bg"> 
                        <div id="divAdd" runat="server" style="display: block;">
                            <ul class="contentul">
                                <div class="divL" style="width: 430px;">
                                   <li>
                                        <asp:Label ID="Label3" runat="server" CssClass="label" Width="80" Text="Store"></asp:Label>
                                        <asp:DropDownList ID="ddlStore"  runat="server" TabIndex="1" Width="245px" Onblur="btnStoreMaterialRecieptItems();" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select a Store" ControlToValidate="ddlStore" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator1">
                                        </cc:ValidatorCalloutExtender>
                                       

                                    </li>
                                    <li class="btnMargin" style="padding-left:85px">
                                      <asp:Button Text="Auto Fill" ID="btnAutofill" OnClick="btnAutofill_Click" runat="server" TabIndex="2" OnClientClick="CalculateTotal();" CssClass="button"  />
                                    </li>
                             
                                </div>

                           
                                <li class="clear"></li>
                                <div class="divSeparation"></div>
                                <li class="clear"></li>
                                <div class="divL" style="width: 310px;">

                                    <li>
                                        <asp:Label ID="Label23" runat="server" CssClass="label" Width="80" Text="Category"></asp:Label>
                                        <asp:DropDownList ID="ddlCategory" onkeydown="EnterTab(event,'imgPopUp');" runat="server" TabIndex="3" CssClass="dropdown" AutoPostBack="true"  OnSelectedIndexChanged="ddlCategory_SelectedIndexChanged"></asp:DropDownList>
                                   
                                    </li>
                                   
                                    <li>
                                        <asp:Label ID="Label11" runat="server" Width="80px" CssClass="label" Text="Expiry Date"></asp:Label>

                                        <asp:TextBox ID="txtExpiryDate" onkeydown="EnterTab(event,'txtPurchaseRate');" CssClass="textboxMandatory" Width="213px" TabIndex="6"  runat="server"></asp:TextBox>
                                        <cc:CalendarExtender ID="txtExpiryDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtExpiryDate" Format="dd/MM/yyyy" TodaysDateFormat="dd/MM/yyyy"></cc:CalendarExtender>
                                  <asp:RequiredFieldValidator ID="reqDate" runat="server" ErrorMessage="Date required" ControlToValidate="txtExpiryDate" Display="None" SetFocusOnError="True" ValidationGroup="submit"></asp:RequiredFieldValidator>
                                  <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender18" runat="server" Enabled="True" TargetControlID="reqDate">
                                  </cc:ValidatorCalloutExtender>
                                    </li>
                                     <li>
                                        <asp:Label ID="Label15" runat="server" CssClass="label" Width="80px" Text="MRP"></asp:Label>
                                        <asp:TextBox ID="txtMRP" onkeydown="EnterTab(event,'txtSalesPrice');" CssClass="textboxMandatory" TabIndex="9" Width="213px" runat="server" MaxLength="11" Style="text-align: right;" onblur="TaxAmount();" onKeypress="CheckNumWithDecimal1(event);">

                                        </asp:TextBox>
                                           <asp:RequiredFieldValidator ID="Req3" runat="server" ErrorMessage="MRP required" ControlToValidate="txtMRP" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="Req3">
                                        </cc:ValidatorCalloutExtender>
                                     <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtMRP" SetFocusOnError="true" ControlToCompare="txtPurchaseRate" Type="Double"  ValidationGroup="add" Display="None" ErrorMessage=" MRP Should be Greater than Purchase rate" Operator="GreaterThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" runat="server" Enabled="True" TargetControlID="CompareValidator1">
                                        </cc:ValidatorCalloutExtender>
                                         <asp:RangeValidator ID="ran5" runat="server" ControlToValidate="txtMRP" ErrorMessage="Enter a valid MRP" Display="None" MaximumValue="999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" runat="server" Enabled="True" TargetControlID="ran5">
                                        </cc:ValidatorCalloutExtender>
                                        </li>
                                                               <li>
                                        <asp:Label ID="Label5" runat="server" CssClass="label" Width="80px" Text="Tax Amount"></asp:Label>
                                        <asp:TextBox ID="txtSTaxAmount" onkeydown="EnterTab(event,'txtQuantity');" CssClass="textbox" TabIndex="12" Width="213px" runat="server" MaxLength="12" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                                    </li>
                                                        <li>
                                        <asp:Label ID="Label63" runat="server" CssClass="label" Width="80px" Text="Opening Liability"></asp:Label>
                                        <asp:TextBox ID="txtOpeningLiability" onkeydown="EnterTab(event,'txtCurrentLiability');" CssClass="textbox" TabIndex="15" Width="213px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                                    </li>
                                      
                                 <li  class="clear"></li>


                                </div>
                                <div class="divL" style="width: 270px;">
                               
                            
                           <li>
                                        <asp:Label ID="Label1" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                                        <asp:TextBox runat="server" CssClass="textboxMandatory" ID="txtItemShortName"  onkeydown="EnterTab(event,'txtBatch');" onBlur="popUpStockSelect();" Width="130px" MaxLength="150" TabIndex="4" />
                                        <asp:Button ID="btnItem" runat="server" Text="Item" Style="display: none;" />
                                        <asp:ImageButton ID="imgPopUp"  runat="server" OnClientClick="return  popUpStockSelectClick();" ImageUrl="~/images/serach_icon.png" Style="padding: 5px; margin: 0px 0px 0px 0px; position: absolute" />
                                         <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ErrorMessage="Item Required " ControlToValidate="txtItemShortName" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender117" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator10">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                                    <li style="display: none;">
                                        <asp:Label ID="Label6" runat="server" Width="80px" CssClass="label" Text="Item"></asp:Label>
                                        <asp:DropDownList ID="ddlItem" runat="server" OnSelectedIndexChanged="ddlItem_SelectedIndexChanged" AutoPostBack="true" CssClass="dropdownMandatory"></asp:DropDownList>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Select an Item" ControlToValidate="ddlItem" InitialValue="0" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator2">
                                        </cc:ValidatorCalloutExtender>
                                    </li>
                            
                                          <li>
                                        <asp:Label ID="Label22" runat="server" Width="80px" CssClass="label" Text="Pur Rate"></asp:Label>
                                        <asp:TextBox ID="txtPurchaseRate" onkeydown="EnterTab(event,'txttaxAmount');" AutoComplete="off" Width="130px" CssClass="textbox" runat="server" TabIndex="7" MaxLength="10" Style="text-align: right;" onKeypress="CheckNumWithDecimal1(event);" onkeyup="getpurchaseamount();" onblur="getSalesPrice(0);"></asp:TextBox>


                                        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtPurchaseRate" ErrorMessage="Enter a valid PurchaseRate" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValtxidatorCalloutExtender4" runat="server" Enabled="True" TargetControlID="RangeValidator2">
                                        </cc:ValidatorCalloutExtender>

                                      
                                    </li>
                                   
             
                                    
                                   
                                                 <li>
                                        <asp:Label ID="Label27" runat="server" CssClass="label" Width="80px" Text="Sales Price"></asp:Label>
                                       <asp:TextBox ID="txtSalesPrice" 
                                            onkeydown="EnterTab(event,'txtSTaxAmount');" 
                                            CssClass="textboxMandatory" 
                                            Width="130px" 
                                            TabIndex="10"  
                                            runat="server" 
                                            MaxLength="11" 
                                            Style="text-align: right;" 
                                            onKeypress="CheckNumWithDecimal(event);"
                                            onblur="TaxAmount();">
                                        </asp:TextBox>
                                     <asp:RequiredFieldValidator ID="Req5" runat="server" ErrorMessage="Sales price required" ControlToValidate="txtSalesPrice" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" runat="server" Enabled="True" TargetControlID="Req5">
                                        </cc:ValidatorCalloutExtender>
                                                       <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtSalesPrice" SetFocusOnError="true" Type="Double" ControlToCompare="txtMRP" ValidationGroup="add" Display="None" ErrorMessage="Sales Price rate Should not  be greater than MRP" Operator="LessThanEqual"></asp:CompareValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender17" runat="server" Enabled="True" TargetControlID="CompareValidator5">
                                        </cc:ValidatorCalloutExtender>
                                                    <asp:RangeValidator ID="rant" runat="server" ControlToValidate="txtSalesPrice" ErrorMessage="Enter a valid Sales Price" Display="None" MaximumValue="999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" runat="server" Enabled="True" TargetControlID="rant">
                                        </cc:ValidatorCalloutExtender>
                                    </li>

                                        <li>
                                        <asp:Label ID="Label9" runat="server" CssClass="label" Width="80px" Text="Quantity"></asp:Label>
                                        <asp:TextBox ID="txtQuantity" onkeydown="EnterTab(event,'txtCurrentStock');" CssClass="textboxMandatory" Width="130px" runat="server" TabIndex="13" MaxLength="10" Onkeyup="calculateActualQuantity();getpurchaseamount();" onKeypress="DecimalOrIntValidation(event);" Style="text-align: right;"></asp:TextBox>

                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Quantity required" ControlToValidate="txtQuantity" Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator4">
                                        </cc:ValidatorCalloutExtender>
                                        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity" ErrorMessage="Enter a valid Quantity" Display="None" MaximumValue="999999999999" ValidationGroup="add" MinimumValue="0.01" Type="Double"></asp:RangeValidator>

                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" runat="server" Enabled="True" TargetControlID="RangeValidator1">
                                        </cc:ValidatorCalloutExtender>
             



                                    </li>
                                            <li>
                                        <asp:Label ID="Label2" runat="server" CssClass="label" Width="80px" Text="Current Liability"></asp:Label>
                                        <asp:TextBox ID="txtCurrentLiability" onkeydown="EnterTab(event,'btnAdd');" CssClass="textbox" TabIndex="16" Width="130px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                                    </li>
           
                

                                        
                                </div>


                             
                                <div style="float: left; width: 270px;">

                                        <li>
                                        <asp:Label ID="Label7" runat="server" CssClass="label" Width="80px" Text="Batch"></asp:Label>
                                        <asp:TextBox runat="server" onkeydown="EnterTab(event,'txtExpiryDate');" CssClass="textboxMandatory" TabIndex="5" Width="130px" ID="txtBatch" MaxLength="20" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ErrorMessage="Enter Batch name" ControlToValidate="txtBatch"  Display="None" SetFocusOnError="True" ValidationGroup="add"></asp:RequiredFieldValidator>
                                        <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator18">
                                        </cc:ValidatorCalloutExtender>
                      

                                    </li>
                                                              <li>
                                        <asp:Label ID="Label31" runat="server" CssClass="label" Width="80px" Text="Pur.Tax"></asp:Label>
                                        <asp:TextBox ID="txttaxAmount" onkeydown="EnterTab(event,'txtMRP');" CssClass="textbox" TabIndex="8" Width="130px" runat="server" MaxLength="11" Style="text-align: right;"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                                    </li>
                                                  
             <li class="stax-row">
    <asp:Label ID="Label4" runat="server" Width="90px" CssClass="label" 
        Text="Include STax">
    </asp:Label>
    
    <asp:CheckBox ID="ChkIncStax" runat="server" Width="15px" 
        onclick="TaxAmount();" TabIndex="11" 
        style="margin-right: 10px;" />
    
    <asp:Button Text="STax" ToolTip="SalesTax" ID="btSubmit" 
        Style="padding: 0px; width: 50px; height: 23px; margin: 0px;" 
        ValidationGroup="Stax" CssClass="button" 
        OnClick="btSubmit_Click" runat="server" />
    
    <asp:RequiredFieldValidator ID="reqStax" runat="server" 
        ErrorMessage="Item required" ControlToValidate="txtItemShortName" 
        Display="None" SetFocusOnError="True" ValidationGroup="Stax">
    </asp:RequiredFieldValidator>
    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" runat="server" 
        Enabled="True" TargetControlID="reqStax">
    </cc:ValidatorCalloutExtender>
</li>
                                  
                                   
                            
                                    <li>
                                        <asp:Label ID="Label62" runat="server" CssClass="label" Width="80px" Text="Cur.Stock"></asp:Label>
                                        <asp:TextBox ID="txtCurrentStock"  onkeydown="EnterTab(event,'txtOpeningLiability');" CssClass="textbox" TabIndex="14" Width="130px" runat="server" MaxLength="11" Style="text-align: right;" onblur="getpurchaseamount();"  onKeypress="CheckNumWithDecimal1(event);"></asp:TextBox>

                                    </li>

                
                                
                                      <li  class="clear"></li>
                                    <br />
                                       <br />
                                       <br />
                                    </div>
                            
                                           
                                 <li class="clear"></li>
                                <div id="divEcelFormat" runat="server" style="display: none; background-color: #FFD6CA; position: relative; width: 50%; height: 100px; margin-left: 188px; margin-top: -796px; box-shadow: 0px 0px 15px #888;">
                                        <div style="left: 0px; margin-left: 7px; height: 30px; background-color: #2b737b;">
                                            <asp:Label ID="Label79" CssClass="label" runat="server" Style="width: 134%; margin-right: 0px; font-family: 'Segoe Print'; font-weight: bold; color: black; background-color: #2b737b; height: 22px; padding-top: 3px; text-align: left; margin-top: -116px"></asp:Label>
                                            <asp:ImageButton ID="ibtnExcelClose" runat="server" Width="4%" Height="64%" TabIndex="18"
                                                Style="float: right; margin-top: -118px; margin-right: -35%;" ToolTip="Close" ImageUrl="~/images/cross.png"
                                                OnClientClick="HideDivOnButtonClick();return false;" />
                                        </div>
                                        <div style="left: 0px; margin-left: 7px;">
                                            <asp:Label ID="Label80" CssClass="label" runat="server" Text="Excel document should contain  
                        Itemcode,Qty,MRP,Purchaserate,Salesprice,Batchno
                        Expirydate (dd/mm/yyyy)"
                                                Style="width: 134%; margin-right: 0px; color: white; font-family: open sans,Helvetica,sans-serif; background-color: #2b737b; font-size: 15px; height: 62px; padding-top: 3px; text-align: left; margin-top: -91px"></asp:Label>
                                        </div>
                                    </div>
                                    <div style="width: 50%; float: left">
                                        <cc:AsyncFileUpload ID="FileUploadExcel" Style="float: left; width: 40%; margin-left: 3%;" TabIndex="40" runat="server" CssClass="imageUploaderField"  OnUploadedComplete="FileUploadExcel_UploadedComplete" OnClientUploadStarted="uploadStartedabdc" />

                                        <asp:Button ID="btnExcelFormat" runat="server" Text="?" TabIndex="40" ToolTip="Excel Format" Style="width: 35px;" OnClientClick="ShowDivOnButtonClick();return false;" />
                                        <asp:Button ID="btnShowData" runat="server"  OnClick="btnShowData_Click" CssClass="button"
                                            TabIndex="41" Text="Show Data" UseSubmitBehavior="False" Width="85px" />
                                    </div>
                               <%-- <div class="divL" style="width: 270px;">
                                    <li style="width: 270px; float: left; margin-top: -20px">--%>
                                <li class="btnMargin" style="padding-left:85px">
                                        <asp:Button Text="Insert" ID="btnAdd" runat="server"  OnClick="btnAdd_Click" TabIndex="17" ValidationGroup="add" OnClientClick="CalculateTotal(); releasebtn();" CssClass="button" />
                                        <asp:Button Text="Clear" ID="btnClearAdd" runat="server" CssClass="button" TabIndex="18"  OnClick="btnClearAdd_Click" OnClientClick="ItemClear();" />
                                      </li>
                                  <%--  </li>
                                </div>--%>
                                    <li class="clear"></li>
                                <li class="clear"></li>
                                <div class="divSeparation"></div>

                       
                                <div style="width: 870px; overflow: auto; height: 350px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">
                         <%--  <div style="width: 930px; overflow: auto; height: 100px; scrollbar-face-color: blue; scrollbar-arrow-color: blue;">--%>

                                    <li>
                                        
                                 
                                       
                                        <asp:GridView ID="gvItems" runat="server" CssClass="grid"  Style="margin-left: 5px;"
                                            EmptyDataText="No Data Found!" AutoGenerateColumns="False" Width="920px"
                                            BackColor="White"  BorderStyle="Solid" OnRowDataBound="gvItems_RowDataBound" OnRowDeleting="gvItems_RowDeleting" BorderWidth="1px"  OnSelectedIndexChanging="gvItems_SelectedIndexChanging"  CellPadding="3" ForeColor="Black" GridLines="Vertical" DataKeyNames="FK_Items">
                                            <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                            <Columns>
                                                  <asp:TemplateField HeaderText="SLNO" Visible="false" ItemStyle-HorizontalAlign="Center">
                                                                <ItemTemplate>
                                                                    <%#Container.DataItemIndex+1 %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                       
                                                        <asp:ImageButton ID="ImageButton1q2" runat="server" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Edit_bg1.png" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                                <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="ImageButton232" runat="server" ToolTip="Delete" CommandName="Delete" OnClientClick="Confirm()" ImageUrl="~/images/modalClose.png" Width="20px" Height="20px" />
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
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

                                                <asp:TemplateField HeaderText="Batch" ItemStyle-HorizontalAlign="Left">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox493" runat="server" Text='<%# Bind("BatchNo") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <div style="overflow: auto; width: 120px;">
                                                            <asp:Label ID="lblgvBatchNo" runat="server" Text='<%# Bind("BatchNo") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField    HeaderText="Exp.Date">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox66" runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <div style="width: 80px; overflow: hidden; white-space: nowrap; text-overflow: ellipsis">

                                                 <asp:Label ID="lblgvExpiryDate" runat="server" Text='<%# Bind("ExpiryDate") %>'></asp:Label>
                                                        </div>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField HeaderText="Pur.Rate">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox68" runat="server" Text='<%# Bind("PurchaseRate") %>' Style="text-align: right;"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvPurchaseRate" runat="server" Text='<%# Bind("PurchaseRate") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             

                                                <asp:TemplateField HeaderText="MRP">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtgvMRP" runat="server" Text='<%# Bind("MRP") %>' Style="text-align: right;"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvMRP" runat="server" Text='<%# Bind("MRP") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                          <asp:TemplateField HeaderText="Sal.Price">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtgvSalesPrice" runat="server" Text='<%# Bind("SalPrice") %>' Style="text-align: right;"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvSalesPrice" runat="server" Text='<%# Bind("SalPrice") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                          

                                               


                                                <asp:TemplateField HeaderText="Qty">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="TextBox67" Style="width: 20px; text-align: right;" runat="server" Text='<%# Bind("OpeningStock") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>

                                                        <asp:Label ID="lblgvquty" runat="server" Text='<%# Bind("OpeningStock") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                      <asp:TemplateField HeaderText="Cur.Stock">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtgvCurrentStock" runat="server" Text='<%# Bind("CurrentStock") %>' Style="text-align: right;"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCurrentStock" runat="server" Text='<%# Bind("CurrentStock") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                             <%--      <asp:TemplateField HeaderText="OpeningLiability">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtgvOpeningLiability" runat="server" Text='<%# Bind("OpeningLiability") %>' Style="text-align: right;"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvOpeningLiability" runat="server" Text='<%# Bind("OpeningLiability") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                       <%--               <asp:TemplateField HeaderText="CurrentLiability">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtgvCurrentLiability" runat="server" Text='<%# Bind("CurrentLiability") %>' Style="text-align: right;"></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvCurrentLiability" runat="server" Text='<%# Bind("CurrentLiability") %>' Style="text-align: right; float: right;"></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>--%>
                                                   <asp:TemplateField HeaderText="Cur.Liability" HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>

                                                        <asp:Label ID="lblCurrent" runat="server" Style="float: right;" Text='<%#Eval("CurrentLiability") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                       
                                                        <asp:TemplateField HeaderText="OpeningLiability"  HeaderStyle-HorizontalAlign="Right" ItemStyle-HorizontalAlign="Right">

                                                    <ItemTemplate>

                                                        <asp:Label ID="lblOpening" runat="server" Style="float: right;" Text='<%#Eval("OpeningLiability") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                         

                                                  <asp:TemplateField HeaderText="ActualSalesPrice" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtActualSalesPrice" runat="server" Text='<%# Bind("ActualSalPrice") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvActualSalesPrice" runat="server" Text='<%# Bind("ActualSalPrice") %>'></asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>



                                                             <asp:TemplateField HeaderText="ActualSalesPrice" Visible="false">
                                                    <EditItemTemplate>
                                                        <asp:TextBox ID="txtActualPurRate" runat="server" Text='<%# Bind("ActualPurRate") %>'></asp:TextBox>
                                                    </EditItemTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lblgvActualPurRate" runat="server" Text='<%# Bind("ActualPurRate") %>'></asp:Label>
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

                                             
                                            </Columns>
                                             
                                            <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <FooterStyle BackColor="#CCCCCC" />
                                            <HeaderStyle  CssClass="gridviewHeaderStyle" HorizontalAlign="Center" Font-Bold="True" ForeColor="White" Height="30px" />
                                            <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                            <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                            <SortedAscendingHeaderStyle BackColor="#808080" />
                                            <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                            <SortedDescendingHeaderStyle BackColor="#383838" />
                                        </asp:GridView>
                                    </li>
                                </div>
                         <div class="divSeparation"></div>
                                <li class="clear"></li>
                                <div class="divL" style="width: 140px;">

                                    <li>
                                        <asp:Label ID="Label69" runat="server" CssClass="label" Width="60px" Text="Opening Liability"></asp:Label>
                                        <asp:TextBox ID="txtOpeninLiablity" TabIndex="19" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right;"  onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>
                                </div>
                           
                                <div class="divL" style="width: 140px;">
                                    <li>
                                        <asp:Label ID="Label8" runat="server" CssClass="label" Width="60px" Text="Current Liability"></asp:Label>
                                        <asp:TextBox ID="txtCurLiability" TabIndex="20" CssClass="textbox" Width="60px" runat="server" MaxLength="10" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>

                                    </li>
                                </div>
                                  <li class="clear"></li>
                                <li class="btnMargin" style="padding-left:85px">
                               
                                    <asp:HiddenField ID="hfClient" runat="server" />
                                    <asp:HiddenField ID="hfDate" runat="server" />
                                    <asp:HiddenField ID="hfUser" runat="server" />
                                    <asp:HiddenField ID="hfIsAdd" runat="server" />
                                    <asp:HiddenField ID="hfIsUpdate" runat="server" />
                                    <asp:HiddenField ID="hfIsDelete" runat="server" />
                                    <asp:HiddenField ID="hfOpningID" runat="server" />
                                    <asp:HiddenField ID="hfSlNo" runat="server" />
                                     <asp:HiddenField ID="hfuserName" runat="server" />
                                     <asp:HiddenField ID="hfMasterID" runat="server" />
                                       <asp:HiddenField ID="hfTotTaxsalesPercentage" runat="server" />
                                    <asp:HiddenField ID="hfAutofill" runat="server" />
                                      <asp:HiddenField ID="hfGroupID" runat="server" />
                                       <asp:HiddenField ID="hfExpirydays" runat="server" />
                                    
                                      <asp:HiddenField ID="HfItemsID" runat="server" />
                                     
                                        <asp:HiddenField ID="hfItem" runat="server" />
                                     <asp:HiddenField ID="hfExceldata" runat="server" />
                                      <asp:HiddenField ID="hfID" runat="server" />
                                        <asp:HiddenField ID="hfGID" runat="server" />
                                    
                                 

                                    <%-- <asp:Button Text="Submit" runat="server" CssClass="button" ID="btnSave"  ValidationGroup="submit" OnClick="btnSave_Click" />--%>
                                    <asp:Button ID="btnSubmit" runat="server" CssClass="button" TabIndex="21"  OnClick="btnSubmit_Click" Text="Submit" ValidationGroup="submit" OnClientClick="return ValidateBeforeSubmit();" />
                                    <asp:Button ID="btnClear" Text="Clear" runat="server" TabIndex="22"  OnClick="btnClear_Click" OnClientClick="ClearAdd(); " CausesValidation="false" CssClass="button" />
                         
                                </li>



                            </ul>
                        </div>
                            </div>
                          
                        <div id="DivPurchaseApprove" class="modalpopup" style="display: none; background-color: #0C3A5E; height: 460px; width: 720px;">
                            <div style="width: 100%; text-align: center; height: 60px;">

                                <li style="text-align: center; padding-top: 20px;">
                                    <asp:Label ID="Label14" runat="server" CssClass="popuplblCentre" Style="" Text="Purchase Approve" Font-Underline="true" Font-Bold="true" Font-Size="Large"></asp:Label>
                                </li>

                            </div>
                            <div class="divL" style="float: left; width: 290px; padding-left: 40px;">
                             

                        


                                <li>
                                    <asp:Label ID="Label20" runat="server" CssClass="popuplbl" Text="Bill No"></asp:Label>
                                    <asp:TextBox ID="txtpopupApproveBillNo" runat="server" CssClass="popuptxtMaterialRcpt"  TabIndex="46" ></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label35" runat="server" CssClass="popuplbl" Text="Bill Amount"></asp:Label>
                                    <asp:TextBox ID="txtpopupPurchaseAmoount" runat="server" TabIndex="47"  CssClass="popuptxtMaterialRcpt" Style="text-align: right;"></asp:TextBox>
                                    <asp:CustomValidator ID="CustomValidator1" runat="server" ControlToValidate="txtpopupPurchaseAmoount" SetFocusOnError="true" ErrorMessage="Total Discount Amount Should not Exceed the BillAmount" Display="None" EnableClientScript="true" ValidationGroup="Approve" ClientValidationFunction="ValidateCheckBox"></asp:CustomValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender41" runat="server" Enabled="True" TargetControlID="CustomValidator1">
                                    </cc:ValidatorCalloutExtender>
                                </li>

                                <li style="display: none;">
                                    <asp:Label ID="Label40" runat="server" CssClass="popuplbl" Text="Tax"></asp:Label>
                                    <asp:TextBox ID="txtpopupTax" runat="server" CssClass="popuptxtMaterialRcpt" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);"></asp:TextBox>
                                </li>


                                <li>
                                    <asp:Label ID="Label39" runat="server" CssClass="popuplbl" Text="Discount %"></asp:Label>
                                    <asp:TextBox ID="txtpopupDiscountPer" runat="server" TabIndex="48" CssClass="popuptxtMaterialRcpt" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" onkeyUp="CalculatePopDiscountAmount();   PopRoundOffCalc(); CalculatePopUpNetAmount();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ErrorMessage="Discount % required" ControlToValidate="txtpopupDiscountPer" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender40" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator25">
                                    </cc:ValidatorCalloutExtender>
                                    <asp:RangeValidator ID="RangeValidator8" runat="server" ControlToValidate="txtpopupDiscountPer" ErrorMessage="Enter a valid Discount %" Display="None" MaximumValue="100" ValidationGroup="Approve" MinimumValue="0" Type="Double"></asp:RangeValidator>

                                </li>
                                <li>
                                    <asp:Label ID="Label43" runat="server" CssClass="popuplbl" Text="Dis Amount"></asp:Label>
                                    <asp:TextBox ID="txtpopupDiscountAmount" TabIndex="49" runat="server" CssClass="popuptxtMaterialRcpt" Style="text-align: right;" onKeypress="CheckNumWithDecimal(event);" onkeyUp="CalculatePopDiscountPercentage(); PopRoundOffCalc();  CalculatePopUpNetAmount();"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator26" runat="server" ErrorMessage="Discount Amount required" ControlToValidate="txtpopupDiscountAmount" Display="None" SetFocusOnError="True" ValidationGroup="Approve"></asp:RequiredFieldValidator>
                                    <cc:ValidatorCalloutExtender ID="ValidatorCalloutExtender42" runat="server" Enabled="True" TargetControlID="RequiredFieldValidator26">
                                    </cc:ValidatorCalloutExtender>

                                </li>
                                <li>
                                    <asp:Label ID="Label57" runat="server" CssClass="popuplbl" Text="Loading"></asp:Label>
                                    <asp:TextBox ID="txtpopupLoading" runat="server" TabIndex="50"  Style="text-align: right;" CssClass="popuptxtMaterialRcpt" onKeypress="CheckNumWithDecimal(event);" onkeyUp=" PopRoundOffCalc();  CalculatePopUpNetAmount();"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label58" runat="server" CssClass="popuplbl" Text="UnLoading"></asp:Label>
                                    <asp:TextBox ID="txtpopupUnLoading" runat="server" TabIndex="51"  Style="text-align: right;" CssClass="popuptxtMaterialRcpt" onKeypress="CheckNumWithDecimal(event);" onkeyUp=" PopRoundOffCalc();  CalculatePopUpNetAmount();"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label59" runat="server" CssClass="popuplbl" Text="Display"></asp:Label>
                                    <asp:TextBox ID="txtpopupDisplay" TabIndex="52"  runat="server" Style="text-align: right;" CssClass="popuptxtMaterialRcpt" onKeypress="CheckNumWithDecimal(event);" onkeyUp=" PopRoundOffCalc();  CalculatePopUpNetAmount();"></asp:TextBox>
                                </li>
                                <li>
                                    <asp:Label ID="Label60" runat="server" CssClass="popuplbl" Text="Others"></asp:Label>
                                    <asp:TextBox ID="txtpopupOthers" TabIndex="53"  runat="server" Style="text-align: right;" CssClass="popuptxtMaterialRcpt" onKeypress="CheckNumWithDecimal(event);" onkeyUp=" PopRoundOffCalc();  CalculatePopUpNetAmount();"></asp:TextBox>
                                </li>

                            </div>
                          
                            <li class="clear"></li>
                            <div style="width: 100%; float: left; padding-left: 40px;">
                            </div>
                        </div>

                     
                         <div class="clear"></div>
                        <div class="main_bg"> 
                        <div id="divView" runat="server" style="">
                            <ul class="contentul">
                                <div class="divL">

                                    <li>
                                    
                                        <label class="label">Store</label>
                                        <asp:DropDownList ID="ddlStoreSearch" onkeydown="EnterTab(event,'btnSearch');"  runat="server" CssClass="dropdown" TabIndex="25"></asp:DropDownList>
                                    </li>
                                     <li>
                                    
                                        <label class="label">Item</label>
                                          <asp:TextBox ID="txtItemSearch" MaxLength="150" runat="server" TabIndex="4" AutoPostBack="false" CssClass="textbox"></asp:TextBox>
                                    </li
                                            <li>
                                    
                                        <label class="label">Batch No</label>
                                          <asp:TextBox ID="txtSearchBatch" runat="server"   MaxLength="20" TabIndex="4" AutoPostBack="false" CssClass="textbox"></asp:TextBox>
                                    </li
                                  
                        
                                    <li class="clear"></li>
                                    <li class="btnMargin">
                                        <asp:Button ID="btnSearch" runat="server" TabIndex="26" CssClass="button" Text="Search" OnClick="btnSearch_Click" />
                                        <asp:Button ID="btnSearchClear" runat="server" TabIndex="27" CssClass="button" Text="Clear" OnClientClick="SearchClear(); return false;" />

                                    </li>
                                </div>
                             
                            </ul>
                            <br class="clear" />
                            <ul class="contentul">
                                <asp:GridView ID="gvOpening" runat="server" CssClass="grid"
                                    EmptyDataText="No Data Found!" AutoGenerateColumns="False"
                                    BackColor="White"  BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical" AllowPaging="true" OnSelectedIndexChanging="gvOpening_SelectedIndexChanging" OnPageIndexChanging="gvOpening_PageIndexChanging" PageSize="10" DataKeyNames="ID_Stock,FK_Store,GroupID">
                                    <AlternatingRowStyle  CssClass="gridviewAlternativeRowStyle" />
                                    <Columns>
                             
                              
                                        <asp:BoundField DataField="Item" HeaderText="Item" />
                                           <asp:BoundField DataField="BatchNo" HeaderText="Batch No"  ItemStyle-HorizontalAlign="Left"/>
                                           <asp:BoundField DataField="ExpiryDate" HeaderText="Exp.Date" />
                                           <asp:BoundField DataField="PurchaseRate" HeaderText="Pur.Rate" ItemStyle-HorizontalAlign="Right" />
                                        <asp:BoundField DataField="ActualPurRate" HeaderText="Act.Purrate" ItemStyle-HorizontalAlign="Right" />
                                        
                                          <asp:BoundField DataField="SalPrice" HeaderText="Sale Price" ItemStyle-HorizontalAlign="Right" />
                                         <asp:BoundField DataField="ActualSalPrice" HeaderText="Act.Salprice" ItemStyle-HorizontalAlign="Right" />

                                        <asp:BoundField DataField="OpeningStock" HeaderText="Opng.Stock" ItemStyle-HorizontalAlign="Right" />
                                    <asp:BoundField DataField="CurrentStock" HeaderText="Cur.Stock" ItemStyle-HorizontalAlign="Right" />

                                        <asp:TemplateField ItemStyle-HorizontalAlign="Center">

                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1784" runat="server" CausesValidation="false" CommandName="Select" ToolTip="Edit" ImageUrl="~/images/Select_bg1.ico" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                    <FooterStyle BackColor="#CCCCCC" />
                                    <HeaderStyle  CssClass="gridviewHeaderStyle" Font-Bold="True" ForeColor="White" Height="30px" />
                                    <PagerStyle  CssClass="gridviewPagerStyle" ForeColor="White" HorizontalAlign="Center" Font-Bold="true" />
                                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                                    <SortedAscendingHeaderStyle BackColor="#808080" />
                                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                                    <SortedDescendingHeaderStyle BackColor="#383838" />
                                </asp:GridView>
                                <div class="Pager">
                                    <asp:Repeater ID="rptPager" runat="server">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkPage" runat="server" CssClass="PagerNos" Text='<%#Eval("Text") %>' CommandArgument='<%# Eval("Value") %>'  Enabled='<%# Eval("Enabled") %>'></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </div>
                            </ul>
                        </div>
                            </div>
                        <div class="clear"></div>
                    
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
                      <asp:Button ID="btS" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                <asp:Panel ID="divPop" runat="server" Style="display: none;" CssClass="DivStyle">
                    <div id="divhead" runat="server" class="PopupHeader">
                        <asp:Label ID="lblPopupH" runat="server"  CssClass="lblPopupHeader"></asp:Label>
                        <asp:ImageButton ID="btnClse" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                    </div>

                    <uc3:CommonPopupGrid  ID="CommonPopupGrid1" runat="server" />
                </asp:Panel>
                <cc:ModalPopupExtender ID="mpGrid" runat="server" PopupControlID="divPop" TargetControlID="btS"
                    CancelControlID="btnClse" Drag="true" PopupDragHandleControlID="divhead">
                </cc:ModalPopupExtender>

                            <asp:Button ID="btnD" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                        <asp:Panel ID="divPopUpdelete" runat="server" Style="display: none;" CssClass="DivStyle">
                            <div id="divPopheader" runat="server" class="PopupHeaderReason">
                                <asp:Label ID="lblPopupHeader1" runat="server" Text="Reason" ></asp:Label>
                                <asp:ImageButton ID="ImageButton5" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                            </div>
                            <div id="divPopupReason" runat="server" class="divPopupOnstores" style="display: none;">
                            <uc2:CommonPopupDelete ID="CommonPopupDelete1" runat="server" />
                                </div>
                        </asp:Panel>
                        <cc:ModalPopupExtender ID="mpDelete" runat="server" PopupControlID="divPopUpdelete" TargetControlID="btnD"
                            CancelControlID="btnClose" Drag="true" PopupDragHandleControlID="divPopheader">
                        </cc:ModalPopupExtender>
                                <asp:Button ID="btnP" runat="server" Text="Button" CausesValidation="false" Style="display: none;" />
                            <asp:Panel ID="pnlPrvlg" runat="server" Style="display: none;" CssClass="DivStyle">
                                <div id="divPrvlg" runat="server" class="PopupHeader">
                                    <asp:Label ID="Label10" runat="server" Text="Privilege" CssClass="lblPopupHeader"></asp:Label>
                                    <asp:ImageButton ID="btnCP" runat="server" ImageUrl="~/images/cross.png" CssClass="imgPopup" />
                                </div>
                                <uc1:CommonPopupPrivilege runat="server" ID="CommonPopupPrivilege" />
                            </asp:Panel>
                            <cc:ModalPopupExtender ID="mpPrivilege" runat="server" PopupControlID="pnlPrvlg" TargetControlID="btnP"
                                CancelControlID="btnCP" Drag="true" PopupDragHandleControlID="divPrvlg">
                            </cc:ModalPopupExtender>
                        <div class="clear"></div>
                        
                    </div>
       
        
            </ContentTemplate>
        </asp:UpdatePanel>
    </form>
</body>
</html>
